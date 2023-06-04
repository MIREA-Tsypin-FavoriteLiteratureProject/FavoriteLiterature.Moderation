namespace FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;

public class CreateDraftResponse
{
    public Guid Id { get; }

    public CreateDraftResponse(Guid id)
        => Id = id;
}