using System.Security.Claims;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Api.Endpoints;

public static class NotificationEndpoints
{
    public static void MapNotificationEndpoints(this WebApplication app)
    {
        app.MapGet("/api/notifications", async (INotificationService notificationService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var notifications = await notificationService.GetNotificationsAsync(userId);
            return Results.Ok(notifications);
        })
        .RequireAuthorization()
        .WithName("GetNotifications")
        .WithOpenApi();

        app.MapPut("/api/notifications/{id:long}/read", async (long id, INotificationService notificationService) =>
        {
            await notificationService.MarkAsReadAsync(id);
            return Results.Ok();
        })
        .RequireAuthorization()
        .WithName("MarkNotificationAsRead")
        .WithOpenApi();
    }
}
