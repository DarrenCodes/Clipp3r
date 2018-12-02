using Clipp3r.Core.Common;
using Clipp3r.Core.Entities;
using System;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IAddVideoMetadataProcess))]
    class AddVideoMetadataProcess : IAddVideoMetadataProcess
    {
        private readonly IGenericPersistenceHandler<VideoMetadata> videoMetadataPersistenceHandler;

        public AddVideoMetadataProcess(IGenericPersistenceHandler<VideoMetadata> videoMetadataPersistenceHandler)
        {
            this.videoMetadataPersistenceHandler = videoMetadataPersistenceHandler;
        }

        public VideoMetadata AddVideoMetadata(string videoFileName)
        {
            return videoMetadataPersistenceHandler.Add(new VideoMetadata
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                VideoFileName = videoFileName
            });
        }
    }
}
