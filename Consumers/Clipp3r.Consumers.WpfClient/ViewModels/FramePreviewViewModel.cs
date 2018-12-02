using Clipp3r.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Clipp3r.ViewModels
{
    [RegisterService(serviceLifetime: ServiceLifetime.Transient)]
    class FramePreviewViewModel
    {
        #region Members
        string _filePath;

        // Member variables
        private MediaPlayer _mediaPlayer = new MediaPlayer();
        private Queue<TimeSpan> _positionsToThumbnail = new Queue<TimeSpan>();
        private DispatcherTimer _watchdogTimer = new DispatcherTimer();
        private uint[] framePixels;
        private uint[] previousFramePixels;
        #endregion

        #region Properties
        public ObservableCollection<ImageSource> Frames { get; set; }
        
        #endregion

        public void LoadVideoFile(string filePath)
        {
            _filePath = filePath;
            Frames = new ObservableCollection<ImageSource>();
            _mediaPlayer.MediaOpened += new EventHandler(HandleMediaPlayerMediaOpened);
            _mediaPlayer.Changed += new EventHandler(HandleMediaPlayerChanged);
            _watchdogTimer.Interval = TimeSpan.FromMilliseconds(10);
            _watchdogTimer.Tick += new EventHandler(HandleWatchdogTimerTick);

            LoadFrames(filePath);
        }

        private void LoadFrames(string filePath)
        {
            Frames.Clear();
            // Open media file
            _mediaPlayer.ScrubbingEnabled = true;
            _mediaPlayer.Stop();
            _mediaPlayer.Open(new Uri(filePath));
            _mediaPlayer.Volume = 0;
        }

        private void HandleMediaPlayerMediaOpened(object sender, EventArgs e)
        {
            // Get details about opened file
            int numberFramesToThumbnail = (15 * 100);
            double totalMilliseconds = _mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
            framePixels = new uint[_mediaPlayer.NaturalVideoWidth * _mediaPlayer.NaturalVideoHeight];
            previousFramePixels = new uint[framePixels.Length];

            // Enqueue a position for each frame (at the center of each of the N segments)
            for (int i = 0; i < numberFramesToThumbnail; i++)
            {
                _positionsToThumbnail.Enqueue(TimeSpan.FromMilliseconds((((2 * i) + 1) * totalMilliseconds) / (2 * numberFramesToThumbnail)));
            }

            // Capture the first frame as a baseline
            RenderBitmapAndCapturePixels(previousFramePixels);

            // Seek to the first thumbnail position
            SeekToNextThumbnailPosition();
        }

        private void HandleMediaPlayerChanged(object sender, EventArgs e)
        {
            CaptureCurrentFrame(false);
        }

        private void SeekToNextThumbnailPosition()
        {
            // If more frames remain to capture...
            if (0 < _positionsToThumbnail.Count)
            {
                // Seek to next position and start watchdog timer
                _mediaPlayer.Position = _positionsToThumbnail.Dequeue();
                _watchdogTimer.Start();
            }
            else
            {
                // Done; close media file and stop processing
                _mediaPlayer.Close();
                framePixels = null;
                previousFramePixels = null;
                //SetProcessing(this, false);
            }
        }

        private void CaptureCurrentFrame(bool forceCapture)
        {
            // Capture the current frame as an ImageSource
            var imageSource = RenderBitmapAndCapturePixels(framePixels);
            imageSource.Freeze();

            // If captured pixels are different than the previous frame...
            if (forceCapture || !framePixels.SequenceEqual(previousFramePixels))
            {
                // Stop the watchdog timer
                _watchdogTimer.Stop();

                Image image =
                    new Image
                    {
                        Source = imageSource,
                        ToolTip = _mediaPlayer.Position,
                        MaxWidth = _mediaPlayer.NaturalVideoWidth,
                        MaxHeight = _mediaPlayer.NaturalVideoHeight,
                        Margin = new Thickness(2)
                    };
                // Add an Image for the Thumbnail
                Frames.Add(image.Source);

                // Swap the pixel buffers (moves current to previous and avoids allocating a new buffer for current)
                var tempPixels = framePixels;
                framePixels = previousFramePixels;
                previousFramePixels = tempPixels;

                // Seek to the next thumbnail position
                SeekToNextThumbnailPosition();
            }
        }

        private void HandleWatchdogTimerTick(object sender, EventArgs e)
        {
            // Stop the watchdog timer
            _watchdogTimer.Stop();

            // Capture the current frame (even if it's not different than the previous)
            CaptureCurrentFrame(true);
        }

        private ImageSource RenderBitmapAndCapturePixels(uint[] pixels)
        {
            // Render the current frame into a bitmap
            var drawingVisual = new DrawingVisual();
            var renderTargetBitmap = new RenderTargetBitmap(_mediaPlayer.NaturalVideoWidth, _mediaPlayer.NaturalVideoHeight, 96, 96, PixelFormats.Default);
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawVideo(_mediaPlayer, new Rect(0, 0, _mediaPlayer.NaturalVideoWidth, _mediaPlayer.NaturalVideoHeight));
            }
            renderTargetBitmap.Render(drawingVisual);

            // Copy the pixels to the specified location
            renderTargetBitmap.CopyPixels(pixels, _mediaPlayer.NaturalVideoWidth * 4, 0);

            // Return the bitmap
            return renderTargetBitmap;
        }
    }
}
