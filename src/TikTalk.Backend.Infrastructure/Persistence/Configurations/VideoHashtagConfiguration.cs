using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Infrastructure.Persistence.Configurations;

public class VideoHashtagConfiguration : IEntityTypeConfiguration<VideoHashtag>
{
    public void Configure(EntityTypeBuilder<VideoHashtag> builder)
    {
        builder.ToTable("VideoHashtags");

        builder.HasKey(vh => new { vh.VideoId, vh.HashtagId });

        builder.HasOne(vh => vh.Video)
            .WithMany(v => v.VideoHashtags)
            .HasForeignKey(vh => vh.VideoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(vh => vh.Hashtag)
            .WithMany(h => h.VideoHashtags)
            .HasForeignKey(vh => vh.HashtagId);
    }
}
