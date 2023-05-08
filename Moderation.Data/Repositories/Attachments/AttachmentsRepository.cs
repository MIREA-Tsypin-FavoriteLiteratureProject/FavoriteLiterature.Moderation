using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories.Common;

namespace FavoriteLiterature.Moderation.Data.Repositories.Attachments;

public class AttachmentsRepository : GenericRepository<Attachment>, IAttachmentsRepository
{
    public AttachmentsRepository(FavoriteLiteratureModerationDbContext dbContext) : base(dbContext)
    {
    }
}