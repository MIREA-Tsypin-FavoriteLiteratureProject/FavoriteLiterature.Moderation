using System.Linq.Expressions;
using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace FavoriteLiterature.Moderation.Data.Repositories.Attachments;

public class AttachmentsRepository : GenericRepository<Attachment>, IAttachmentsRepository
{
    public AttachmentsRepository(FavoriteLiteratureModerationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Guid>> FindAllFileIdsAsync(Guid draftId, CancellationToken cancellationToken = default) 
        => await _dbContext.Attachments
            .Where(x => x.DraftId == draftId)
            .Select(x => x.FileId)
            .ToListAsync(cancellationToken);
}