using Clipp3r.Core.Common;
using Clipp3r.Core.DomainLogic;
using Clipp3r.Core.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipp3r.Consumers.WPF
{
    [RegisterService(typeof(IVideoMomentController), ServiceLifetime.Singleton)]
    class VideoMomentController : IVideoMomentController
    {
        private readonly IGetServicesScope getServicesScope;

        public VideoMomentController(IGetServicesScope getServicesScope)
        {
            this.getServicesScope = getServicesScope;
        }

        public Task<VideoMomentDto> CreateVideoMomentAsync(string videoMomentName)
        {
            var scope = getServicesScope.BeginScope();

            var createVideoMomentUseCase = scope.GetRequiredService<ICreateVideoMomentUseCase>();
            return createVideoMomentUseCase.CreateVideoMomentAsync(videoMomentName);
        }

        public Task<IEnumerable<VideoMomentDto>> GetVideoMomentListAsync()
        {
            var scope = getServicesScope.BeginScope();

            var getVideoMomentUseCase = scope.GetRequiredService<IGetVideoMomentUseCase>();
            return getVideoMomentUseCase.GetVideoMomentListAsync();
        }
    }
}
