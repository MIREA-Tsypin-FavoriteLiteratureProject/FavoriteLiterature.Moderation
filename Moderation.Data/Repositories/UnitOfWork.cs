using FavoriteLiterature.Moderation.Data.Repositories.Attachments;
using FavoriteLiterature.Moderation.Data.Repositories.Drafts;

namespace FavoriteLiterature.Moderation.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly FavoriteLiteratureModerationDbContext _dbContext;

    public IAttachmentsRepository AttachmentsRepository { get; }
    public IDraftsRepository DraftsRepository { get; }

    public UnitOfWork(FavoriteLiteratureModerationDbContext dbContext, IAttachmentsRepository attachmentsRepository,
        IDraftsRepository draftsRepository)
    {
        _dbContext = dbContext;
        AttachmentsRepository = attachmentsRepository;
        DraftsRepository = draftsRepository;
    }

    public void Commit()
        => _dbContext.SaveChanges();

    public async Task CommitAsync()
        => await _dbContext.SaveChangesAsync();

    public async Task BeginTransactionAsync(IEnumerable<Action> actions)
    {
        try
        {
            foreach (var action in actions)
            {
                action();
            }
            await CommitAsync();
        }
        catch (Exception)
        {
            await RollbackAsync();
            throw;
        }
    }

    public void Rollback()
        => _dbContext.Dispose();

    public async Task RollbackAsync()
        => await _dbContext.DisposeAsync();
}