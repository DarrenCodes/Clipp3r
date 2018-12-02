using Clipp3r.Core.Common;
using Clipp3r.Core.DomainLogic;
using Clipp3r.Core.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipp3r.Consumers.WPF
{
    [RegisterService(typeof(IVideoMomentCaptureController), ServiceLifetime.Singleton)]
    class VideoMomentCaptureController : IVideoMomentCaptureController
    {
        private readonly IGetServicesScope getServicesScope;

        public VideoMomentCaptureController(IGetServicesScope getServicesScope)
        {
            this.getServicesScope = getServicesScope;
        }

        public async Task CaptureVideoMomentAsync(VideoMomentCaptureDto videoMomentCaptureDto)
        {
            var scope = getServicesScope.BeginScope();

            var captureVideoMomentUseCase = scope.GetRequiredService<ICaptureVideoMomentUseCase>();
            await captureVideoMomentUseCase.CaptureVideoMomentAsync(videoMomentCaptureDto);
        }

        public Task<List<VideoMomentCaptureDto>> GetVideoMomentCaptureListAsync(Guid videoMetadataGuid)
        {
            var scope = getServicesScope.BeginScope();

            var getVideoMomentCaptureUseCase = scope.GetRequiredService<IGetVideoMomentCaptureUseCase>();
            return getVideoMomentCaptureUseCase.GetVideoMomentCaptureListAsync(videoMetadataGuid);
        }
    }
}
