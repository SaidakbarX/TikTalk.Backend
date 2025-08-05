using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Application.Dtos;

public class VideoUploadDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile Thumbnail { get; set; }
    public IFormFile File { get; set; }
}