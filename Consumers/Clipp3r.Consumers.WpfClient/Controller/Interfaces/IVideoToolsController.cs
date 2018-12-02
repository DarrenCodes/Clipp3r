using System.Collections.Generic;
using Clipp3r.Core.Dtos;

namespace Clipp3r.Consumers.WPF
{
    public interface IVideoToolsController
    {
        void ClipVideo(string filePath, List<VideoMomentCaptureDto> videoMomentCaptureList);
    }
}