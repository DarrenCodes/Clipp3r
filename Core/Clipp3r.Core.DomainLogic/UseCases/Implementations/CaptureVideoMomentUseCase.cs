using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(ICaptureVideoMomentUseCase))]
    class CaptureVideoMomentUseCase : ICaptureVideoMomentUseCase
    {
        private readonly IAddVideoMomentCaptureProcess addVideoMomentCaptureProcess;
        private readonly ISaveChangesToRepository saveChangesToRepository;

        public CaptureVideoMomentUseCase(IAddVideoMomentCaptureProcess addVideoMomentCaptureProcess,
            ISaveChangesToRepository saveChangesToRepository)
        {
            this.addVideoMomentCaptureProcess = addVideoMomentCaptureProcess;
            this.saveChangesToRepository = saveChangesToRepository;
        }

        public async Task CaptureVideoMomentAsync(VideoMomentCaptureDto videoMomentCaptureDto)
        {
            addVideoMomentCaptureProcess.AddVideoMomentCapture(videoMomentCaptureDto);
            await saveChangesToRepository.SaveChangesAsync();
        }
    }
}
