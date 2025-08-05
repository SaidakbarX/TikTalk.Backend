using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class SendMessageDto
{
    public long ReceiverId { get; set; }
    public string Text { get; set; } = null!;
}