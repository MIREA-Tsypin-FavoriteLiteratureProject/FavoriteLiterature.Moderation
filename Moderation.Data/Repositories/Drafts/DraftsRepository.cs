using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories.Common;

namespace FavoriteLiterature.Moderation.Data.Repositories.Drafts;

public class DraftsRepository : GenericRepository<Draft>, IDraftsRepository
{
    public DraftsRepository(FavoriteLiteratureModerationDbContext dbContext) : base(dbContext)
    {
    }
}