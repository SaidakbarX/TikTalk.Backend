using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class UpdateUserDto
{
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
}