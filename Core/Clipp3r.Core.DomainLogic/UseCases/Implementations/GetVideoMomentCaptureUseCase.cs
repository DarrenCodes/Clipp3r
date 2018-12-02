using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IGetVideoMomentCaptureUseCase))]
    class GetVideoMomentCaptureUseCase : IGetVideoMomentCaptureUseCase
    {
        private readonly IGetVideoMomentCaptureProcess getVideoMomentCaptureProcess;

        public GetVideoMomentCaptureUseCase(IGetVideoMomentCaptureProcess getVideoMomentCaptureProcess)
        {
            this.getVideoMomentCaptureProcess = getVideoMomentCaptureProcess;
        }

        public async Task<List<VideoMomentCaptureDto>> GetVideoMomentCaptureListAsync(Guid videoMetadataGuid)
        {
            var videoMomentCaptureList = await getVideoMomentCaptureProcess.GetVideoMomentCaptureListAsync(
                videoMetadataGuid, 
                incl => incl.VideoMetadata,
                incl => incl.VideoMoment);

            return videoMomentCaptureList.Select(x => new VideoMomentCaptureDto
            {
                VideoMetadataGuid = x.VideoMetadataGuid,
                VideoMetadata = new VideoMetadataDto
                {
                    VideoFileName = x.VideoMetadata.VideoFileName,
                    VideoMetadataDataGuid = x.VideoMetadataGuid
                },
                VideoMomentGuid = x.VideoMomentGuid,
                VideoMoment = new VideoMomentDto
                {
                    VideoMomentGuid = x.VideoMomentGuid,
                    VideoMomentName = x.VideoMoment.VideoMomentName
                },
                CapturedTime = x.VideoMomentCaptureTime
            }).ToList();
        }
    }
}
