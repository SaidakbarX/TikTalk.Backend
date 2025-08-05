using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class Role
{
    public long Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}
