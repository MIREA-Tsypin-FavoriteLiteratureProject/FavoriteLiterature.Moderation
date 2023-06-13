using FavoriteLiterature.Moderation.Application.Handlers.Common.Policies;
using FavoriteLiterature.Moderation.Application.Policies;
using Microsoft.AspNetCore.Authorization;

namespace FavoriteLiterature.Moderation.Extensions.Builder.Common;

public static class AuthorizationExtensions
{
    public static void AddRolePolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthorizationHandler, MinimumRoleRequirementHandler>();

        builder.Services.AddAuthorization(options =>
        {
            // Настройка политик доступа для роли - Автор.
            options.AddPolicy(nameof(RolePolicy.Author), policy =>
                policy.Requirements.Add(new MinimumRoleRequirement("author")));

            // Настройка политик доступа для роли - Критик.
            options.AddPolicy(nameof(RolePolicy.Critic), policy =>
                policy.Requirements.Add(new MinimumRoleRequirement("critic")));
        });
    }
}