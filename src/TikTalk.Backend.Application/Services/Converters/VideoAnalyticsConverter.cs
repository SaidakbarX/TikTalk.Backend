using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services.Converters;

public static class VideoAnalyticsConverter
{
    public static VideoAnalyticsDto ToDto(VideoAnalytics analytics) => new()
    {
        VideoId = analytics.VideoId,
        ViewCount = analytics.ViewCount,
        LikeCount = analytics.LikeCount,
        CommentCount = analytics.CommentCount,
        UpdatedAt = analytics.UpdatedAt
    };
}
