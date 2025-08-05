using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class AssignHashtagsDto
{
    public long VideoId { get; set; }
    public List<string> Hashtags { get; set; } = new();
}