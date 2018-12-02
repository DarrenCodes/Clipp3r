using System;

namespace Clipp3r.Core.Entities
{
    public class VideoMomentCapture : BaseEntity<Guid>
    {
        public Guid VideoMetadataGuid { get; set; }
        public Guid VideoMomentGuid { get; set; }
        public long VideoMomentCaptureTime { get; set; }

        public VideoMetadata VideoMetadata { get; set; }
        public VideoMoment VideoMoment { get; set; }
    }
}
