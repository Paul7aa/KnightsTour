using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Knights_Tour.Commands
{
    public class AsyncRelayCommand : ICommand
    {
        readonly Func<object,Task> _execute;
        readonly Predicate<object> _canExecute;

        public AsyncRelayCommand(Func<object,Task> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            await _execute(parameter);
        }
    }

}
