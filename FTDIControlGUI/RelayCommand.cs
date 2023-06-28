using System;
using System.Windows.Input;

namespace MVVM
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        Action<T> executeTargets = delegate { };
        Func<bool> canExecuteTargets = delegate { return false; };

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return (_canExecute == null) || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public event Action<T> ExecuteTargets
        {
            add
            {
                executeTargets += value;
            }
            remove
            {
                executeTargets -= value;
            }
        }
        public event Func<bool> CanExecuteTargets
        {
            add
            {
                canExecuteTargets += value;
            }
            remove
            {
                canExecuteTargets -= value;
            }
        }


    }
}