using System.ComponentModel.DataAnnotations;
using FavoriteLiterature.Moderation.Domain.Attachments.Responses.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FavoriteLiterature.Moderation.Domain.Attachments.Requests.Commands;

public class CreateAttachmentCommand : IRequest<CreateAttachmentResponse>
{
    [Required]
    public Guid DraftId { get; set; }

    [Required]
    public IFormFile File { get; set; }
}