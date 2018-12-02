using Clipp3r.Core.Common;
using Clipp3r.Core.DomainLogic;
using Clipp3r.Core.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Clipp3r.Consumers.WPF
{
    [RegisterService(typeof(IVideoMetdataController), ServiceLifetime.Singleton)]
    class VideoMetdataController : IVideoMetdataController
    {
        private readonly IGetServicesScope getServicesScope;

        public VideoMetdataController(IGetServicesScope getServicesScope)
        {
            this.getServicesScope = getServicesScope;
        }

        public Task<VideoMetadataDto> CreateVideoMetadataAsync(string videoFileName)
        {
            var scope = getServicesScope.BeginScope();

            var createVideoMetadataUseCase = scope.GetRequiredService<ICreateVideoMetadataUseCase>();
            return createVideoMetadataUseCase.CreateVideoMetdataAsync(videoFileName);
        }

        public Task<VideoMetadataDto> GetVideoMetadataAsync(string videoMomentName)
        {
            var scope = getServicesScope.BeginScope();

            var getVideoMetadataUseCase = scope.GetRequiredService<IGetVideoMetadataUseCase>();
            return getVideoMetadataUseCase.GetVideoMetadataAsync(videoMomentName);
        }

        public Task UpdateLastClippedDateAsync(Guid videoMetadataGuid)
        {
            var scope = getServicesScope.BeginScope();

            var updateVideoMetadataUseCase = scope.GetRequiredService<IUpdateVideoMetadataUseCase>();
            return updateVideoMetadataUseCase.UpdateLastClippedDateAsync(videoMetadataGuid);
        }
    }
}
