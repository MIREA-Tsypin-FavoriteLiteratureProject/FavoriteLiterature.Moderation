using System.Text;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FavoriteLiterature.Moderation.Application.Handlers.Drafts.Commands;

public sealed class VerifyDraftCommandHandler : IRequestHandler<VerifyDraftCommand, VerifyDraftResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public VerifyDraftCommandHandler(IUnitOfWork unitOfWork) 
        => _unitOfWork = unitOfWork;

    public async Task<VerifyDraftResponse> Handle(VerifyDraftCommand command, CancellationToken cancellationToken)
    {
        var draftData = await _unitOfWork.DraftsRepository.GetAsync(draft =>
                draft.Id == command.Id,
            cancellationToken);
        if (draftData == null)
        {
            throw new ArgumentException($"{command.Id} is not found.", nameof(command.Id));
        }

        if (command.Verified)
        {
            SendMessage(draftData);
        }
        
        await _unitOfWork.BeginTransactionAsync(new[]
        {
            () => _unitOfWork.DraftsRepository.Remove(draftData)
        });
        
        return new VerifyDraftResponse(draftData.Id);
    }

    private static void SendMessage(Draft draft)
    {
        // TODO: add IOptions
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        const string queueName = "favLitQueue";
        
        channel.QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(draft));
        
        channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }
}