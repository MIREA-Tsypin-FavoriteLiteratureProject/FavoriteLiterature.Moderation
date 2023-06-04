namespace FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;

public class CreateAttachmentResponse
{
    public Guid Id { get; }

    public CreateAttachmentResponse(Guid id)
        => Id = id;
}