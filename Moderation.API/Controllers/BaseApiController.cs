using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}