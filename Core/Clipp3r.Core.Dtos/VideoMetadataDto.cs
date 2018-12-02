using System;
using System.Collections.Generic;

namespace Clipp3r.Core.Dtos
{
    public class VideoMetadataDto
    {
        public Guid VideoMetadataDataGuid { get; set; }
        public string VideoFileName { get; set; }
        public List<VideoMomentCaptureDto> VideoMomentCaptureList { get; set; }
    }
}
