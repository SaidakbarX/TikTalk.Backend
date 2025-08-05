using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Interfaces.Repositories;

public interface IVideoRepository
{
    Task<Video?> GetByIdAsync(long id);
    Task<List<Video>> GetByUserIdAsync(long userId);
    Task<List<Video>> GetTrendingVideosAsync(int count);
    Task<List<Video>> GetVideosByHashtagAsync(string hashtag);
    Task AddAsync(Video video);
    void Update(Video video);
    void Delete(Video video);
    Task SaveChangesAsync();
}

public interface IFollowRepository
{
    Task<bool> IsFollowingAsync(long followerId, long followeeId);
    Task<List<long>> GetFollowersIdsAsync(long userId);
    Task<List<long>> GetFolloweesIdsAsync(long userId);
    Task AddAsync(Follow follow);
    Task DeleteAsync(long followerId, long followeeId);
    Task SaveChangesAsync();
}

public interface IVideoLikeRepository
{
    Task<bool> IsLikedAsync(long videoId, long userId);
    Task<int> CountByVideoIdAsync(long videoId);
    Task AddAsync(VideoLike like);
    Task DeleteAsync(long videoId, long userId);
    Task SaveChangesAsync();
}

public interface ICommentLikeRepository
{
    Task<bool> IsLikedAsync(long commentId, long userId);
    Task<int> CountByCommentIdAsync(long commentId);
    Task AddAsync(CommentLike like);
    Task DeleteAsync(long commentId, long userId);
    Task SaveChangesAsync();
}

public interface IVideoCommentRepository
{
    Task<VideoComment?> GetByIdAsync(long id);
    Task<List<VideoComment>> GetByVideoIdAsync(long videoId);
    Task<List<VideoComment>> GetRepliesAsync(long parentCommentId);
    Task<int> CountByVideoIdAsync(long videoId);
    Task AddAsync(VideoComment comment);
    void Delete(VideoComment comment);
    Task SaveChangesAsync();
}

public interface IVideoViewRepository
{
    Task<int> CountByVideoIdAsync(long videoId);
    Task<bool> HasViewedAsync(long videoId, long userId);
    Task AddAsync(VideoView view);
    Task<List<VideoView>> GetByVideoIdAsync(long videoId);
    Task SaveChangesAsync();
}

public interface IHashtagRepository
{
    Task<Hashtag?> GetByNameAsync(string name);
    Task<List<Hashtag>> GetTrendingAsync(int count);
    Task AddAsync(Hashtag hashtag);
    Task SaveChangesAsync();
}

public interface IVideoHashtagRepository
{
    Task<List<string>> GetHashtagsByVideoIdAsync(long videoId);
    Task AddAsync(VideoHashtag videoHashtag);
    Task DeleteByVideoIdAsync(long videoId);
    Task SaveChangesAsync();
}

public interface IRepostRepository
{
    Task<bool> IsRepostedAsync(long videoId, long userId);
    Task<int> CountByVideoIdAsync(long videoId);
    Task<List<Repost>> GetByUserIdAsync(long userId);
    Task AddAsync(Repost repost);
    Task DeleteAsync(long videoId, long userId);
    Task SaveChangesAsync();
}

public interface INotificationRepository
{
    Task<List<Notification>> GetByUserIdAsync(long userId);
    Task AddAsync(Notification notification);
    Task MarkAsReadAsync(long id);
    Task DeleteAsync(long id);
    Task SaveChangesAsync();
}

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateAsync(RefreshToken token);
    Task<RefreshToken?> GetByTokenAsync(string token, long userId);
    Task DeleteAsync(string refreshToken);
    Task DeleteAllByUserIdAsync(long userId);
    Task SaveChangesAsync();
}

public interface IBlockedUserRepository
{
    Task<bool> IsBlockedAsync(long blockerId, long blockedId);
    Task<List<long>> GetBlockedUserIdsAsync(long blockerId);
    Task AddAsync(BlockedUser blockedUser);
    Task DeleteAsync(long blockerId, long blockedId);
    Task SaveChangesAsync();
}

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(long id);
    Task<Role?> GetByNameAsync(string name);
    Task<List<Role>> GetAllAsync();
    Task AddAsync(Role role);
    void Update(Role role);
    void Delete(Role role);
    Task SaveChangesAsync();
}

public interface IUserRoleRepository
{
    Task<List<string>> GetRoleNamesByUserIdAsync(long userId);
    Task<List<UserRole>> GetByUserIdAsync(long userId);
    Task AddAsync(UserRole userRole);
    Task DeleteAsync(long userId, long roleId);
    Task SaveChangesAsync();
}

public interface IUserLoginRepository
{
    Task AddAsync(UserLogin login);
    Task<List<UserLogin>> GetByUserIdAsync(long userId);
    Task SaveChangesAsync();
}

public interface IMessageRepository
{
    Task<List<Message>> GetMessagesAsync(long senderId, long receiverId);
    Task<Message?> GetByIdAsync(long id);
    Task AddAsync(Message message);
    Task DeleteAsync(long id, long senderId);
    Task SaveChangesAsync();
}

public interface IVideoAnalyticsRepository
{
    Task<VideoAnalytics?> GetByVideoIdAsync(long videoId);
    Task AddAsync(VideoAnalytics analytics);
    void Update(VideoAnalytics analytics);
    Task SaveChangesAsync();
}