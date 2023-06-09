﻿using FavoriteLiterature.Moderation.Data.Entities;
using FavoriteLiterature.Moderation.Data.Repositories.Common;

namespace FavoriteLiterature.Moderation.Data.Repositories.Attachments;

public interface IAttachmentsRepository : IGenericRepository<Attachment>
{
    Task<List<Guid>> FindAllFileIdsAsync(Guid draftId, CancellationToken cancellationToken = default);
}