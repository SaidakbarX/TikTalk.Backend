using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Infrastructure.Persistence.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Videos");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Title).IsRequired().HasMaxLength(200);
        builder.Property(v => v.VideoUrl).IsRequired().HasMaxLength(1000);
        builder.Property(v => v.ThumbnailUrl).HasMaxLength(1000);

        builder.HasOne(v => v.Owner)
            .WithMany()
            .HasForeignKey(v => v.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Likes).WithOne(l => l.Video).HasForeignKey(l => l.VideoId);
        builder.HasMany(v => v.Comments).WithOne(c => c.Video).HasForeignKey(c => c.VideoId);
        builder.HasMany(v => v.Views).WithOne(vw => vw.Video).HasForeignKey(vw => vw.VideoId);
        builder.HasMany(v => v.Reposts).WithOne(r => r.Video).HasForeignKey(r => r.VideoId);
    }
}
