using System.ComponentModel.DataAnnotations;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using MediatR;

namespace FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;

public class CreateDraftCommand : IRequest<CreateDraftResponse>
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; } 

    [Required]
    public List<Guid> Authors { get; set; }

    [Required]
    public List<Guid> Genres { get; set; }
}