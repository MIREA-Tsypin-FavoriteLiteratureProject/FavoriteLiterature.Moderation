using AutoMapper;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using MediatR;

namespace FavoriteLiterature.Moderation.Application.Handlers.Drafts.Commands;

public sealed class CreateDraftCommandHandler : IRequestHandler<CreateDraftCommand, CreateDraftResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDraftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateDraftResponse> Handle(CreateDraftCommand command, CancellationToken cancellationToken)
    {
        var draftData = _mapper.Map<Draft>(command);

        await _unitOfWork.BeginTransactionAsync(new[]
        {
            () => _unitOfWork.DraftsRepository.Add(draftData)
        });

        return new CreateDraftResponse(draftData.Id);
    }
}