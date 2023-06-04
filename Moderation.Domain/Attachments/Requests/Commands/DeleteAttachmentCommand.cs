using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using MediatR;

namespace FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;

public class DeleteAttachmentCommand : IRequest<DeleteAttachmentResponse>
{
    public Guid Id { get; }

    public DeleteAttachmentCommand(Guid id)
        => Id = id;
}