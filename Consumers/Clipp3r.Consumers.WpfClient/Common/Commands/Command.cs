using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clipp3r.Common.Commands
{
    /// <summary>
    /// Contains the implemention of the ICommand interface which allows Command binding in XAML
    /// </summary>
     class Command : ICommand
    {
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }
        
        public Command(Action<object> executeDelegate)
        {
            ExecuteDelegate = executeDelegate;
        }

        public Command(Func<object, bool, Task> executeDelegate)
        {
            ExecuteDelegate = async (o) => await executeDelegate(o, true);
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }
    }
}
