using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class VideoAnalytics
{
    public long Id { get; set; }
    public long VideoId { get; set; }
    public int ViewCount { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Video Video { get; set; }
}