using FavoriteLiterature.Moderation.Application.Policies;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Requests.Queries;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Controllers;

public sealed class AttachmentsController : BaseApiController
{
    public AttachmentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = nameof(RolePolicy.Author))]
    public async Task<FileContentResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAttachmentQuery(id), cancellationToken);
        return File(response.FileContents, response.ContentType, response.FileDownloadName);
    }

    [HttpGet]
    [Authorize(Policy = nameof(RolePolicy.Critic))]
    public async Task<GetAllAttachmentsResponse> GetAllAsync([FromQuery] GetAllAttachmentsQuery query, CancellationToken cancellationToken)
        => await _mediator.Send(query, cancellationToken);

    [HttpPost]
    [Authorize(Policy = nameof(RolePolicy.Author))]
    public async Task<CreateAttachmentResponse> CreateAsync([FromForm] CreateAttachmentCommand command, 
        CancellationToken cancellationToken) 
        => await _mediator.Send(command, cancellationToken);

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = nameof(RolePolicy.Author))]
    public async Task<DeleteAttachmentResponse> DeleteAsync(Guid id, CancellationToken cancellationToken)
        => await _mediator.Send(new DeleteAttachmentCommand(id), cancellationToken);
}