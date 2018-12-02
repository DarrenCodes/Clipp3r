using Clipp3r.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Clipp3r.Infrastructure.EntityFramework
{
    class VideoMetadataConfiguration : BaseConfiguration<VideoMetadata, Guid>, IEntityTypeConfiguration<VideoMetadata>
    {
        public override void Configure(EntityTypeBuilder<VideoMetadata> builder)
        {
            base.Configure(builder);

            builder.ToTable("VideoMetadatas");

            builder.Property(x => x.Id)
                .HasConversion(new GuidToStringConverter());

            builder.Property(x => x.LastClippedDate)
                .IsRequired();

            builder.Property(x => x.VideoFileName)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasMany(x => x.VideoMomentCaptureList)
                .WithOne(x => x.VideoMetadata)
                .HasForeignKey(x => x.VideoMetadataGuid)
                .IsRequired();
        }
    }
}
