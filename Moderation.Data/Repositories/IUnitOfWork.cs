using FavoriteLiterature.Moderation.Data.Repositories.Attachments;
using FavoriteLiterature.Moderation.Data.Repositories.Drafts;

namespace FavoriteLiterature.Moderation.Data.Repositories;

public interface IUnitOfWork
{
    IAttachmentsRepository AttachmentsRepository { get; }
    IDraftsRepository DraftsRepository { get; }
    
    void Commit();
    Task CommitAsync();
    Task BeginTransactionAsync(IEnumerable<Action> actions);
    void Rollback();
    Task RollbackAsync();
}