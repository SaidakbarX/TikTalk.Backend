using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services.Converters;

public static class VideoCommentConverter
{
    public static VideoCommentDto ToDto(VideoComment comment, int likeCount, bool isLiked, List<VideoCommentDto> replies) => new()
    {
        Id = comment.Id,
        Text = comment.Text,
        CreatedAt = comment.CreatedAt,
        LikeCount = likeCount,
        IsLikedByCurrentUser = isLiked,
        User = UserConverter.ToDto(comment.User),
        Replies = replies
    };
}