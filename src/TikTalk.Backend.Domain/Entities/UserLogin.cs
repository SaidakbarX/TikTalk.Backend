using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class UserLogin
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Provider { get; set; } 
    public string ProviderKey { get; set; }
    public DateTime LoginTime { get; set; }

    public virtual User User { get; set; }
}