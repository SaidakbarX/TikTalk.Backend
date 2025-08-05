using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class CommentLike
{
    public long UserId { get; set; }
    public long CommentId { get; set; }
    public DateTime LikedAt { get; set; }

    public virtual User User { get; set; }
    public virtual VideoComment Comment { get; set; }
}
