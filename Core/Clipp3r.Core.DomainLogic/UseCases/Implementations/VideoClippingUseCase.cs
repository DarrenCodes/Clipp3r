using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System.Collections.Generic;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IVideoClippingUseCase))]
    class VideoClippingUseCase : IVideoClippingUseCase
    {
        private readonly IVideoClippingProcess videoClippingProcess;

        public VideoClippingUseCase(IVideoClippingProcess videoClippingProcess)
        {
            this.videoClippingProcess = videoClippingProcess;
        }

        public void ClipVideo(string filePath, IEnumerable<VideoMomentCaptureDto> videoMomentCaptureList)
        {
            videoClippingProcess.ClipVideo(filePath, videoMomentCaptureList);
        }
    }
}
