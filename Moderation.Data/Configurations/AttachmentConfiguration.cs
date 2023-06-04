using FavoriteLiterature.Moderation.Data.Common;
using FavoriteLiterature.Moderation.Data.Configurations.Abstract;
using FavoriteLiterature.Moderation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteLiterature.Moderation.Data.Configurations;

public sealed class AttachmentConfiguration : BaseEntityConfiguration<Attachment>
{
    public override void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable(ModerationApiTables.Attachments);
        builder.HasKey(x => x.Id).HasName($"{ModerationApiTables.Attachments}_pkey");

        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.DraftId).HasColumnName($"{ModerationApiTables.Drafts}_id").IsRequired();
        builder.Property(x => x.FileId).HasColumnName("file_id").IsRequired();
        builder.Property(x => x.AttachmentTypeId).HasColumnName("attachment_types_id").IsRequired();
    }
}