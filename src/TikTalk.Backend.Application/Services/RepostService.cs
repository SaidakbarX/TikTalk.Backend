using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Interfaces.Repositories;
using TikTalk.Backend.Application.Interfaces.Services;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services;

public class RepostService : IRepostService
{
    private readonly IRepostRepository _repostRepository;

    public RepostService(IRepostRepository repostRepository)
    {
        _repostRepository = repostRepository;
    }

    public async Task RepostAsync(long userId, long videoId)
    {
        var alreadyReposted = await _repostRepository.IsRepostedAsync(videoId, userId);
        if (!alreadyReposted)
        {
            var repost = new Repost
            {
                VideoId = videoId,
                UserId = userId,
                RepostedAt = DateTime.UtcNow
            };

            await _repostRepository.AddAsync(repost);
        }
    }

    public async Task UnrepostAsync(long userId, long videoId)
    {
        var alreadyReposted = await _repostRepository.IsRepostedAsync(videoId, userId);
        if (alreadyReposted)
        {
            await _repostRepository.DeleteAsync(videoId, userId);
        }
    }

    public Task<bool> IsRepostedAsync(long userId, long videoId)
    {
        return _repostRepository.IsRepostedAsync(videoId, userId);
    }

    public Task<int> CountByVideoIdAsync(long videoId)
    {
        return _repostRepository.CountByVideoIdAsync(videoId);
    }
}