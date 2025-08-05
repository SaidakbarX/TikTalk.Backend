using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class VideoDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public UserDto User { get; set; } = null!;
}