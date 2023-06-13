using FavoriteLiterature.Moderation.Data;
using FavoriteLiterature.Moderation.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FavoriteLiterature.Moderation.Extensions.Builder;

public static class DatabaseExtensions
{
    public static void AddPostgresDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Connection string is missing.");
        }

        builder.Services.AddDbContext<FavoriteLiteratureModerationDbContext>(options => options.UseNpgsql(connectionString));
    }

    public static void SeedDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var mainContext = serviceScope.ServiceProvider.GetRequiredService<FavoriteLiteratureModerationDbContext>();

        DatabaseInitializer.Initialize(mainContext);
    }
}