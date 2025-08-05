using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class CreateVideoCommentDto
{
    public long VideoId { get; set; }
    public string Text { get; set; } = null!;
    public long? ParentCommentId { get; set; } // optional reply
}
