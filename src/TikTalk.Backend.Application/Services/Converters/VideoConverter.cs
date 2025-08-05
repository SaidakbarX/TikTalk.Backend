using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services.Converters;

public static class VideoConverter
{
    public static VideoDto ToDto(Video video) => new()
    {
        Id = video.Id,
        Title = video.Title,
        ThumbnailUrl = video.ThumbnailUrl ?? "",
        CreatedAt = video.UploadedAt,
        User = UserConverter.ToDto(video.Owner)
    };

    public static VideoDetailedDto ToDetailedDto(Video video, List<string> hashtags, int likeCount, int commentCount, int viewCount, bool isLiked, bool isReposted) => new()
    {
        Id = video.Id,
        Title = video.Title,
        Description = "", // Agar mavjud bo‘lsa alohida field qo‘shing
        VideoUrl = video.VideoUrl,
        ThumbnailUrl = video.ThumbnailUrl ?? "",
        CreatedAt = video.UploadedAt,
        User = UserConverter.ToDto(video.Owner),
        Hashtags = hashtags,
        LikeCount = likeCount,
        CommentCount = commentCount,
        ViewCount = viewCount,
        IsLikedByCurrentUser = isLiked,
        IsRepostedByCurrentUser = isReposted
    };
}