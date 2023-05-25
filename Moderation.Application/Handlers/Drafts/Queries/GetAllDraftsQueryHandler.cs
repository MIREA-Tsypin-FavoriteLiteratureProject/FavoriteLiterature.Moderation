using AutoMapper;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using MediatR;

namespace FavoriteLiterature.Moderation.Application.Handlers.Drafts.Queries;

public sealed class GetAllDraftsQueryHandler : IRequestHandler<GetAllDraftsQuery, GetAllDraftsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllDraftsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<GetAllDraftsResponse> Handle(GetAllDraftsQuery query, CancellationToken cancellationToken)
    {
        var genresData = await _unitOfWork.DraftsRepository.GetAllAsync(
                query.Skip, query.Take, 
            cancellationToken);

        return new GetAllDraftsResponse(_mapper.Map<IEnumerable<GetAllDraftsItemResponse>>(genresData));
    }
}