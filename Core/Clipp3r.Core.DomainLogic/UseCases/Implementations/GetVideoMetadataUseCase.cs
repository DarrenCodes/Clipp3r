using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IGetVideoMetadataUseCase))]
    class GetVideoMetadataUseCase : IGetVideoMetadataUseCase
    {
        private readonly IGetVideoMetadataProcess getVideoMetadataProcess;

        public GetVideoMetadataUseCase(IGetVideoMetadataProcess getVideoMetadataProcess)
        {
            this.getVideoMetadataProcess = getVideoMetadataProcess;
        }

        public Task<VideoMetadataDto> GetVideoMetadataAsync(string videoFileName)
        {
            return getVideoMetadataProcess.GetVideoMetadataAsync(videoFileName);
        }
    }
}
