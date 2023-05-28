using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using MediatR;

namespace FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;

public class UpdateDraftCommand : IRequest<UpdateDraftResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
    [Required]
    public List<Guid> Genres { get; set; }
}