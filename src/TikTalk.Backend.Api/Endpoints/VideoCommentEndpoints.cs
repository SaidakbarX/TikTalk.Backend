using FluentValidation;
using System.Security.Claims;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Api.Endpoints;



    public static class VideoCommentEndpoints
    {
        public static void MapVideoCommentEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/videos/{id:long}/comments", async (long id, IVideoCommentService commentService, ClaimsPrincipal user) =>
            {
                var userIdClaim = user.FindFirst("Id")?.Value;
                var currentUserId = long.TryParse(userIdClaim, out var uid) ? uid : 0;

                var comments = await commentService.GetCommentsAsync(id, currentUserId);
                return Results.Ok(comments);
            })
            .WithName("GetComments")
            .WithOpenApi();

            app.MapPost("/api/videos/{id:long}/comments", async (long id, CreateVideoCommentDto dto, IVideoCommentService commentService, IValidator<CreateVideoCommentDto> validator, ClaimsPrincipal user) =>
            {
                dto.VideoId = id;

                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);

                var userIdClaim = user.FindFirst("Id")?.Value;
                if (!long.TryParse(userIdClaim, out var userId))
                    return Results.Unauthorized();

                await commentService.CreateAsync(dto, userId);
                return Results.Ok();
            })
            .RequireAuthorization()
            .WithName("CreateComment")
            .WithOpenApi();
        }
    }

