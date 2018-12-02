using Clipp3r.ViewModels;
using System.Windows;

namespace Clipp3r
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)DataContext;
            mainWindowViewModel.MediaPlayerViewModel.MediaPlayer = mediaPlayer;
            mainWindowViewModel.MediaPlayerViewModel.MediaPlayerProgressBar = sliProgress;
        }
    }
}
