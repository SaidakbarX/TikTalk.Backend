using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class VideoLikeDto
{
    public long UserId { get; set; }
    public DateTime LikedAt { get; set; }
}