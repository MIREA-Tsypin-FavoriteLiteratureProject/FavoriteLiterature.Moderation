using Microsoft.OpenApi.Models;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class SwaggerExtensions
{
    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            // Дополнительная информация для генерации документации.
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Favorite literature Moderation.API",
                Description = "Сервис по работе с начинающими писателями. Микросервис по работе с черновиками.",
                Contact = new OpenApiContact
                {
                    Name = "Цыпин Илья Павлович",
                    Email = "tsypin.i.p@mail.ru",
                    Url = new Uri("https://t.me/Dedicated407"),
                },
            });

            // Добавление описания к Swagger о том, как защищен API.
            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Пожалуйста, введите в поле слово 'Bearer', за которым следует пробел и JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            // Добавление глобальной безопасности.
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme,
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}