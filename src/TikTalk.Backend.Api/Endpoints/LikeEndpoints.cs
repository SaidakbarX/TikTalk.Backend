using System.Security.Claims;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Api.Endpoints;

public static class LikeEndpoints
{
    public static void MapLikeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/videos/{id:long}/like", async (long id, ILikeService likeService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            await likeService.LikeVideoAsync(id, userId);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("LikeVideo")
        .WithOpenApi();

        app.MapDelete("/api/videos/{id:long}/like", async (long id, ILikeService likeService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            await likeService.UnlikeVideoAsync(id, userId);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("UnlikeVideo")
        .WithOpenApi();
    }
}
