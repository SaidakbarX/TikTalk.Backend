using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services;

public class VideoCommentService : IVideoCommentService
{
    private readonly IVideoCommentRepository _commentRepository;
    private readonly ICommentLikeRepository _likeRepository;

    public VideoCommentService(
        IVideoCommentRepository commentRepository,
        ICommentLikeRepository likeRepository)
    {
        _commentRepository = commentRepository;
        _likeRepository = likeRepository;
    }

    public async Task<List<VideoCommentDto>> GetCommentsAsync(long videoId, long currentUserId)
    {
        var comments = await _commentRepository.GetByVideoIdAsync(videoId);
        var topLevelComments = comments.Where(c => c.ParentCommentId == null).ToList();

        var result = new List<VideoCommentDto>();

        foreach (var comment in topLevelComments)
        {
            var dto = await MapToDtoAsync(comment, currentUserId);
            result.Add(dto);
        }

        return result;
    }

    public async Task CreateAsync(CreateVideoCommentDto dto, long userId)
    {
        var comment = new VideoComment
        {
            VideoId = dto.VideoId,
            UserId = userId,
            Text = dto.Text,
            ParentCommentId = dto.ParentCommentId,
            CreatedAt = DateTime.UtcNow
        };

        await _commentRepository.AddAsync(comment);
    }

    public async Task DeleteAsync(long commentId, long userId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment is null || comment.UserId != userId) return;

        _commentRepository.Delete(comment);
    }

    private async Task<VideoCommentDto> MapToDtoAsync(VideoComment comment, long currentUserId)
    {
        var likeCount = await _likeRepository.CountByCommentIdAsync(comment.Id);
        var isLiked = await _likeRepository.IsLikedAsync(comment.Id, currentUserId);

        var dto = new VideoCommentDto
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            LikeCount = likeCount,
            IsLikedByCurrentUser = isLiked,
            User = new UserDto
            {
                Id = comment.User.Id,
                FullName = comment.User.FullName,
                Username = comment.User.FullName,
                AvatarUrl = comment.User.ProfileImageUrl ?? ""
            },
            Replies = new List<VideoCommentDto>()
        };

        var replies = await _commentRepository.GetRepliesAsync(comment.Id);
        foreach (var reply in replies)
        {
            var replyDto = await MapToDtoAsync(reply, currentUserId);
            dto.Replies.Add(replyDto);
        }

        return dto;
    }
}