using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Infrastructure.Persistence.Configurations;

public class VideoAnalyticsConfiguration : IEntityTypeConfiguration<VideoAnalytics>
{
    public void Configure(EntityTypeBuilder<VideoAnalytics> builder)
    {
        builder.ToTable("VideoAnalytics");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.ViewCount)
            .IsRequired();

        builder.Property(a => a.LikeCount)
            .IsRequired();

        builder.Property(a => a.CommentCount)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired();

        builder.HasOne(a => a.Video)
            .WithMany()
            .HasForeignKey(a => a.VideoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
