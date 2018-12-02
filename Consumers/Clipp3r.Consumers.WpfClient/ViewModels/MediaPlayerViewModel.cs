using Clipp3r.Common.Commands;
using Clipp3r.Core.Common;
using JohnnysLibrary.WPF;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Clipp3r.Consumers.WpfClient
{
    [RegisterService]
    class MediaPlayerViewModel
    {
        private bool _sliderIsBeingMoved;

        public bool SliderIsBeingMoved
        {
            get { return _sliderIsBeingMoved; }
            set
            {
                _sliderIsBeingMoved = value;
            }
        }

        public Command BackwardCommand { get; }
        public Command PlayCommand { get; }
        public Command PauseCommand { get; }
        public Command StopCommand { get; }
        public Command ForwardCommand { get; }

        public Command MouseWheelEventHandler { get; set; }

        public Command SliderDragStartedCommand { get;  }
        public Command SliderDragCompletedCommand { get; }

        public MediaElement MediaPlayer { get; set; }
        public Slider MediaPlayerProgressBar { get; set; }

        public string Adjuster
        {
            get { return (_Adjuster / 10000).ToString(); }
            set { _Adjuster = Convert.ToInt64(value) * 10000; }
        }
        private long _Adjuster;

        public ObserverableProperty<string> PositionDisplay { get; }
        public ObserverableProperty<double> CurrentPosition { get; }
        public ObserverableProperty<double> Volume { get; }

        public MediaPlayerViewModel()
        {
            PositionDisplay = new ObserverableProperty<string>();
            PositionDisplay.Value = "00:00:00";
            CurrentPosition = new ObserverableProperty<double>();
            Volume = new ObserverableProperty<double>();

            Adjuster = "2500";

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            BackwardCommand = new Command(x =>
            {
                MediaPlayer.Position -= new TimeSpan(_Adjuster);
            });

            PlayCommand = new Command(x =>
            {
                MediaPlayer.Play();
            });

            PauseCommand = new Command(x =>
            {
                MediaPlayer.Pause();
            });

            StopCommand = new Command(x =>
            {
                MediaPlayer.Stop();
            });

            ForwardCommand = new Command(x =>
            {
                MediaPlayer.Position += new TimeSpan(_Adjuster);
            });

            CurrentPosition.PropertyChanged += (sneder, args) =>
            {
                var mediaPlayerTime = MediaPlayer.Position;
                var currentPosition = TimeSpan.FromMilliseconds(CurrentPosition.Value);
                var timeDifference = currentPosition - mediaPlayerTime;

                var absoluteTimeDifferenceInSeconds = Math.Abs(timeDifference.Seconds);

                if (absoluteTimeDifferenceInSeconds > 1)
                {
                    if (!SliderIsBeingMoved)
                        MediaPlayer.Position = currentPosition;
                }

                PositionDisplay.Value = mediaPlayerTime.ToString(@"hh\:mm\:ss");
            };
            MouseWheelEventHandler = new Command(x =>
            {
                if (x != null)
                    MediaPlayer.Volume += (((MouseWheelEventArgs)x).Delta > 0) ? 0.1 : -0.1;
            });

            SliderDragStartedCommand = new Command(x => SliderIsBeingMoved = true);
            SliderDragCompletedCommand = new Command(x => SliderIsBeingMoved = false);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((MediaPlayer.Source != null) && (MediaPlayer.NaturalDuration.HasTimeSpan))
            {
                MediaPlayerProgressBar.Minimum = 0;
                MediaPlayerProgressBar.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                MediaPlayerProgressBar.Value = MediaPlayer.Position.TotalMilliseconds;
            }
        }

        public void LoadMedia(Uri sourceUri)
        {
            MediaPlayer.Source = sourceUri;
        }
    }
}
