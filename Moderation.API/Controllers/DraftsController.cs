using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DraftsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<GetAllDraftsResponse> GetAllAsync([FromQuery] GetAllDraftsQuery query, CancellationToken cancellationToken)
        => await _mediator.Send(query, cancellationToken);

    [HttpGet("{id:guid}")]
    public async Task<GetDraftResponse> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _mediator.Send(new GetDraftQuery(id), cancellationToken);

    [HttpPost]
    public async Task<CreateDraftResponse> CreateAsync(CreateDraftCommand command, CancellationToken cancellationToken)
        => await _mediator.Send(command, cancellationToken);

    [HttpPost("{id:guid}")]
    public async Task<VerifyDraftResponse> VerifyDraftAsync(Guid id, VerifyDraftCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task<UpdateDraftResponse> UpdateAsync(Guid id, UpdateDraftCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return await _mediator.Send(command, cancellationToken);
    }
}