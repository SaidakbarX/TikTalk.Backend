using TikTalk.Backend.Application.Helpers;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;
using TikTalk.Backend.Application.Services;
using TikTalk.Backend.Infrastructure.Persistence.Repositories;

namespace TikTalk.Backend.Api.Configuration;

public static class DependesiesInjectionConfiguration
{
    public static void ConfigureDependencies(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IVideoCommentService, VideoCommentService>();
        services.AddScoped<IRepostService, RepostService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        services.AddScoped<IFileStorageService, CloudinaryFileStorageService>();


        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IVideoLikeRepository, VideoLikeRepository>();
        services.AddScoped<ICommentLikeRepository, CommentLikeRepository>();
        services.AddScoped<IVideoCommentRepository, VideoCommentRepository>();
        services.AddScoped<IVideoViewRepository, VideoViewRepository>();
        services.AddScoped<IHashtagRepository, HashtagRepository>();
        services.AddScoped<IVideoHashtagRepository, VideoHashtagRepository>();
        services.AddScoped<IRepostRepository, RepostRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IBlockedUserRepository, BlockedUserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserLoginRepository, UserLoginRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IVideoAnalyticsRepository, VideoAnalyticsRepository>();
    }
}
