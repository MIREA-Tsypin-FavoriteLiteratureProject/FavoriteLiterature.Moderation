using Microsoft.EntityFrameworkCore;

namespace FavoriteLiterature.Moderation.Data.Common;

public static class DatabaseInitializer
{
    public static void Initialize(FavoriteLiteratureModerationDbContext context)
    {
        if (context.Database.CanConnect())
        {
            context.Database.Migrate();
        }
    }
}