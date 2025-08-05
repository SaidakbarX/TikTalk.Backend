using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Infrastructure.Persistence.Configurations;

public class VideoViewConfiguration : IEntityTypeConfiguration<VideoView>
{
    public void Configure(EntityTypeBuilder<VideoView> builder)
    {
        builder.ToTable("VideoViews");

        builder.HasKey(v => v.Id);

        builder.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.Video)
            .WithMany(v => v.Views)
            .HasForeignKey(v => v.VideoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
