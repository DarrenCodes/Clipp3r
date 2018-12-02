using Clipp3r.Core.Common;
using Clipp3r.Core.DomainLogic;
using Clipp3r.Core.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Clipp3r.Consumers.WPF
{
    [RegisterService(typeof(IVideoToolsController), ServiceLifetime.Singleton)]
    class VideoToolsController : IVideoToolsController
    {
        private readonly IGetServicesScope getServicesScope;

        public VideoToolsController(IGetServicesScope getServicesScope)
        {
            this.getServicesScope = getServicesScope;
        }

        public void ClipVideo(string filePath, List<VideoMomentCaptureDto> videoMomentCaptureList)
        {
            var scope = getServicesScope.BeginScope();

            var videoClippingUseCase = scope.GetRequiredService<IVideoClippingUseCase>();
            videoClippingUseCase.ClipVideo(filePath, videoMomentCaptureList);
        }
    }
}
