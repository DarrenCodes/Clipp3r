using Clipp3r.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Clipp3r.Infrastructure.EntityFramework
{
    class VideoMomentCaptureConfiguration : BaseConfiguration<VideoMomentCapture, Guid>, IEntityTypeConfiguration<VideoMomentCapture>
    {
        public override void Configure(EntityTypeBuilder<VideoMomentCapture> builder)
        {
            base.Configure(builder);

            builder.ToTable("VideoMomentCaptures");
            
            builder.Property(x => x.Id)
                .HasConversion(new GuidToStringConverter());

            builder.Property(x => x.VideoMomentCaptureTime)
                .IsRequired();

            builder.HasOne(x => x.VideoMoment)
                .WithMany()
                .HasForeignKey(x => x.VideoMomentGuid)
                .IsRequired();

            builder.HasOne(x => x.VideoMetadata)
                .WithMany(x => x.VideoMomentCaptureList)
                .HasForeignKey(x => x.VideoMetadataGuid)
                .IsRequired();
        }
    }
}
