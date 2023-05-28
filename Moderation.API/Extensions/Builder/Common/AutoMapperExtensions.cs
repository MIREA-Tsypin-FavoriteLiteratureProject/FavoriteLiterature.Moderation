using AutoMapper;
using FavoriteLiterature.Moderation.Application.Mapping;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class AutoMapperExtensions
{
    public static void AddAutoMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMapper>(_ =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DraftProfile());
            });

            return config.CreateMapper();
        });
    }
}