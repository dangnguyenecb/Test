using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECBIM.APIUI
{
    public  class CustomDialogViewModel : ViewModelBase
    {
        #region Local Variables
        private CustomDialogResult _dialogResult;
        private Button buttons;
        private string _dialogTitle;
        private string _mainContent;
        private string mainInstruction;
        private Icon mainIcon;
        private string iconStatut;
        private bool buttonNo;
        private bool buttonYes;
        private bool buttonOk;
        private bool buttonCancel;
        #endregion

        #region Properties
        public Button Buttons { get => buttons; set { SetProperty(ref buttons, value); SetButtonVisibility(); } }
        public bool ButtonCancel { get => buttonCancel; set { SetProperty(ref buttonCancel, value); } }
        public bool ButtonNo { get => buttonNo; set { SetProperty(ref buttonNo, value); } }
        public bool ButtonOk { get => buttonOk; set { SetProperty(ref buttonOk, value); } }
        public bool ButtonYes { get => buttonYes; set { SetProperty(ref buttonYes, value); } }
        public new CustomDialogResult DialogResult { get { return _dialogResult; } set { SetProperty(ref _dialogResult, value); } }
        public string DialogTitle { get => _dialogTitle; set { SetProperty(ref _dialogTitle, value); } }
        public string IconStatut { get => iconStatut; set { SetProperty(ref iconStatut, value); } }
        public string MainContent { get => _mainContent; set { SetProperty(ref _mainContent, value); } }
        public Icon MainIcon { get => mainIcon; set { SetProperty(ref mainIcon, value); DisplayedIcon(); } }
        public string MainInstruction { get => mainInstruction; set { SetProperty(ref mainInstruction, value); } }
        public CustomDialogResult Result { get; set; }

        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand YesCommand { get; set; }
        public DelegateCommand NoCommand { get; set; }
        #endregion

        #region Constructor
        public CustomDialogViewModel(string title)
        {
            DialogTitle = title;
            OkCommand = new DelegateCommand(OkCommandResult);
            CancelCommand = new DelegateCommand(CancelCommandResult);
            YesCommand = new DelegateCommand(YesCommandResult);
            NoCommand = new DelegateCommand(NoCommandResult);
        }
        #endregion

        #region Methods
        private string DisplayedIcon()
        {
            switch (MainIcon)
            {
                case APIUI.Icon.Valid:
                    IconStatut = "/ECBIM_APIUI;component/ResourcesAPI/EC_Valid.png";
                    break;
                case APIUI.Icon.Error:
                    IconStatut = "/ECBIM_APIUI;component/ResourcesAPI/EC_Error.png";
                    break;
                case APIUI.Icon.Warning:
                    IconStatut = "/ECBIM_APIUI;component/ResourcesAPI/EC_Warning.png";
                    break;
            }

            return string.Empty;
        }

        private void SetButtonVisibility()
        {
            ButtonOk = (Buttons & Button.Ok) == Button.Ok;
            ButtonCancel = (Buttons & Button.Cancel) == Button.Cancel;
            ButtonYes = (Buttons & Button.Yes) == Button.Yes;
            ButtonNo = (Buttons & Button.No) == Button.No;
        }

        private void OkCommandResult()
        {
            Result = CustomDialogResult.OK;
            Application.Current.MainWindow.Close();
            //window.Close();
        }

        public override void Validation(object parameter)
        {
            base.Validation(parameter);
        }

        private void YesCommandResult()
        {
            Result = CustomDialogResult.Yes;
            Application.Current.MainWindow.Close();
        }
        private void CancelCommandResult()
        {
            Result = CustomDialogResult.Cancel;
            Application.Current.MainWindow.Close();
        }

        private void NoCommandResult()
        {
            Result = CustomDialogResult.No;
            Application.Current.MainWindow.Close();
        }

        #endregion
    }
}
