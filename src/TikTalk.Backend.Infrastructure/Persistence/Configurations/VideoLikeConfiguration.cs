using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Infrastructure.Persistence.Configurations;

public class VideoLikeConfiguration : IEntityTypeConfiguration<VideoLike>
{
    public void Configure(EntityTypeBuilder<VideoLike> builder)
    {
        builder.ToTable("VideoLikes");

        builder.HasKey(l => new { l.UserId, l.VideoId });

        builder.HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
