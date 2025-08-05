using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class FollowInfoDto
{
    public long UserId { get; set; }
    public DateTime FollowedAt { get; set; }
}