using FavoriteLiterature.Moderation.Data.Configurations;
using FavoriteLiterature.Moderation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FavoriteLiterature.Moderation.Data;

public sealed class FavoriteLiteratureModerationDbContext : DbContext
{
    #region DbSets

    public DbSet<Attachment> Attachments { get; set; } = null!;
    
    public DbSet<Draft> Drafts { get; set; } = null!;

    #endregion
    
    public FavoriteLiteratureModerationDbContext(DbContextOptions<FavoriteLiteratureModerationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new AttachmentConfiguration().Configure(modelBuilder.Entity<Attachment>());
        new DraftConfiguration().Configure(modelBuilder.Entity<Draft>());
    }
}