using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class BlockedUser
{
    public long BlockerId { get; set; }
    public long BlockedId { get; set; }
    public DateTime BlockedAt { get; set; }

    public virtual User Blocker { get; set; }
    public virtual User Blocked { get; set; }
}