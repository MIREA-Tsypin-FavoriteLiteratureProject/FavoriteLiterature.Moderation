using FavoriteLiterature.Moderation.Application.Policies;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Controllers;

public sealed class DraftsController : BaseApiController
{
    public DraftsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Authorize(Policy = nameof(RolePolicy.Critic))]
    public async Task<GetAllDraftsResponse> GetAllAsync([FromQuery] GetAllDraftsQuery query, CancellationToken cancellationToken)
        => await _mediator.Send(query, cancellationToken);

    [HttpGet("{id:guid}")]
    [Authorize(Policy = nameof(RolePolicy.Author))]
    public async Task<GetDraftResponse> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _mediator.Send(new GetDraftQuery(id), cancellationToken);

    [HttpPost]
    [Authorize(Policy = nameof(RolePolicy.Author))]
    public async Task<CreateDraftResponse> CreateAsync(CreateDraftCommand command, CancellationToken cancellationToken)
        => await _mediator.Send(command, cancellationToken);

    [HttpPost("{id:guid}")]
    [Authorize(Policy = nameof(RolePolicy.Critic))]
    public async Task<VerifyDraftResponse> VerifyDraftAsync(Guid id, VerifyDraftCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = nameof(RolePolicy.Author))]
    public async Task<UpdateDraftResponse> UpdateAsync(Guid id, UpdateDraftCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return await _mediator.Send(command, cancellationToken);
    }
}