using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IUpdateVideoMetadataUseCase))]
    class UpdateVideoMetadataUseCase : IUpdateVideoMetadataUseCase
    {
        private readonly IUpdateVideoMetadataProcess updateVideoMetadataProcess;
        private readonly ISaveChangesToRepository saveChangesToRepository;

        public UpdateVideoMetadataUseCase(IUpdateVideoMetadataProcess updateVideoMetadataProcess,
            ISaveChangesToRepository saveChangesToRepository)
        {
            this.updateVideoMetadataProcess = updateVideoMetadataProcess;
            this.saveChangesToRepository = saveChangesToRepository;
        }

        public async Task UpdateLastClippedDateAsync(Guid videoMetadataGuid)
        {
            await updateVideoMetadataProcess.UpdateLastClippedDateAsync(videoMetadataGuid);
            await saveChangesToRepository.SaveChangesAsync();
        }
    }
}
