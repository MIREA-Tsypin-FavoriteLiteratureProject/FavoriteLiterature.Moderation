using FavoriteLiterature.Moderation.Data.Repositories;
using FavoriteLiterature.Moderation.Data.Repositories.Attachments;
using FavoriteLiterature.Moderation.Data.Repositories.Drafts;

namespace FavoriteLiterature.Moderation.Extensions.Builder;

public static class RepositoryExtensions
{
    public static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
        builder.Services.AddScoped<IDraftsRepository, DraftsRepository>();
        
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}