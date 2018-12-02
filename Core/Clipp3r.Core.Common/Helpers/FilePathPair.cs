using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JohnnysLibrary.WPF
{
    public class FilePathPair : INotifyPropertyChanged
    {
        string _displayText;
        string _filePath;

        public string DisplayText { get { return _displayText; } set { _displayText = value; OnPropertyChanged(); } }
        public string FilePath { get { return _filePath; } set { _filePath = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public FilePathPair() { }
        public FilePathPair(string displayText, string filePath)
        {
            DisplayText = displayText;
            FilePath = filePath;
        }

        private void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
