using Clipp3r.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface IGetVideoMomentCaptureProcess
    {
        Task<List<VideoMomentCapture>> GetVideoMomentCaptureListAsync(Guid videoMetadataGuid,
            params Expression<Func<VideoMomentCapture, object>>[] includes);
    }
}