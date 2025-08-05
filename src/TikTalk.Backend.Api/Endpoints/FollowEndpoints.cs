using System.Security.Claims;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Api.Endpoints;

public static class FollowEndpoints
{
    public static void MapFollowEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users/{id:long}/follow", async (long id, IFollowService followService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            await followService.FollowAsync(userId, id);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("FollowUser")
        .WithOpenApi();

        app.MapDelete("/api/users/{id:long}/follow", async (long id, IFollowService followService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            await followService.UnfollowAsync(userId, id);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("UnfollowUser")
        .WithOpenApi();
    }
}