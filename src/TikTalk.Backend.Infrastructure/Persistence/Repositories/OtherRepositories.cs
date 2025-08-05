using Microsoft.EntityFrameworkCore;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Domain.Entities;
using TikTalk.Backend.Infrastructure.Persistence.DbContexts;

namespace TikTalk.Backend.Infrastructure.Persistence.Repositories;

public class VideoLikeRepository : IVideoLikeRepository
{
    private readonly TikTalkDbContext _context;

    public VideoLikeRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsLikedAsync(long videoId, long userId)
    {
        return await _context.VideoLikes
            .AnyAsync(l => l.VideoId == videoId && l.UserId == userId);
    }

    public async Task<int> CountByVideoIdAsync(long videoId)
    {
        return await _context.VideoLikes
            .CountAsync(l => l.VideoId == videoId);
    }

    public async Task AddAsync(VideoLike like)
    {
        await _context.VideoLikes.AddAsync(like);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long videoId, long userId)
    {
        var like = await _context.VideoLikes
            .FirstOrDefaultAsync(l => l.VideoId == videoId && l.UserId == userId);

        if (like is not null)
        {
            _context.VideoLikes.Remove(like);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class CommentLikeRepository : ICommentLikeRepository
{
    private readonly TikTalkDbContext _context;

    public CommentLikeRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsLikedAsync(long commentId, long userId)
    {
        return await _context.CommentLikes
            .AnyAsync(l => l.CommentId == commentId && l.UserId == userId);
    }

    public async Task<int> CountByCommentIdAsync(long commentId)
    {
        return await _context.CommentLikes
            .CountAsync(l => l.CommentId == commentId);
    }

    public async Task AddAsync(CommentLike like)
    {
        await _context.CommentLikes.AddAsync(like);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long commentId, long userId)
    {
        var like = await _context.CommentLikes
            .FirstOrDefaultAsync(l => l.CommentId == commentId && l.UserId == userId);

        if (like is not null)
        {
            _context.CommentLikes.Remove(like);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class VideoCommentRepository : IVideoCommentRepository
{
    private readonly TikTalkDbContext _context;

    public VideoCommentRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<VideoComment?> GetByIdAsync(long id)
    {
        return await _context.VideoComments
            .Include(c => c.User)
            .Include(c => c.Replies)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<VideoComment>> GetByVideoIdAsync(long videoId)
    {
        return await _context.VideoComments
            .Where(c => c.VideoId == videoId && c.ParentCommentId == null)
            .Include(c => c.User)
            .Include(c => c.Likes)
            .Include(c => c.Replies)
            .ToListAsync();
    }

    public async Task<List<VideoComment>> GetRepliesAsync(long parentCommentId)
    {
        return await _context.VideoComments
            .Where(c => c.ParentCommentId == parentCommentId)
            .Include(c => c.User)
            .Include(c => c.Likes)
            .ToListAsync();
    }

    public async Task<int> CountByVideoIdAsync(long videoId)
    {
        return await _context.VideoComments
            .CountAsync(c => c.VideoId == videoId);
    }

    public async Task AddAsync(VideoComment comment)
    {
        await _context.VideoComments.AddAsync(comment);
        await SaveChangesAsync();
    }

    public void Delete(VideoComment comment)
    {
        _context.VideoComments.Remove(comment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class VideoViewRepository : IVideoViewRepository
{
    private readonly TikTalkDbContext _context;

    public VideoViewRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountByVideoIdAsync(long videoId)
    {
        return await _context.VideoViews
            .CountAsync(v => v.VideoId == videoId);
    }

    public async Task<bool> HasViewedAsync(long videoId, long userId)
    {
        return await _context.VideoViews
            .AnyAsync(v => v.VideoId == videoId && v.UserId == userId);
    }

    public async Task AddAsync(VideoView view)
    {
        await _context.VideoViews.AddAsync(view);
        await SaveChangesAsync();
    }

    public async Task<List<VideoView>> GetByVideoIdAsync(long videoId)
    {
        return await _context.VideoViews
            .Where(v => v.VideoId == videoId)
            .Include(v => v.User)
            .OrderByDescending(v => v.ViewedAt)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class HashtagRepository : IHashtagRepository
{
    private readonly TikTalkDbContext _context;

    public HashtagRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<Hashtag?> GetByNameAsync(string name)
    {
        return await _context.Hashtags
            .FirstOrDefaultAsync(h => h.Name == name);
    }

    public async Task<List<Hashtag>> GetTrendingAsync(int count)
    {
        return await _context.Hashtags
            .OrderByDescending(h => h.VideoHashtags.Count)
            .Take(count)
            .ToListAsync();
    }

    public async Task AddAsync(Hashtag hashtag)
    {
        await _context.Hashtags.AddAsync(hashtag);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class VideoHashtagRepository : IVideoHashtagRepository
{
    private readonly TikTalkDbContext _context;

    public VideoHashtagRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetHashtagsByVideoIdAsync(long videoId)
    {
        return await _context.VideoHashtags
            .Where(vh => vh.VideoId == videoId)
            .Include(vh => vh.Hashtag)
            .Select(vh => vh.Hashtag.Name)
            .ToListAsync();
    }

    public async Task AddAsync(VideoHashtag videoHashtag)
    {
        await _context.VideoHashtags.AddAsync(videoHashtag);
        await SaveChangesAsync();
    }

    public async Task DeleteByVideoIdAsync(long videoId)
    {
        var tags = await _context.VideoHashtags
            .Where(vh => vh.VideoId == videoId)
            .ToListAsync();

        _context.VideoHashtags.RemoveRange(tags);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class RepostRepository : IRepostRepository
{
    private readonly TikTalkDbContext _context;

    public RepostRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsRepostedAsync(long videoId, long userId)
    {
        return await _context.Reposts
            .AnyAsync(r => r.VideoId == videoId && r.UserId == userId);
    }

    public async Task<int> CountByVideoIdAsync(long videoId)
    {
        return await _context.Reposts
            .CountAsync(r => r.VideoId == videoId);
    }

    public async Task<List<Repost>> GetByUserIdAsync(long userId)
    {
        return await _context.Reposts
            .Where(r => r.UserId == userId)
            .Include(r => r.Video)
            .OrderByDescending(r => r.RepostedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Repost repost)
    {
        await _context.Reposts.AddAsync(repost);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long videoId, long userId)
    {
        var repost = await _context.Reposts
            .FirstOrDefaultAsync(r => r.VideoId == videoId && r.UserId == userId);

        if (repost is not null)
        {
            _context.Reposts.Remove(repost);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class NotificationRepository : INotificationRepository
{
    private readonly TikTalkDbContext _context;

    public NotificationRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<List<Notification>> GetByUserIdAsync(long userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        await SaveChangesAsync();
    }

    public async Task MarkAsReadAsync(long id)
    {
        var noti = await _context.Notifications.FindAsync(id);
        if (noti is not null)
        {
            noti.IsRead = true;
            await SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(long id)
    {
        var noti = await _context.Notifications.FindAsync(id);
        if (noti is not null)
        {
            _context.Notifications.Remove(noti);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly TikTalkDbContext _context;

    public RefreshTokenRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken> CreateAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
        await SaveChangesAsync();
        return token;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, long userId)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token && (userId == 0 || rt.UserId == userId));
    }

    public async Task DeleteAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token is not null)
        {
            _context.RefreshTokens.Remove(token);
            await SaveChangesAsync();
        }
    }

    public async Task DeleteAllByUserIdAsync(long userId)
    {
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .ToListAsync();

        _context.RefreshTokens.RemoveRange(tokens);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class BlockedUserRepository : IBlockedUserRepository
{
    private readonly TikTalkDbContext _context;

    public BlockedUserRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsBlockedAsync(long blockerId, long blockedId)
    {
        return await _context.BlockedUsers
            .AnyAsync(b => b.BlockerId == blockerId && b.BlockedId == blockedId);
    }

    public async Task<List<long>> GetBlockedUserIdsAsync(long blockerId)
    {
        return await _context.BlockedUsers
            .Where(b => b.BlockerId == blockerId)
            .Select(b => b.BlockedId)
            .ToListAsync();
    }

    public async Task AddAsync(BlockedUser blockedUser)
    {
        await _context.BlockedUsers.AddAsync(blockedUser);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long blockerId, long blockedId)
    {
        var blocked = await _context.BlockedUsers
            .FirstOrDefaultAsync(b => b.BlockerId == blockerId && b.BlockedId == blockedId);

        if (blocked is not null)
        {
            _context.BlockedUsers.Remove(blocked);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class RoleRepository : IRoleRepository
{
    private readonly TikTalkDbContext _context;

    public RoleRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(long id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
        await SaveChangesAsync();
    }

    public void Update(Role role)
    {
        _context.Roles.Update(role);
    }

    public void Delete(Role role)
    {
        _context.Roles.Remove(role);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class UserRoleRepository : IUserRoleRepository
{
    private readonly TikTalkDbContext _context;

    public UserRoleRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetRoleNamesByUserIdAsync(long userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
    }

    public async Task<List<UserRole>> GetByUserIdAsync(long userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(UserRole userRole)
    {
        await _context.UserRoles.AddAsync(userRole);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long userId, long roleId)
    {
        var ur = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (ur is not null)
        {
            _context.UserRoles.Remove(ur);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class UserLoginRepository : IUserLoginRepository
{
    private readonly TikTalkDbContext _context;

    public UserLoginRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserLogin login)
    {
        await _context.UserLogins.AddAsync(login);
        await SaveChangesAsync();
    }

    public async Task<List<UserLogin>> GetByUserIdAsync(long userId)
    {
        return await _context.UserLogins
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.LoginTime)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class MessageRepository : IMessageRepository
{
    private readonly TikTalkDbContext _context;

    public MessageRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetMessagesAsync(long senderId, long receiverId)
    {
        return await _context.Messages
            .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId)
                     || (m.SenderId == receiverId && m.ReceiverId == senderId))
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<Message?> GetByIdAsync(long id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long id, long senderId)
    {
        var msg = await _context.Messages
            .FirstOrDefaultAsync(m => m.Id == id && m.SenderId == senderId);

        if (msg is not null)
        {
            _context.Messages.Remove(msg);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class VideoAnalyticsRepository : IVideoAnalyticsRepository
{
    private readonly TikTalkDbContext _context;

    public VideoAnalyticsRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<VideoAnalytics?> GetByVideoIdAsync(long videoId)
    {
        return await _context.VideoAnalytics
            .FirstOrDefaultAsync(a => a.VideoId == videoId);
    }

    public async Task AddAsync(VideoAnalytics analytics)
    {
        await _context.VideoAnalytics.AddAsync(analytics);
        await SaveChangesAsync();
    }

    public void Update(VideoAnalytics analytics)
    {
        _context.VideoAnalytics.Update(analytics);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}