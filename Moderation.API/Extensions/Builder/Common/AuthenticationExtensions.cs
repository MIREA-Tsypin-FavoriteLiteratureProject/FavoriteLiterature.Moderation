using System.Text;
using FavoriteLiterature.Moderation.Application.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class AuthenticationExtensions
{
    public static AuthenticationBuilder AddJwtBearerAuthentication(this AuthenticationBuilder builder, 
        IConfiguration configuration)
    {
        builder.Services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        builder.AddJwtBearer(options => 
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions!.Issuer,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return builder;
    }
}