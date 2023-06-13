using System.Security;
using System.Security.Claims;
using FavoriteLiterature.Moderation.Application.Policies;
using Microsoft.AspNetCore.Authorization;

namespace FavoriteLiterature.Moderation.Application.Handlers.Common.Policies;

public sealed class MinimumRoleRequirementHandler  : AuthorizationHandler<MinimumRoleRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRoleRequirement requirement)
    {
        var roleName = context.User.FindFirst(claim => claim.Type == ClaimTypes.Role)?.Value;
        
        if (Enum.TryParse<RolePolicy>(roleName, true, out var currentWeight) &&
            Enum.TryParse<RolePolicy>(requirement.RoleName, true, out var requirementWeight))
        {
            if (currentWeight > requirementWeight)
            {
                throw new SecurityException("No access!");
            }

            context.Succeed(requirement);
        }
        else
        {
            throw new SecurityException("No access!");
        }
    }
}