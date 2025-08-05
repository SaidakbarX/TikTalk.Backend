using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class Repost
{
    public long Id { get; set; }
    public long VideoId { get; set; }
    public long UserId { get; set; }
    public DateTime RepostedAt { get; set; }

    public virtual Video Video { get; set; }
    public virtual User User { get; set; }
}