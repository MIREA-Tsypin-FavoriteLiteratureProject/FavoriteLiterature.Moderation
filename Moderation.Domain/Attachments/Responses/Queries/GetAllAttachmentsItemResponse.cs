namespace FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;

public class GetAllAttachmentsItemResponse
{
    public Guid Id { get; set; }

    public Guid DraftId { get; set; }

    public Guid FileId { get; set; }

    public string AttachmentTypeId { get; set; } = null!;
}