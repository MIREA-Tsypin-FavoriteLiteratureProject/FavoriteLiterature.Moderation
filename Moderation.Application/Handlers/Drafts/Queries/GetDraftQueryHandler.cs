using AutoMapper;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using MediatR;

namespace FavoriteLiterature.Moderation.Application.Handlers.Drafts.Queries;

public sealed class GetDraftQueryHandler : IRequestHandler<GetDraftQuery, GetDraftResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetDraftQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetDraftResponse> Handle(GetDraftQuery query, CancellationToken cancellationToken)
    {
        var draftData = await _unitOfWork.DraftsRepository.GetAsync(draft =>
                draft.Id == query.Id,
            cancellationToken);

        return _mapper.Map<GetDraftResponse>(draftData);
    }
}