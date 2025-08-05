using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services.Converters;

public static class RepostConverter
{
    public static RepostDto ToDto(Repost repost) => new()
    {
        VideoId = repost.VideoId
    };
}