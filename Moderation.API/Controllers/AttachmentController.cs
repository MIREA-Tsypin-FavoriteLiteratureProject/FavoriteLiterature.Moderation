using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Controllers;

public sealed class AttachmentController : BaseApiController
{
    public AttachmentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<CreateAttachmentResponse> CreateAsync([FromForm] CreateAttachmentCommand command, 
        CancellationToken cancellationToken) 
        => await _mediator.Send(command, cancellationToken);
}