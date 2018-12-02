using Clipp3r.Core.Dtos;

namespace Clipp3r.Core.DomainLogic
{
    public interface IAddVideoMomentCaptureProcess
    {
        void AddVideoMomentCapture(VideoMomentCaptureDto videoMomentCaptureDto);
    }
}