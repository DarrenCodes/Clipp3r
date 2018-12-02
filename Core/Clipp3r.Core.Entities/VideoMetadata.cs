
using System;
using System.Collections.Generic;

namespace Clipp3r.Core.Entities
{
    public class VideoMetadata : BaseEntity<Guid>
    {
        public string VideoFileName { get; set; }
        public DateTime LastClippedDate { get; set; }
        public List<VideoMomentCapture> VideoMomentCaptureList { get; set; }
    }
}
