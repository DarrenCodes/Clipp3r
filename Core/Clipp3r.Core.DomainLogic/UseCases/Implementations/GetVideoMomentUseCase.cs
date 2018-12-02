using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IGetVideoMomentUseCase))]
    class GetVideoMomentUseCase : IGetVideoMomentUseCase
    {
        private readonly IGetVideoMomentProcess getVideoMomentProcess;

        public GetVideoMomentUseCase(IGetVideoMomentProcess getVideoMomentProcess)
        {
            this.getVideoMomentProcess = getVideoMomentProcess;
        }

        public async Task<IEnumerable<VideoMomentDto>> GetVideoMomentListAsync()
        {
            var videoMomentList = await getVideoMomentProcess.GetVideoMomentListAsync();

            return videoMomentList.Select(x => new VideoMomentDto
            {
                VideoMomentGuid = x.Id,
                VideoMomentName = x.VideoMomentName
            });
        }
    }
}
