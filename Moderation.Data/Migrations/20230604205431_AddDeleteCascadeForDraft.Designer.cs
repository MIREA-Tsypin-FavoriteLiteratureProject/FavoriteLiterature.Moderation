﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FavoriteLiterature.Moderation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FavoriteLiterature.Moderation.Data.Migrations
{
    [DbContext(typeof(FavoriteLiteratureModerationDbContext))]
    [Migration("20230604205431_AddDeleteCascadeForDraft")]
    partial class AddDeleteCascadeForDraft
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FavoriteLiterature.Moderation.Data.Entities.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AttachmentTypeId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("attachment_types_id");

                    b.Property<Guid>("DraftId")
                        .HasColumnType("uuid")
                        .HasColumnName("drafts_id");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid")
                        .HasColumnName("file_id");

                    b.HasKey("Id")
                        .HasName("attachments_pkey");

                    b.HasIndex("DraftId");

                    b.ToTable("attachments", (string)null);
                });

            modelBuilder.Entity("FavoriteLiterature.Moderation.Data.Entities.Draft", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<List<Guid>>("Authors")
                        .IsRequired()
                        .HasColumnType("uuid[]")
                        .HasColumnName("authors");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<List<Guid>>("Genres")
                        .IsRequired()
                        .HasColumnType("uuid[]")
                        .HasColumnName("genres");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("drafts_pkey");

                    b.ToTable("drafts", (string)null);
                });

            modelBuilder.Entity("FavoriteLiterature.Moderation.Data.Entities.Attachment", b =>
                {
                    b.HasOne("FavoriteLiterature.Moderation.Data.Entities.Draft", "Draft")
                        .WithMany("Attachments")
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Draft");
                });

            modelBuilder.Entity("FavoriteLiterature.Moderation.Data.Entities.Draft", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
