using FavoriteLiterature.Moderation.Domain.Common.Pagination.Responses;

namespace FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;

public class GetAllAttachmentsResponse : PagedResponse<GetAllAttachmentsItemResponse>
{
    public GetAllAttachmentsResponse(IEnumerable<GetAllAttachmentsItemResponse> items) : base(items)
    {
    }
}