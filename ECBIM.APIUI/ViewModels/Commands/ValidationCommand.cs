using System;
using System.Windows.Input;

namespace ECBIM.APIUI
{
    internal class ValidationCommand : ICommand
    {
        private ViewModelBase _viewModel;

        public ValidationCommand(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _viewModel.Validation(parameter);
        }
    }
}
