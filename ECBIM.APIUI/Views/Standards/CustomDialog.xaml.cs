using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ECBIM.APIUI
{
    /// <summary>
    /// Logique d'interaction pour CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window, INotifyPropertyChanged
    {
        private Button buttons;
        private bool buttonCancel;
        private bool buttonNo;
        private bool buttonOk;
        private bool buttonYes;
        private CustomDialogResult _dialogResult;
        private string _dialogTitle;
        private string iconStatut;
        private string _mainContent;
        private Icon mainIcon;
        private string mainInstruction;

        public Button Buttons { get => buttons; set { buttons = value; OnPropertyChanged(); SetButtonVisibility(); } }
        public bool ButtonCancel { get => buttonCancel; set { buttonCancel = value; OnPropertyChanged(); } }
        public bool ButtonNo { get => buttonNo; set { buttonNo = value; OnPropertyChanged(); } }
        public bool ButtonOk { get => buttonOk; set { buttonOk = value; OnPropertyChanged(); } }
        public bool ButtonYes { get => buttonYes; set { buttonYes = value; OnPropertyChanged(); } }
        public new CustomDialogResult DialogResult { get { return _dialogResult; } set { _dialogResult = value; OnPropertyChanged(); } }
        public string DialogTitle { get => _dialogTitle; set { _dialogTitle = value; OnPropertyChanged(); } }
        public string IconStatut { get => iconStatut; set { iconStatut = value; OnPropertyChanged(); } }
        public string MainContent { get => _mainContent; set { _mainContent = value; OnPropertyChanged(); } }
        public Icon MainIcon { get => mainIcon; set { mainIcon = value; OnPropertyChanged(); DisplayedIcon(); } }
        public string MainInstruction { get => mainInstruction; set { mainInstruction = value; OnPropertyChanged(); } }
        public CustomDialogResult Result { get; set; }
        public RelayCommand ResultCommand { get; set; }

        public CustomDialog(string title)
        {
            InitializeComponent();
            DialogTitle = title;
            this.DataContext = this;
            ResultCommand = new RelayCommand(CommandResult);
        }

        private void CommandResult(object parameter)
        {
            //string button = (string)parameter;
            CustomDialogResult result = (CustomDialogResult)Enum.Parse(typeof(CustomDialogResult), (string)parameter);

            switch (result)
            {
                case CustomDialogResult.OK:
                    Result = CustomDialogResult.OK;
                    break;
                case CustomDialogResult.Cancel:
                    Result = CustomDialogResult.Cancel;
                    break;
                case CustomDialogResult.Yes:
                    Result = CustomDialogResult.Yes;
                    break;
                case CustomDialogResult.No:
                    Result = CustomDialogResult.No;
                    break;
            }

            this.Close();
        }

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

        public CustomDialogResult ShowCustomDialog()
        {
            this.ShowDialog();
            return Result;
        }

        #region INotifyPropertyChange Implementation
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
