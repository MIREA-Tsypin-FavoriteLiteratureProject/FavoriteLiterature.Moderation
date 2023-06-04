using FavoriteLiterature.Moderation.Application.Options;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class AttachmentStorageExtensions
{
    public static void AddAttachmentStorage(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AttachmentStorageOptions>(builder.Configuration.GetSection("AttachmentStorage"));
    }
}