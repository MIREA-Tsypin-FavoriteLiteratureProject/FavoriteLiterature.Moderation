namespace FavoriteLiterature.Moderation.Extensions;

public static class RouteExtensions
{
    public static void AddNormalizeRoute(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(options => 
        { 
            options.LowercaseUrls = true; 
            options.LowercaseQueryStrings = true;
        });
    }
}