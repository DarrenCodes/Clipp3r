using Clipp3r.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Clipp3r.Infrastructure.EntityFramework
{
    class VideoMomentConfiguration : BaseConfiguration<VideoMoment, Guid>, IEntityTypeConfiguration<VideoMoment>
    {
        public override void Configure(EntityTypeBuilder<VideoMoment> builder)
        {
            base.Configure(builder);

            builder.ToTable("VideoMoments");
            
            builder.Property(x => x.Id)
                .HasConversion(new GuidToStringConverter());

            builder.Property(x => x.VideoMomentName)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
