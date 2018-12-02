using Clipp3r.Core.Dtos;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface ICreateVideoMomentUseCase
    {
        Task<VideoMomentDto> CreateVideoMomentAsync(string videoMomentName);
    }
}