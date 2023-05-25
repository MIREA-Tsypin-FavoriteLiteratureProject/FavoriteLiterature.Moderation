using FavoriteLiterature.Moderation.Domain.Common.Pagination.Requests;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using MediatR;

namespace FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;

public class GetAllDraftsQuery : PagedRequest, IRequest<GetAllDraftsResponse>
{
}