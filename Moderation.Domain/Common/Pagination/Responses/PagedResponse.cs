namespace FavoriteLiterature.Moderation.Domain.Common.Pagination.Responses;

public abstract class PagedResponse<T>
{
    protected PagedResponse(IEnumerable<T> items)
    {
        Items = items;
    }

    public IEnumerable<T> Items { get; }
}