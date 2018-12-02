using Clipp3r.Core.Common;
using Clipp3r.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IGetVideoMomentProcess))]
    class GetVideoMomentProcess : IGetVideoMomentProcess
    {
        private readonly IGenericPersistenceHandler<VideoMoment> videoMomentPersistenceHandler;

        public GetVideoMomentProcess(IGenericPersistenceHandler<VideoMoment> videoMomentPersistenceHandler)
        {
            this.videoMomentPersistenceHandler = videoMomentPersistenceHandler;
        }

        public async Task<IEnumerable<VideoMoment>> GetVideoMomentListAsync()
        {
            return await videoMomentPersistenceHandler.GetListAsync(x => x.IsActive);
        }
    }
}
