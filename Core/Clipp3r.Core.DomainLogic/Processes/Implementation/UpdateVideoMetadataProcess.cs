using Clipp3r.Core.Common;
using Clipp3r.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IUpdateVideoMetadataProcess))]
    class UpdateVideoMetadataProcess : IUpdateVideoMetadataProcess
    {
        private readonly IGenericPersistenceHandler<VideoMetadata> videoMetadataPersistenceHandler;

        public UpdateVideoMetadataProcess(IGenericPersistenceHandler<VideoMetadata> videoMetadataPersistenceHandler)
        {
            this.videoMetadataPersistenceHandler = videoMetadataPersistenceHandler;
        }

        public async Task<VideoMetadata> UpdateLastClippedDateAsync(Guid videoMetadataGuid)
        {
            VideoMetadata videoMetadata = await videoMetadataPersistenceHandler.GetAsync(
                x => x.Id.ToString() == videoMetadataGuid.ToString());

            videoMetadata.LastClippedDate = DateTime.Now;

            return videoMetadata;
        }
    }
}
