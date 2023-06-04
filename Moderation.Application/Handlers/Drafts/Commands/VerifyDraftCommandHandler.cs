using System.Text;
using FavoriteLiterature.Moderation.Application.Options;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FavoriteLiterature.Moderation.Application.Handlers.Drafts.Commands;

public sealed class VerifyDraftCommandHandler : IRequestHandler<VerifyDraftCommand, VerifyDraftResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private static RabbitMqOptions _rabbitMqOptions;

    public VerifyDraftCommandHandler(IUnitOfWork unitOfWork, IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _unitOfWork = unitOfWork;
        _rabbitMqOptions = rabbitMqOptions.Value;
    }

    public async Task<VerifyDraftResponse> Handle(VerifyDraftCommand command, CancellationToken cancellationToken)
    {
        var draftData = await _unitOfWork.DraftsRepository.GetAsync(draft =>
                draft.Id == command.Id,
            cancellationToken);
        if (draftData == null)
        {
            throw new ArgumentException("Draft is not found!", nameof(command.Id));
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
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            Port = _rabbitMqOptions.Port,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: _rabbitMqOptions.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(draft));
        
        channel.BasicPublish(exchange: "",
            routingKey: _rabbitMqOptions.Queue,
            basicProperties: null,
            body: body);
    }
}