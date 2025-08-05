using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

public class Message
{
    public long Id { get; set; }
    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public string Text { get; set; }
    public bool IsRead { get; set; }
    public DateTime SentAt { get; set; }

    public User Sender { get; set; }
    public User Receiver { get; set; }
}