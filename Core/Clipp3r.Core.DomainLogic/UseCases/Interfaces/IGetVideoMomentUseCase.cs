using Clipp3r.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface IGetVideoMomentUseCase
    {
        Task<IEnumerable<VideoMomentDto>> GetVideoMomentListAsync();
    }
}