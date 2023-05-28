using System.Text.Json.Serialization;
using FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;
using MediatR;

namespace FavoriteLiterature.Moderation.Domain.Drafts.Requests.Commands;

public class VerifyDraftCommand : IRequest<VerifyDraftResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    public bool Verified { get; set; }
}