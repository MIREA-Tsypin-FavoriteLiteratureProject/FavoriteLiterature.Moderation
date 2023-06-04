using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Controllers;

public sealed class AttachmentsController : BaseApiController
{
    public AttachmentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<FileContentResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAttachmentQuery(id), cancellationToken);
        return File(response.FileContents, response.ContentType, response.FileDownloadName);
    }

    [HttpGet]
    public async Task<GetAllAttachmentsResponse> GetAllAsync([FromQuery] GetAllAttachmentsQuery query, CancellationToken cancellationToken)
        => await _mediator.Send(query, cancellationToken);

    [HttpPost]
    public async Task<CreateAttachmentResponse> CreateAsync([FromForm] CreateAttachmentCommand command, 
        CancellationToken cancellationToken) 
        => await _mediator.Send(command, cancellationToken);

    [HttpDelete("{id:guid}")]
    public async Task<DeleteAttachmentResponse> DeleteAsync(Guid id, CancellationToken cancellationToken)
        => await _mediator.Send(new DeleteAttachmentCommand(id), cancellationToken);
}