using Clipp3r.Core.Common;
using Clipp3r.ViewModels;
using System.Windows;

namespace Clipp3r.Views
{
    /// <summary>
    /// Interaction logic for FramePreviewWindow.xaml
    /// </summary>
    public partial class FramePreviewWindow : Window
    {
        public FramePreviewWindow(IGetServices getServices, string filePath)
        {
            FramePreviewViewModel viewModel = getServices.GetRequiredService<FramePreviewViewModel>();
            viewModel.LoadVideoFile(filePath);
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
