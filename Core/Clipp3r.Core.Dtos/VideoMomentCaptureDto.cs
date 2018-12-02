using System;

namespace Clipp3r.Core.Dtos
{
    public class VideoMomentCaptureDto
    {
        public Guid VideoMomentGuid { get; set; }
        public VideoMomentDto VideoMoment { get; set; }
        public Guid VideoMetadataGuid { get; set; }
        public VideoMetadataDto VideoMetadata { get; set; }
        public long CapturedTime { get; set; }
    }
}
