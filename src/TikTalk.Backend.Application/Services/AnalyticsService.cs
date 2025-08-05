using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;

namespace TikTalk.Backend.Application.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly IVideoViewRepository _viewRepository;
    private readonly IVideoLikeRepository _likeRepository;
    private readonly IVideoCommentRepository _commentRepository;
    private readonly IRepostRepository _repostRepository;

    public AnalyticsService(
        IVideoViewRepository viewRepository,
        IVideoLikeRepository likeRepository,
        IVideoCommentRepository commentRepository,
        IRepostRepository repostRepository)
    {
        _viewRepository = viewRepository;
        _likeRepository = likeRepository;
        _commentRepository = commentRepository;
        _repostRepository = repostRepository;
    }

    public async Task<VideoAnalyticsDto> GetAnalyticsAsync(long videoId)
    {
        var viewCount = await _viewRepository.CountByVideoIdAsync(videoId);
        var likeCount = await _likeRepository.CountByVideoIdAsync(videoId);
        var commentCount = await _commentRepository.CountByVideoIdAsync(videoId);
        var repostCount = await _repostRepository.CountByVideoIdAsync(videoId);

        return new VideoAnalyticsDto
        {
            VideoId = videoId,
            ViewCount = viewCount,
            LikeCount = likeCount,
            CommentCount = commentCount + repostCount,
            UpdatedAt = DateTime.UtcNow
        };
    }
}