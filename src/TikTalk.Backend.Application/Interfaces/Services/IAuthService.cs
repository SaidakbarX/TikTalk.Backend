using TikTalk.Backend.Application.Dtos;

namespace TikTalk.Backend.Application.Interfaces.Services;

public interface IAuthService
{
    Task<long> SignUpUserAsync(CreateUserDto dto);
    Task<LoginResponseDto> LoginUserAsync(UserLoginDto dto);
    Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
    Task LogOutAsync(string refreshToken);
}

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(long id);
    Task<UserWithRolesDto?> GetWithRolesAsync(long id);
    Task<UserDto> CreateAsync(CreateUserDto dto);
    Task UpdateProfileAsync(long id, string? fullName, string? avatarUrl);
}

public interface IVideoService
{
    Task<long> CreateAsync(CreateVideoDto dto, long userId);
    Task<List<VideoDto>> GetAllAsync();
    Task<VideoDetailedDto> GetByIdAsync(long id, long currentUserId);
    Task UpdateAsync(long id, UpdateVideoDto dto, long userId);
}

public interface IFollowService
{
    Task FollowAsync(long followerId, long followeeId);
    Task UnfollowAsync(long followerId, long followeeId);
    Task<bool> IsFollowingAsync(long followerId, long followeeId);
    Task<List<FollowInfoDto>> GetFollowersAsync(long userId);
    Task<List<FollowInfoDto>> GetFollowingAsync(long userId);
}

public interface ILikeService
{
    Task LikeVideoAsync(long videoId, long userId);
    Task UnlikeVideoAsync(long videoId, long userId);
    Task LikeCommentAsync(long commentId, long userId);
    Task UnlikeCommentAsync(long commentId, long userId);
}

public interface IVideoCommentService
{
    Task<List<VideoCommentDto>> GetCommentsAsync(long videoId, long currentUserId);
    Task CreateAsync(CreateVideoCommentDto dto, long userId);
    Task DeleteAsync(long commentId, long userId);
}

public interface IRepostService
{
    Task RepostAsync(long userId, long videoId);
    Task UnrepostAsync(long userId, long videoId);
    Task<bool> IsRepostedAsync(long userId, long videoId);
    Task<int> CountByVideoIdAsync(long videoId);
}

public interface INotificationService
{
    Task<List<NotificationDto>> GetNotificationsAsync(long userId);
    Task AddAsync(long userId, string title, string message);
    Task MarkAsReadAsync(long id);
    Task DeleteAsync(long id);
}

public interface IAnalyticsService
{
    Task<VideoAnalyticsDto> GetAnalyticsAsync(long videoId);
}