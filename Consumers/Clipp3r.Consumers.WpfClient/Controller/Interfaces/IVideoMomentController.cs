using System.Collections.Generic;
using System.Threading.Tasks;
using Clipp3r.Core.Dtos;

namespace Clipp3r.Consumers.WPF
{
    public interface IVideoMomentController
    {
        Task<VideoMomentDto> CreateVideoMomentAsync(string videoMomentName);
        Task<IEnumerable<VideoMomentDto>> GetVideoMomentListAsync();
    }
}