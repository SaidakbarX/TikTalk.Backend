using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Api.Endpoints;

public static class VideoEndpoints
{
    public static void MapVideoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/videos")
            .WithTags("Video");

        group.MapGet("/", async (IVideoService videoService) =>
        {
            var videos = await videoService.GetAllAsync();
            return Results.Ok(videos);
        })
        .WithName("GetVideos")
        .WithOpenApi();

        group.MapGet("/{id:long}", async (long id, IVideoService videoService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst("Id")?.Value;
            var currentUserId = long.TryParse(userIdClaim, out var uid) ? uid : 0;

            var video = await videoService.GetByIdAsync(id, currentUserId);
            return video is not null ? Results.Ok(video) : Results.NotFound();
        })
        .WithName("GetVideo")
        .WithOpenApi();

        group.MapPost("/upload", async (
        HttpContext httpContext,
        [FromForm] VideoUploadDto videoUploadDto,
        IFileStorageService storageService,
        IVideoService videoService) =>
        {
            var userIdClaim = httpContext.User.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            if (videoUploadDto.File == null || videoUploadDto.File.Length == 0)
                return Results.BadRequest("Video file is missing");

            if (videoUploadDto.Thumbnail == null || videoUploadDto.Thumbnail.Length == 0)
                return Results.BadRequest("Thumbnail is missing");

            var videoFileName = $"{Guid.NewGuid()}{Path.GetExtension(videoUploadDto.File.FileName)}";
            var thumbnailFileName = $"{Guid.NewGuid()}{Path.GetExtension(videoUploadDto.Thumbnail.FileName)}";

            await using var videoStream = videoUploadDto.File.OpenReadStream();
            var videoPath = await storageService.SaveFileAsync(videoStream, "videos", videoFileName);

            await using var thumbStream = videoUploadDto.Thumbnail.OpenReadStream();
            var thumbPath = await storageService.SaveFileAsync(thumbStream, "thumbnails", thumbnailFileName);

            var createDto = new CreateVideoDto
            {
                Title = videoUploadDto.Title,
                Description = videoUploadDto.Description,
                VideoUrl = videoPath,
                ThumbnailUrl = thumbPath,
                Hashtags = new List<string>()
            };

            var videoId = await videoService.CreateAsync(createDto, userId);
            var videoPathh = await storageService.SaveFileAsync(videoUploadDto.File.OpenReadStream(), "videos", videoUploadDto.File.FileName);
            var thumbPathh = await storageService.SaveFileAsync(videoUploadDto.Thumbnail.OpenReadStream(), "thumbnails", videoUploadDto.Thumbnail.FileName);


            return Results.Ok(new { VideoId = videoId, VideoUrl = videoPath, ThumbnailUrl = thumbPath });
        })
        .DisableAntiforgery()
        .WithMetadata(new ConsumesAttribute("multipart/form-data"))
        .WithName("CreateVideoWithFile");

        group.MapPost("/", async (
            CreateVideoDto dto,
            IVideoService videoService,
            IValidator<CreateVideoDto> validator,
            ClaimsPrincipal user) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var userIdClaim = user.FindFirst("Id")?.Value;
            if (!long.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var videoId = await videoService.CreateAsync(dto, userId);
            return Results.Ok(new { VideoId = videoId });
        })
        .RequireAuthorization()
        .WithName("CreateVideo")
        .WithOpenApi();
    }
}
