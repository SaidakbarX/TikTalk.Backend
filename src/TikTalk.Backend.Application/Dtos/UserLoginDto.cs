using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class UserLoginDto
{
    public string Provider { get; set; } = null!;
    public string ProviderKey { get; set; } = null!;
    public DateTime LoginTime { get; set; }
    public string? Password { get; set; }
}