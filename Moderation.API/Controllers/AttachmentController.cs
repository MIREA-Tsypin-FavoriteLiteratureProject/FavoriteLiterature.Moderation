using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Controllers;

public sealed class AttachmentController : BaseApiController
{
    public AttachmentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<FileContentResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAttachmentQuery(id), cancellationToken);
        return File(response.FileContents, response.ContentType, response.FileDownloadName);
    }

    [HttpPost]
    public async Task<CreateAttachmentResponse> CreateAsync([FromForm] CreateAttachmentCommand command, 
        CancellationToken cancellationToken) 
        => await _mediator.Send(command, cancellationToken);
}