using Clipp3r.Core.Common;
using Clipp3r.Core.Entities;
using System;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IAddVideoMomentProcess))]
    class AddVideoMomentProcess : IAddVideoMomentProcess
    {
        private readonly IGenericPersistenceHandler<VideoMoment> videoMomentPersistenceHandler;

        public AddVideoMomentProcess(IGenericPersistenceHandler<VideoMoment> videoMomentPersistenceHandler)
        {
            this.videoMomentPersistenceHandler = videoMomentPersistenceHandler;
        }

        public VideoMoment AddVideoMoment(string videoMomentName)
        {
            return videoMomentPersistenceHandler.Add(new VideoMoment
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                VideoMomentName = videoMomentName
            });
        }
    }
}
