using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Infrastructure.Persistence.Configurations;

public class RepostConfiguration : IEntityTypeConfiguration<Repost>
{
    public void Configure(EntityTypeBuilder<Repost> builder)
    {
        builder.ToTable("Reposts");

        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Video)
            .WithMany(v => v.Reposts)
            .HasForeignKey(r => r.VideoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
