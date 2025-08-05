using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services.Converters;

public static class FollowConverter
{
    public static FollowInfoDto ToDto(Follow follow) => new()
    {
        UserId = follow.FolloweeId,
        FollowedAt = follow.FollowedAt
    };
}