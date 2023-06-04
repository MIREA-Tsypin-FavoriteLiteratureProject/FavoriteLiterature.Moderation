using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;
using FavoriteLiterature.Moderation.Domain.Common.Pagination.Requests;
using MediatR;

namespace FavoriteLiterature.Moderation.Domain.Attachments.Requests.Queries;

public class GetAllAttachmentsQuery : PagedRequest, IRequest<GetAllAttachmentsResponse>
{
}