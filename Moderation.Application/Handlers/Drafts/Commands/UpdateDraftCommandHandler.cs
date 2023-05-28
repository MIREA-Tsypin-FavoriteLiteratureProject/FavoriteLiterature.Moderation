using AutoMapper;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using MediatR;

namespace FavoriteLiterature.Moderation.Application.Handlers.Drafts.Commands;

public sealed class UpdateDraftCommandHandler : IRequestHandler<UpdateDraftCommand, UpdateDraftResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UpdateDraftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<UpdateDraftResponse> Handle(UpdateDraftCommand command, CancellationToken cancellationToken)
    {
        var draftData = await _unitOfWork.DraftsRepository.GetAsync(draft =>
                draft.Id == command.Id,
            cancellationToken);
        if (draftData == null)
        {
            throw new ArgumentException($"{command.Id} is not found.", nameof(command.Id));
        }
        
        _mapper.Map(command, draftData);
        
        await _unitOfWork.BeginTransactionAsync(new[]
        {
            () => _unitOfWork.DraftsRepository.Update(draftData)
        });

        return new UpdateDraftResponse(draftData.Id);
    }
}