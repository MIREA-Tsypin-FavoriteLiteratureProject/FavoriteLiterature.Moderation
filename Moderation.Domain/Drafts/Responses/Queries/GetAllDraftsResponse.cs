using FavoriteLiterature.Moderation.Domain.Common.Pagination.Responses;

namespace FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;

public class GetAllDraftsResponse : PagedResponse<GetAllDraftsItemResponse>
{
    public GetAllDraftsResponse(IEnumerable<GetAllDraftsItemResponse> items) : base(items)
    {
    }
}