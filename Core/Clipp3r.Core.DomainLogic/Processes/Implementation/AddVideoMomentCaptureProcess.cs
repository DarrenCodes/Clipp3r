using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using Clipp3r.Core.Entities;
using System;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IAddVideoMomentCaptureProcess))]
    class AddVideoMomentCaptureProcess : IAddVideoMomentCaptureProcess
    {
        private readonly IGenericPersistenceHandler<VideoMomentCapture> videoMomentCapturePersistenceHandler;

        public AddVideoMomentCaptureProcess(IGenericPersistenceHandler<VideoMomentCapture> videoMomentCapturePersistenceHandler)
        {
            this.videoMomentCapturePersistenceHandler = videoMomentCapturePersistenceHandler;
        }

        public void AddVideoMomentCapture(VideoMomentCaptureDto videoMomentCaptureDto)
        {
            VideoMomentCapture videoMomentCapture = new VideoMomentCapture()
            {
                Id = Guid.NewGuid(),
                VideoMetadataGuid = videoMomentCaptureDto.VideoMetadataGuid,
                VideoMomentGuid = videoMomentCaptureDto.VideoMomentGuid,
                VideoMomentCaptureTime = videoMomentCaptureDto.CapturedTime
            };
            videoMomentCapturePersistenceHandler.Add(videoMomentCapture);
        }
    }
}
