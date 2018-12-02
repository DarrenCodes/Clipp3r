using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using Clipp3r.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IGetVideoMetadataProcess))]
    class GetVideoMetadataProcess : IGetVideoMetadataProcess
    {
        private readonly IGenericPersistenceHandler<VideoMetadata> videoMetadataPersistenceHandler;

        public GetVideoMetadataProcess(IGenericPersistenceHandler<VideoMetadata> videoMetadataPersistenceHandler)
        {
            this.videoMetadataPersistenceHandler = videoMetadataPersistenceHandler;
        }

        public Task<VideoMetadataDto> GetVideoMetadataAsync(string videoFileName)
        {
            return videoMetadataPersistenceHandler.GetSelectAsync(
                x => x.VideoFileName == videoFileName,
                s => new VideoMetadataDto
                {
                    VideoFileName = s.VideoFileName,
                    VideoMetadataDataGuid = s.Id,
                    VideoMomentCaptureList = s.VideoMomentCaptureList
                    .Select(x => new VideoMomentCaptureDto
                    {
                        VideoMetadataGuid = x.VideoMetadataGuid,
                        VideoMomentGuid = x.VideoMomentGuid,
                        VideoMoment = new VideoMomentDto
                        {
                            VideoMomentGuid = x.VideoMomentGuid,
                            VideoMomentName = x.VideoMoment.VideoMomentName
                        },
                        CapturedTime = x.VideoMomentCaptureTime
                    }).ToList()
                });
        }
    }
}
