using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class Follow
{
    public long FollowerId { get; set; }
    public long FolloweeId { get; set; }
    public DateTime FollowedAt { get; set; }

    public virtual User Follower { get; set; }
    public virtual User Followee { get; set; }
}