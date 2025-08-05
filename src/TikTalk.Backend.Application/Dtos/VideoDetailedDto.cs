using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class VideoDetailedDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string VideoUrl { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public UserDto User { get; set; } = null!;
    public List<string> Hashtags { get; set; } = new();
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public int ViewCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
    public bool IsRepostedByCurrentUser { get; set; }
}