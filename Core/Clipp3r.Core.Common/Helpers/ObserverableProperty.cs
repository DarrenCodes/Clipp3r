using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JohnnysLibrary.WPF
{
    public class ObserverableProperty<T> : INotifyPropertyChanged
    {
        Action<T> operation;
        T _value;
        public T Value { get { return _value; } set { _value = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialises a new instance of ObserverableProperty type
        /// </summary>
        public ObserverableProperty() { }

        /// <summary>
        /// Initialises a new instance of ObserverableProperty type with operation that should be executed when the property is changed
        /// </summary>
        /// <param name="operation">Alternative way of subscribing to this type's property changed event</param>
        public ObserverableProperty(Action<T> operation) { this.operation = operation; }

        private void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            ExecuteOnPropertyChanged();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private void ExecuteOnPropertyChanged()
        {
            operation?.Invoke(Value);
        }
    }
}
