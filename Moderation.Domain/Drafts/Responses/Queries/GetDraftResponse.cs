namespace FavoriteLiterature.Moderation.Domain.Drafts.Responses.Queries;

public class GetDraftResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public List<Guid> Authors { get; }
    
    public List<Guid> Genres { get; }
}