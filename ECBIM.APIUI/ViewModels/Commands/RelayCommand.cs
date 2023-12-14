using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECBIM.APIUI
{
    public class RelayCommand : ICommand
    {
        public delegate void ICommandOnExecute(object parameter);
        public delegate bool ICommmandOnCanExecute(object parameter);

        private ICommandOnExecute _execute;
        private ICommmandOnCanExecute _canExecute;

        public RelayCommand(ICommandOnExecute onExecuteMethod, ICommmandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        public RelayCommand(ICommandOnExecute onExecuteMethod) : this(onExecuteMethod, null)
        {
            _execute = onExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
