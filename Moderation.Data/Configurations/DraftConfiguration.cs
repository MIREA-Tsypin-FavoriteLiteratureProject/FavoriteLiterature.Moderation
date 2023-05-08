using FavoriteLiterature.Moderation.Data.Common;
using FavoriteLiterature.Moderation.Data.Configurations.Abstract;
using FavoriteLiterature.Moderation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteLiterature.Moderation.Data.Configurations;

public sealed class DraftConfiguration : BaseEntityConfiguration<Draft>
{
    public override void Configure(EntityTypeBuilder<Draft> builder)
    {
        builder.ToTable(ModerationApiTables.Drafts);
        builder.HasKey(x => x.Id).HasName($"{ModerationApiTables.Drafts}_pkey");

        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Authors).HasColumnName("authors").IsRequired();
        builder.Property(x => x.Genres).HasColumnName("genres").IsRequired();
    }
}