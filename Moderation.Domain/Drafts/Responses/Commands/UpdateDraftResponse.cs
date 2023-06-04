namespace FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;

public class UpdateDraftResponse
{
    public Guid Id { get; }

    public UpdateDraftResponse(Guid id)
        => Id = id;
}