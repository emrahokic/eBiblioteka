using System;
using System.Windows.Input;

namespace eBiblioteka.MVVM
{
    public class ActionCommand : ICommand
    {
        private readonly Action<Object> _action;
        private readonly Predicate<Object> _predicate;
        public ActionCommand(Action<Object> action) : this(action,null)
        {
        }
        public ActionCommand(Action<Object> action,Predicate<Object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "You must specify an Action<T>.");
            }

            this._action = action;
            this._predicate = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_predicate == null)
            {
                return true;
            }
            return _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
        public void Execute()
        {
            Execute(null);
        }
    }
}
