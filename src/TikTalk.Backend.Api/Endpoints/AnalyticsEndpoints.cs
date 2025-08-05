using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Api.Endpoints;

public static class AnalyticsEndpoints
{
    public static void MapAnalyticsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/videos/{id:long}/analytics", async (long id, IAnalyticsService analyticsService) =>
        {
            var analytics = await analyticsService.GetAnalyticsAsync(id);
            return Results.Ok(analytics);
        })
        .WithName("GetVideoAnalytics")
        .WithOpenApi();
    }
}
