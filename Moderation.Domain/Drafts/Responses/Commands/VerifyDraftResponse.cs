namespace FavoriteLiterature.Moderation.Domain.Drafts.Responses.Commands;

public class VerifyDraftResponse
{
    public Guid Id { get; }

    public VerifyDraftResponse(Guid id)
        => Id = id;
}