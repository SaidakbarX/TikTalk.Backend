using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class CreateNotificationDto
{
    public long UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
}