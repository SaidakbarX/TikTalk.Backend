using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class VideoHashtag
{
    public long VideoId { get; set; }
    public long HashtagId { get; set; }

    public virtual Video Video { get; set; }
    public virtual Hashtag Hashtag { get; set; }
}