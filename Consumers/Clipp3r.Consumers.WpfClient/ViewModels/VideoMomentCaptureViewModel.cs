using Clipp3r.Common.Commands;
using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Clipp3r.ViewModels
{
    [RegisterService(serviceLifetime: ServiceLifetime.Transient)]
    class VideoMomentCaptureViewModel
    {
        public string VideoMomentName { get; set; }
        public long VideoMomentCaptureTime { get; set; }

        public Command LoadVideoMomentCaptureCommand { get; }
        public event Action<long> CpaturedVideoMomentClicked;

        public VideoMomentCaptureViewModel()
        {
            LoadVideoMomentCaptureCommand = new Command(
                (o) => CpaturedVideoMomentClicked(VideoMomentCaptureTime));
        }

        public VideoMomentCaptureViewModel(VideoMomentCaptureDto videoMomentDto) : this()
        {
            VideoMomentName = videoMomentDto.VideoMoment.VideoMomentName;
            VideoMomentCaptureTime = videoMomentDto.CapturedTime;
        }
    }
}
