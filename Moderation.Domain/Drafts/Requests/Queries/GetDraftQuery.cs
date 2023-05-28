using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using MediatR;

namespace FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;

public class GetDraftQuery : IRequest<GetDraftResponse>
{
    public Guid Id { get; }

    public GetDraftQuery(Guid id)
        => Id = id;
}