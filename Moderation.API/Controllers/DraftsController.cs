using FavoriteLiterature.Moderation.Domain.Drafts.Requests.Queries;
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
}