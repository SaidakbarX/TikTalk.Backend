using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class CreateVideoDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string VideoUrl { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public List<string> Hashtags { get; set; } = new();
}