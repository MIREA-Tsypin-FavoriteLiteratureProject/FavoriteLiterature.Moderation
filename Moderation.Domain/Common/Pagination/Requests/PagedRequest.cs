namespace FavoriteLiterature.Moderation.Domain.Common.Pagination.Requests;

public abstract class PagedRequest
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
}