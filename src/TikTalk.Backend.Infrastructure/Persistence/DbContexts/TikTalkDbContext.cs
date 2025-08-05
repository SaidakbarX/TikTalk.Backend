using Microsoft.EntityFrameworkCore;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Infrastructure.Persistence.DbContexts;

public class TikTalkDbContext : DbContext
{
    public TikTalkDbContext(DbContextOptions<TikTalkDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<BlockedUser> BlockedUsers { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Video> Videos { get; set; }
    public DbSet<VideoLike> VideoLikes { get; set; }
    public DbSet<VideoComment> VideoComments { get; set; }
    public DbSet<CommentLike> CommentLikes { get; set; }
    public DbSet<VideoView> VideoViews { get; set; }
    public DbSet<Hashtag> Hashtags { get; set; }
    public DbSet<VideoHashtag> VideoHashtags { get; set; }
    public DbSet<Repost> Reposts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<VideoAnalytics> VideoAnalytics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TikTalkDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}