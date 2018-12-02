using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clipp3r.Core.Dtos;

namespace Clipp3r.Consumers.WPF
{
    public interface IVideoMomentCaptureController
    {
        Task<List<VideoMomentCaptureDto>> GetVideoMomentCaptureListAsync(Guid videoMetadataGuid);
        Task CaptureVideoMomentAsync(VideoMomentCaptureDto videoMomentCaptureDto);
    }
}