using System.Windows.Input;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;

namespace ECBIM.APIUI
{
    public abstract class ViewModelBase : BindingBase
    {
        private bool? _dialogResult;
        public bool? DialogResult { get { return _dialogResult; } set { this.SetProperty(ref _dialogResult, value); } }
        public UIApplication UIApp { get; set; }
        public Application App { get; set; }
        public UIDocument UIDoc { get; set; }
        public Document Doc { get; set; }
        public ICommand ValidationCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        protected ViewModelBase(ExternalCommandData commandData)
        {
            UIApp = commandData.Application;
            App = commandData.Application.Application;
            UIDoc = commandData.Application.ActiveUIDocument;
            Doc = commandData.Application.ActiveUIDocument.Document;

            ValidationCommand = new ValidationCommand(this);
            CloseCommand = new CloseCommand(this);
        }

        protected ViewModelBase()
        {
            ValidationCommand = new ValidationCommand(this);
            CloseCommand = new CloseCommand(this);
        }


        public virtual void Validation(object parameter)
        {
            DialogResult = true;
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Cancel()
        {
            DialogResult = false;
        }

    }
}

