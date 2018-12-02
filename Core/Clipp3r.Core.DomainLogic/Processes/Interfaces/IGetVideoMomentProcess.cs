using System.Collections.Generic;
using System.Threading.Tasks;
using Clipp3r.Core.Dtos;
using Clipp3r.Core.Entities;

namespace Clipp3r.Core.DomainLogic
{
    public interface IGetVideoMomentProcess
    {
        Task<IEnumerable<VideoMoment>> GetVideoMomentListAsync();
    }
}