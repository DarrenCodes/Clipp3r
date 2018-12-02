using Clipp3r.Core.Dtos;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    public interface ICaptureVideoMomentUseCase
    {
        Task CaptureVideoMomentAsync(VideoMomentCaptureDto videoMomentCaptureDto);
    }
}