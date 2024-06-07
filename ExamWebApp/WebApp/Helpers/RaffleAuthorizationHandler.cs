using System.Security.Claims;
using App.DAL.Contracts;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Helpers;

public class RaffleAuthorizationHandler(IAppUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<RaffleAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RaffleAuthorizationRequirement requirement)
    {
        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            context.Fail();
            return;
        }

        // Extract the RaffleId from the route data
        HttpContext? httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            context.Fail();
            return;
        }

        var routeData = httpContext.Request.RouteValues;
        if (!routeData.TryGetValue("id", out var raffleIdValue) || !Guid.TryParse(raffleIdValue?.ToString(), out var raffleId))
        {
            // context.Fail();
            context.Succeed(requirement);
            return;
        }

        // Load the raffle from the database
        var raffle = await unitOfWork.Raffles.FindAsync(raffleId);
        if (raffle == null)
        {
            context.Fail();
            return;
        }

        if (raffle.VisibleToPublic)
        {
            context.Succeed(requirement);
            return;
        }

        // Check if the user is an admin
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return;
        }

        // Check if the user is part of the company that hosts the raffle
        var user = await userManager.FindByIdAsync(userId);
        if (user.CompanyId == raffle.CompanyId)
        {
            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}
