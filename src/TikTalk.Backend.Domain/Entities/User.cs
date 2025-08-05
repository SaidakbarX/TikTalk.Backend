using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<UserLogin> Logins { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    public virtual ICollection<Follow> Followers { get; set; }
    public virtual ICollection<Follow> Following { get; set; }
    public virtual ICollection<BlockedUser> BlockedUsers { get; set; }
}
