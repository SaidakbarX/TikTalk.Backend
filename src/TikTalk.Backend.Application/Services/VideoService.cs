using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;
using TikTalk.Backend.Application.Services.Converters;
using TikTalk.Backend.Domain.Entities;
using TikTalk.Backend.Core.Exceptions;

namespace TikTalk.Backend.Application.Services;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;
    private readonly IHashtagRepository _hashtagRepository;
    private readonly IVideoHashtagRepository _videoHashtagRepository;
    private readonly IVideoViewRepository _viewRepository;
    private readonly IVideoLikeRepository _likeRepository;
    private readonly IVideoCommentRepository _commentRepository;
    private readonly IRepostRepository _repostRepository;

    public VideoService(
        IVideoRepository videoRepository,
        IHashtagRepository hashtagRepository,
        IVideoHashtagRepository videoHashtagRepository,
        IVideoViewRepository viewRepository,
        IVideoLikeRepository likeRepository,
        IVideoCommentRepository commentRepository,
        IRepostRepository repostRepository)
    {
        _videoRepository = videoRepository;
        _hashtagRepository = hashtagRepository;
        _videoHashtagRepository = videoHashtagRepository;
        _viewRepository = viewRepository;
        _likeRepository = likeRepository;
        _commentRepository = commentRepository;
        _repostRepository = repostRepository;
    }

    public async Task<long> CreateAsync(CreateVideoDto dto, long userId)
    {
        var video = new Video
        {
            Title = dto.Title,
            VideoUrl = dto.VideoUrl,
            ThumbnailUrl = dto.ThumbnailUrl,
            Duration = TimeSpan.Zero,
            UploadedAt = DateTime.UtcNow,
            OwnerId = userId
        };

        await _videoRepository.AddAsync(video);

        foreach (var tag in dto.Hashtags.Distinct())
        {
            var existing = await _hashtagRepository.GetByNameAsync(tag);
            if (existing is null)
            {
                existing = new Hashtag { Name = tag };
                await _hashtagRepository.AddAsync(existing);
            }

            await _videoHashtagRepository.AddAsync(new VideoHashtag
            {
                VideoId = video.Id,
                HashtagId = existing.Id
            });
        }

        return video.Id;
    }

    public async Task<List<VideoDto>> GetAllAsync()
    {
        var videos = await _videoRepository.GetTrendingVideosAsync(20);

        return videos.Select(VideoConverter.ToDto).ToList();
    }

    public async Task<VideoDetailedDto?> GetByIdAsync(long id, long currentUserId)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        if (video is null || video.Owner is null) 
            return null;

        var hashtags = await _videoHashtagRepository.GetHashtagsByVideoIdAsync(id);
        var likeCount = await _likeRepository.CountByVideoIdAsync(id);
        var commentCount = await _commentRepository.CountByVideoIdAsync(id);
        var viewCount = await _viewRepository.CountByVideoIdAsync(id);
        var isLiked = currentUserId > 0 ? await _likeRepository.IsLikedAsync(id, currentUserId) : false;
        var isReposted = currentUserId > 0 ? await _repostRepository.IsRepostedAsync(id, currentUserId) : false;

        return VideoConverter.ToDetailedDto(video, hashtags, likeCount, commentCount, viewCount, isLiked, isReposted);
    }

    public async Task UpdateAsync(long id, UpdateVideoDto dto, long userId)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        if (video is null) 
            throw new EntityNotFoundException("Video", id);
            
        if (video.OwnerId != userId)
            throw new ForbiddenException("You can only update your own videos");

        if (!string.IsNullOrWhiteSpace(dto.Title))
            video.Title = dto.Title;

        if (!string.IsNullOrWhiteSpace(dto.ThumbnailUrl))
            video.ThumbnailUrl = dto.ThumbnailUrl;

        _videoRepository.Update(video);
        await _videoRepository.SaveChangesAsync();
    }
}