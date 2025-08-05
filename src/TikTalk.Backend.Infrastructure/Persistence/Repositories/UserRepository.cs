using Microsoft.EntityFrameworkCore;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Domain.Entities;
using TikTalk.Backend.Infrastructure.Persistence.DbContexts;
using TikTalk.Backend.Core.Exceptions;

namespace TikTalk.Backend.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TikTalkDbContext _context;

    public UserRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.FullName == username);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await SaveChangesAsync();
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class VideoRepository : IVideoRepository
{
    private readonly TikTalkDbContext _context;

    public VideoRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<Video?> GetByIdAsync(long id)
    {
        return await _context.Videos
            .Include(v => v.Owner)
            .Include(v => v.Comments)
            .Include(v => v.Likes)
            .Include(v => v.VideoHashtags)
                .ThenInclude(vh => vh.Hashtag)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Video>> GetByUserIdAsync(long userId)
    {
        return await _context.Videos
            .Where(v => v.OwnerId == userId)
            .OrderByDescending(v => v.UploadedAt)
            .ToListAsync();
    }

    public async Task<List<Video>> GetTrendingVideosAsync(int count)
    {
        return await _context.Videos
            .Include(v => v.Owner)
            .OrderByDescending(v => v.Views.Count)
            .ThenByDescending(v => v.Likes.Count)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<Video>> GetVideosByHashtagAsync(string hashtag)
    {
        return await _context.Videos
            .Where(v => v.VideoHashtags.Any(vh => vh.Hashtag.Name == hashtag))
            .ToListAsync();
    }

    public async Task AddAsync(Video video)
    {
        await _context.Videos.AddAsync(video);
        await SaveChangesAsync();
    }

    public void Update(Video video)
    {
        _context.Videos.Update(video);
    }

    public void Delete(Video video)
    {
        _context.Videos.Remove(video);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class FollowRepository : IFollowRepository
{
    private readonly TikTalkDbContext _context;

    public FollowRepository(TikTalkDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsFollowingAsync(long followerId, long followeeId)
    {
        return await _context.Follows
            .AnyAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
    }

    public async Task<List<long>> GetFollowersIdsAsync(long userId)
    {
        return await _context.Follows
            .Where(f => f.FolloweeId == userId)
            .Select(f => f.FollowerId)
            .ToListAsync();
    }

    public async Task<List<long>> GetFolloweesIdsAsync(long userId)
    {
        return await _context.Follows
            .Where(f => f.FollowerId == userId)
            .Select(f => f.FolloweeId)
            .ToListAsync();
    }

    public async Task AddAsync(Follow follow)
    {
        await _context.Follows.AddAsync(follow);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(long followerId, long followeeId)
    {
        var follow = await _context.Follows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);

        if (follow is not null)
        {
            _context.Follows.Remove(follow);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}