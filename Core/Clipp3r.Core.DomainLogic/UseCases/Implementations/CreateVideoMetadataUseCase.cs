using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(ICreateVideoMetadataUseCase))]
    class CreateVideoMetadataUseCase : ICreateVideoMetadataUseCase
    {
        private readonly IAddVideoMetadataProcess addVideoMetadataProcess;
        private readonly ISaveChangesToRepository saveChangesToRepository;

        public CreateVideoMetadataUseCase(IAddVideoMetadataProcess addVideoMetadataProcess,
            ISaveChangesToRepository saveChangesToRepository)
        {
            this.addVideoMetadataProcess = addVideoMetadataProcess;
            this.saveChangesToRepository = saveChangesToRepository;
        }

        public async Task<VideoMetadataDto> CreateVideoMetdataAsync(string videoFileName)
        {
            var videoMetadata  = addVideoMetadataProcess.AddVideoMetadata(videoFileName);
            await saveChangesToRepository.SaveChangesAsync();

            return new VideoMetadataDto
            {
                VideoMetadataDataGuid = videoMetadata.Id,
                VideoFileName = videoMetadata.VideoFileName
            };
        }
    }
}
