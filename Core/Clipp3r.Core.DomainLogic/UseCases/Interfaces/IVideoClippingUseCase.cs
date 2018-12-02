using Clipp3r.Core.Dtos;
using System.Collections.Generic;

namespace Clipp3r.Core.DomainLogic
{
    public interface IVideoClippingUseCase
    {
        void ClipVideo(string filePath, IEnumerable<VideoMomentCaptureDto> videoMomentCaptureList);
    }
}