using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(ICreateVideoMomentUseCase))]
    class CreateVideoMomentUseCase : ICreateVideoMomentUseCase
    {
        private readonly IAddVideoMomentProcess addVideoMomentProcess;
        private readonly ISaveChangesToRepository saveChangesToRepository;

        public CreateVideoMomentUseCase(IAddVideoMomentProcess addVideoMomentProcess,
            ISaveChangesToRepository saveChangesToRepository)
        {
            this.addVideoMomentProcess = addVideoMomentProcess;
            this.saveChangesToRepository = saveChangesToRepository;
        }

        public async Task<VideoMomentDto> CreateVideoMomentAsync(string videoMomentName)
        {
            var videoMoment  = addVideoMomentProcess.AddVideoMoment(videoMomentName);
            await saveChangesToRepository.SaveChangesAsync();

            return new VideoMomentDto
            {
                VideoMomentGuid = videoMoment.Id,
                VideoMomentName = videoMoment.VideoMomentName
            };
        }
    }
}
