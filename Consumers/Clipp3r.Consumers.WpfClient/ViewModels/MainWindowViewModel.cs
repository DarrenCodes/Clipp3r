using Clipp3r.Common.Commands;
using Clipp3r.Consumers.WPF;
using Clipp3r.Consumers.WpfClient;
using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using Clipp3r.Views;
using JohnnysLibrary.WPF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Clipp3r.ViewModels
{
    [RegisterService(serviceLifetime: ServiceLifetime.Singleton)]
    class MainWindowViewModel
    {
        private VideoMetadataDto videoMetadata;

        public MediaPlayerViewModel MediaPlayerViewModel { get; }

        public VideoMomentCaptureDto VideoMomentCapture { get; }

        public Command AddCodeCommand { get; }

        public Command OpenFileCommand { get; }
        public Command PreviewFileCommand { get; }

        public Command ClipVideoCommand { get; }

        public ObserverableProperty<FilePathPair> CurrentFilePath { get; private set; }
        public ObserverableProperty<bool> AbleToLoadViewPreview { get; private set; }

        private readonly IGetServicesScope getServicesScope;
        private readonly IVideoMetdataController videoMetdataController;
        private readonly IVideoMomentController videoMomentController;
        private readonly IVideoMomentCaptureController videoMomentCaptureController;
        private readonly IVideoToolsController videoToolsController;

        public string NewVideoMomentName { get; set; }

        public ObservableCollection<VideoMomentViewModel> VideoMomentList { get; }
        public ObservableCollection<VideoMomentCaptureViewModel> VideoMomentCaptureList { get; }
        public ObservableCollection<FilePathPair> Videos { get; }

        public MainWindowViewModel(IGetServicesScope getServicesScope,
            IVideoMetdataController videoMetdataController,
            IVideoMomentController videoMomentController,
            IVideoMomentCaptureController videoMomentCaptureController,
            IVideoToolsController videoToolsController,
            MediaPlayerViewModel mediaPlayerViewModel)
        {
            this.getServicesScope = getServicesScope;
            this.videoMetdataController = videoMetdataController;
            this.videoMomentController = videoMomentController;
            this.videoMomentCaptureController = videoMomentCaptureController;
            this.videoToolsController = videoToolsController;

            MediaPlayerViewModel = mediaPlayerViewModel;

            VideoMomentCapture = new VideoMomentCaptureDto();
            MediaPlayerViewModel.CurrentPosition.PropertyChanged += (sender, args) =>
                VideoMomentCapture.CapturedTime = (long)MediaPlayerViewModel.CurrentPosition.Value;

            CurrentFilePath = new ObserverableProperty<FilePathPair>(x => LoadVideo(x.FilePath));
            AbleToLoadViewPreview = new ObserverableProperty<bool>();

            AddCodeCommand = new Command(x => { AddCode(); });

            OpenFileCommand = new Command(x => { LoadVideos(); AbleToLoadViewPreview.Value = true; });
            PreviewFileCommand = new Command(x => { LoadFrames(); });

            ClipVideoCommand = new Command(async x => { await ClipVideoAsync(); });

            VideoMomentList = new ObservableCollection<VideoMomentViewModel>();
            VideoMomentCaptureList = new ObservableCollection<VideoMomentCaptureViewModel>();
            Videos = new ObservableCollection<FilePathPair>();

            LoadCodes().Wait();
        }

        private void LoadVideoMetadata(string fileName)
        {
            videoMetadata = videoMetdataController.GetVideoMetadataAsync(fileName).Result;

            if (videoMetadata == null)
                videoMetadata = videoMetdataController.CreateVideoMetadataAsync(fileName).Result;

            VideoMomentCapture.VideoMetadata = videoMetadata;
            VideoMomentCapture.VideoMetadataGuid = videoMetadata.VideoMetadataDataGuid;

            LoadVideoMoments(videoMetadata.VideoMomentCaptureList);
        }

        private void LoadVideoMomentCapture(long time)
        {
            MediaPlayerViewModel.CurrentPosition.Value = time;
        }

        private async Task LoadCodes()
        {
            var scope = getServicesScope.BeginScope();
            var videoCodes = await videoMomentController.GetVideoMomentListAsync();
            videoCodes = videoCodes.OrderBy(x => x.VideoMomentName);

            foreach (VideoMomentDto videoMomentDto in videoCodes)
            {
                VideoMomentViewModel codeViewModel = scope.GetRequiredService<VideoMomentViewModel>();
                codeViewModel.LoadVideoMoment(videoMomentDto);
                codeViewModel.NewVideoMomentCaptureEvent += videoMomentCaptureDto =>
                {
                    AddVideoMomentToView(videoMomentCaptureDto);
                    videoMetadata.VideoMomentCaptureList.Add(videoMomentCaptureDto);
                };
                int elementIndex = SortedListHelper.GetNewElementIndex(
                    VideoMomentList, codeViewModel, x => x.VideoMomentName);
                VideoMomentList.Insert(elementIndex, codeViewModel);
            }
        }

        private void AddVideoMomentToView(VideoMomentCaptureDto videoMomentCaptureDto)
        {
            if (videoMetadata == null)
                return;

            VideoMomentCaptureViewModel videoMomentCaptureViewModel = new VideoMomentCaptureViewModel(videoMomentCaptureDto);
            videoMomentCaptureViewModel.CpaturedVideoMomentClicked += LoadVideoMomentCapture;
            VideoMomentCaptureList.Add(videoMomentCaptureViewModel);
        }

        private void LoadVideoMoments(List<VideoMomentCaptureDto> videoMomentDtos)
        {
            VideoMomentCaptureList.Clear();
            videoMomentDtos?.ForEach(x => AddVideoMomentToView(x));
        }

        private void LoadVideo(string filePath)
        {
            MediaPlayerViewModel.LoadMedia(new Uri(filePath));
            MediaPlayerViewModel.PlayCommand.Execute(null);

            LoadVideoMetadata(Path.GetFileName(filePath));
        }

        private void LoadVideos()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                CurrentFilePath.Value = GetFilePathPair(dialog.FileNames.FirstOrDefault());

                foreach (string filePath in dialog.FileNames)
                    Videos.Add(new FilePathPair(Path.GetFileName(filePath), filePath));

                LoadVideo(CurrentFilePath.Value.FilePath);
            }
        }

        void LoadFrames()
        {
            FramePreviewWindow framePreviewWindow = new FramePreviewWindow(getServicesScope.BeginScope(), CurrentFilePath.Value.FilePath);
            framePreviewWindow.Show();
            MediaPlayerViewModel.PauseCommand?.Execute(null);
        }

        private async Task ClipVideoAsync()
        {
            if (videoMetadata == null)
                return;

            var videoMomentCaptureList = videoMetadata.VideoMomentCaptureList;
            videoToolsController.ClipVideo(CurrentFilePath.Value.FilePath, videoMomentCaptureList);
            await videoMetdataController.UpdateLastClippedDateAsync(videoMetadata.VideoMetadataDataGuid);
        }

        private void AddCode()
        {
            var videoMoment = videoMomentController.CreateVideoMomentAsync(NewVideoMomentName).Result;

            var scope = getServicesScope.BeginScope();

            VideoMomentViewModel codeViewModel = scope.GetRequiredService<VideoMomentViewModel>();
            codeViewModel.LoadVideoMoment(videoMoment);
            codeViewModel.NewVideoMomentCaptureEvent += AddVideoMomentToView;

            int elementIndex = SortedListHelper.GetNewElementIndex(
                VideoMomentList, codeViewModel, x => x.VideoMomentName);
            VideoMomentList.Insert(elementIndex, codeViewModel);
        }
        private FilePathPair GetFilePathPair(string filePath)
        {
            return new FilePathPair(Path.GetFileName(filePath), filePath);
        }
    }
}
