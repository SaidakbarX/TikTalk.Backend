using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTalk.Backend.Domain.Entities;

    public class VideoLike
    {
        public long UserId { get; set; }
        public long VideoId { get; set; }
        public DateTime LikedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Video Video { get; set; }
    }