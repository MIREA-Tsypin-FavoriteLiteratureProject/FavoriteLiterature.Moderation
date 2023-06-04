namespace FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;

public class DeleteAttachmentResponse
{
    public Guid Id { get; }

    public DeleteAttachmentResponse(Guid id)
        => Id = id;
}