namespace FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;

public class GetAttachmentResponse
{
    public byte[] FileContents { get; set; }

    public string ContentType { get; set; }

    public string? FileDownloadName { get; set; }

    public GetAttachmentResponse(byte[] fileContents, string contentType, string? fileDownloadName)
    {
        FileContents = fileContents;
        ContentType = contentType;
        FileDownloadName = fileDownloadName;
    }
}