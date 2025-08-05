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

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;

    public FollowService(
        IFollowRepository followRepository,
        IUserRepository userRepository)
    {
        _followRepository = followRepository;
        _userRepository = userRepository;
    }

    public async Task FollowAsync(long followerId, long followeeId)
    {
        if (followerId == followeeId) return; // o‘zini follow qila olmaydi

        var isFollowing = await _followRepository.IsFollowingAsync(followerId, followeeId);
        if (!isFollowing)
        {
            var follow = new Follow
            {
                FollowerId = followerId,
                FolloweeId = followeeId,
                FollowedAt = DateTime.UtcNow
            };

            await _followRepository.AddAsync(follow);
        }
    }

    public async Task UnfollowAsync(long followerId, long followeeId)
    {
        var isFollowing = await _followRepository.IsFollowingAsync(followerId, followeeId);
        if (isFollowing)
        {
            await _followRepository.DeleteAsync(followerId, followeeId);
        }
    }

    public Task<bool> IsFollowingAsync(long followerId, long followeeId)
    {
        return _followRepository.IsFollowingAsync(followerId, followeeId);
    }

    public async Task<List<FollowInfoDto>> GetFollowersAsync(long userId)
    {
        var ids = await _followRepository.GetFollowersIdsAsync(userId);

        var result = new List<FollowInfoDto>();
        foreach (var id in ids)
        {
            result.Add(new FollowInfoDto
            {
                UserId = id,
                FollowedAt = DateTime.UtcNow
            });
        }

        return result;
    }

    public async Task<List<FollowInfoDto>> GetFollowingAsync(long userId)
    {
        var ids = await _followRepository.GetFolloweesIdsAsync(userId);

        var result = new List<FollowInfoDto>();
        foreach (var id in ids)
        {
            result.Add(new FollowInfoDto
            {
                UserId = id,
                FollowedAt = DateTime.UtcNow
            });
        }

        return result;
    }
}