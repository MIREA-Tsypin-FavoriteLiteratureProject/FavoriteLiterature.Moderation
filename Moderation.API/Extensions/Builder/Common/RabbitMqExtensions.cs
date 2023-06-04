using FavoriteLiterature.Moderation.Application.Options;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class RabbitMqExtensions
{
    public static void AddRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
    }
}