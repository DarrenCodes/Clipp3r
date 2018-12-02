using Clipp3r.Core.Common;
using Clipp3r.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IGetVideoMomentCaptureProcess))]
    class GetVideoMomentCaptureProcess : IGetVideoMomentCaptureProcess
    {
        private readonly IGenericPersistenceHandler<VideoMomentCapture> videoMomentCapturePersistenceHandler;

        public GetVideoMomentCaptureProcess(IGenericPersistenceHandler<VideoMomentCapture> videoMomentCapturePersistenceHandler)
        {
            this.videoMomentCapturePersistenceHandler = videoMomentCapturePersistenceHandler;
        }

        public Task<List<VideoMomentCapture>> GetVideoMomentCaptureListAsync(Guid videoMetadataGuid,
            params Expression<Func<VideoMomentCapture, object>>[] includes)
        {
            return videoMomentCapturePersistenceHandler.GetListAsync(
                x => x.VideoMetadataGuid == videoMetadataGuid, includes);
        }
    }
}
