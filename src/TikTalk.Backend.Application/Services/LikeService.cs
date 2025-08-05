using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services;

public class LikeService : ILikeService
{
    private readonly IVideoLikeRepository _videoLikeRepository;
    private readonly ICommentLikeRepository _commentLikeRepository;

    public LikeService(
        IVideoLikeRepository videoLikeRepository,
        ICommentLikeRepository commentLikeRepository)
    {
        _videoLikeRepository = videoLikeRepository;
        _commentLikeRepository = commentLikeRepository;
    }

    public async Task LikeVideoAsync(long videoId, long userId)
    {
        var alreadyLiked = await _videoLikeRepository.IsLikedAsync(videoId, userId);
        if (!alreadyLiked)
        {
            var like = new VideoLike
            {
                VideoId = videoId,
                UserId = userId,
                LikedAt = DateTime.UtcNow
            };
            await _videoLikeRepository.AddAsync(like);
        }
    }

    public async Task UnlikeVideoAsync(long videoId, long userId)
    {
        var alreadyLiked = await _videoLikeRepository.IsLikedAsync(videoId, userId);
        if (alreadyLiked)
        {
            await _videoLikeRepository.DeleteAsync(videoId, userId);
        }
    }

    public async Task LikeCommentAsync(long commentId, long userId)
    {
        var alreadyLiked = await _commentLikeRepository.IsLikedAsync(commentId, userId);
        if (!alreadyLiked)
        {
            var like = new CommentLike
            {
                CommentId = commentId,
                UserId = userId,
                LikedAt = DateTime.UtcNow
            };
            await _commentLikeRepository.AddAsync(like);
        }
    }

    public async Task UnlikeCommentAsync(long commentId, long userId)
    {
        var alreadyLiked = await _commentLikeRepository.IsLikedAsync(commentId, userId);
        if (alreadyLiked)
        {
            await _commentLikeRepository.DeleteAsync(commentId, userId);
        }
    }
}