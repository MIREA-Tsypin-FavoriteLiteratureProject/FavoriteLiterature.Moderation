using Microsoft.AspNetCore.Authorization;
namespace FavoriteLiterature.Moderation.Application.Policies;

public class MinimumRoleRequirement : IAuthorizationRequirement
{
    public string RoleName { get; init; }

    public MinimumRoleRequirement(string roleName)
    {
        RoleName = roleName;
    }
}