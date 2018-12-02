using Clipp3r.Common.Commands;
using Clipp3r.Consumers.WPF;
using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Clipp3r.ViewModels
{
    [RegisterService(serviceLifetime: ServiceLifetime.Transient)]
    class VideoMomentViewModel
    {
        private readonly IVideoMomentCaptureController videoMomentCaptureController;

        public Guid VideoMomentGuid { get; private set; }
        public string VideoMomentName { get; private set; }
        public Command SaveVideoMomentCommand { get; }

        public event Action<VideoMomentCaptureDto> NewVideoMomentCaptureEvent;

        public VideoMomentViewModel(IVideoMomentCaptureController videoMomentCaptureController)
        {
            SaveVideoMomentCommand = new Command(AddVideoMomentCapture);
            this.videoMomentCaptureController = videoMomentCaptureController;
        }

        public void LoadVideoMoment(VideoMomentDto videoMoment)
        {
            VideoMomentName = videoMoment.VideoMomentName;
            VideoMomentGuid = videoMoment.VideoMomentGuid;
        }

        private void AddVideoMomentCapture(object videoMomentCaptureDto)
        {
            var videoMomentCapture = (VideoMomentCaptureDto)videoMomentCaptureDto;

            if (videoMomentCapture.VideoMetadata == null)
                return;

            videoMomentCapture.VideoMomentGuid = VideoMomentGuid;
            videoMomentCapture.VideoMoment = new VideoMomentDto
            {
                VideoMomentGuid = VideoMomentGuid,
                VideoMomentName = VideoMomentName
            };
            videoMomentCaptureController.CaptureVideoMomentAsync(videoMomentCapture).Wait();

            NewVideoMomentCaptureEvent((VideoMomentCaptureDto)videoMomentCaptureDto);
        }
    }
}
