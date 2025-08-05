using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class VideoComment
{
    public long Id { get; set; }
    public long VideoId { get; set; }
    public long UserId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }

    public long? ParentCommentId { get; set; }
    public virtual VideoComment? ParentComment { get; set; }

    public virtual User User { get; set; }
    public virtual Video Video { get; set; }
    public virtual ICollection<CommentLike> Likes { get; set; }
    public virtual ICollection<VideoComment> Replies { get; set; }
}