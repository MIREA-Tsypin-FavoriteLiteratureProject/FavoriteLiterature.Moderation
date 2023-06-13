using FavoriteLiterature.Moderation.Middleware;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class CustomMiddlewaresExtensions
{
    public static void AddCustomMiddlewares(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ExceptionHandlingMiddleware>();
    }
}