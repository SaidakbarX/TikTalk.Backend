using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class Video
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public string Title { get; set; }
    public string VideoUrl { get; set; } 
    public string? ThumbnailUrl { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime UploadedAt { get; set; }

    public virtual User Owner { get; set; }

    public virtual ICollection<VideoLike> Likes { get; set; }
    public virtual ICollection<VideoComment> Comments { get; set; }
    public virtual ICollection<VideoView> Views { get; set; }
    public virtual ICollection<VideoHashtag> VideoHashtags { get; set; }
    public virtual ICollection<Repost> Reposts { get; set; }
}
