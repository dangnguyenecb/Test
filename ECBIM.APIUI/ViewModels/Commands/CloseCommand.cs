using System;
using System.Windows.Input;

namespace ECBIM.APIUI
{
    public class CloseCommand : ICommand
    {
        private ViewModelBase _viewModel;

        public CloseCommand(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.Cancel();
        }
    }
}
