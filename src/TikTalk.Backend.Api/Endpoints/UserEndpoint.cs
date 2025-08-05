using System.Security.Claims;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Api.Endpoints;

public static class UserEndpoint
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/api/users/{id:long}", async (long id, IUserService userService) =>
        {
            var user = await userService.GetByIdAsync(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        })
        .WithName("GetUser")
        .WithOpenApi();

        app.MapPut("/api/users/profile", async (UpdateUserDto dto, IUserService userService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            await userService.UpdateProfileAsync(userId, dto.FullName, dto.AvatarUrl);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("UpdateProfile")
        .WithOpenApi();

        return app;
    }
}
