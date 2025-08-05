using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class VideoCommentDto
{
    public long Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int LikeCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }

    public UserDto User { get; set; } = null!;
    public List<VideoCommentDto> Replies { get; set; } = new();
}