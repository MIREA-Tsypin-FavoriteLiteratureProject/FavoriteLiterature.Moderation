using System.Text;
using AutoMapper;
using FavoriteLiterature.Moderation.Application.Options;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using FavoriteLiterature.Moderation.Domain.RabbitMq;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FavoriteLiterature.Moderation.Application.Handlers.Drafts.Commands;

public sealed class VerifyDraftCommandHandler : IRequestHandler<VerifyDraftCommand, VerifyDraftResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly IMapper _mapper;

    public VerifyDraftCommandHandler(IUnitOfWork unitOfWork, IOptions<RabbitMqOptions> rabbitMqOptions, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _rabbitMqOptions = rabbitMqOptions.Value;
        _mapper = mapper;
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
            var message = _mapper.Map<AcceptedDraftMessage>(draftData);
            var files = await _unitOfWork.AttachmentsRepository.FindAllDraftFilesAsync(draftData.Id, cancellationToken);
            message.Files = files;
            SendMessage(message);
        }
        
        await _unitOfWork.BeginTransactionAsync(new[]
        {
            () => _unitOfWork.DraftsRepository.Remove(draftData)
        });
        
        return new VerifyDraftResponse(draftData.Id);
    }

    private void SendMessage(AcceptedDraftMessage message)
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

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        
        channel.BasicPublish(exchange: "",
            routingKey: _rabbitMqOptions.Queue,
            basicProperties: null,
            body: body);
    }
}