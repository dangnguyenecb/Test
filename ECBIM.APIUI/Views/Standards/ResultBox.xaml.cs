using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ECBIM.APIUI
{
    /// <summary>
    /// Logique d'interaction pour ResultBox.xaml
    /// </summary>
    public partial class ResultBox : Window, INotifyPropertyChanged
    {
        private string _customTitle;
        private string _mainContent;

        public string CustomTitle
        {
            get
            {
                return _customTitle;
            }
            set
            {
                _customTitle = value;
                OnPropertyChanged();
            }
        }
        public string MainContent
        {
            get => _mainContent;
            set
            {
                _mainContent = value;
                OnPropertyChanged();
            }
        }

        public ResultBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, MainTextBox.SelectedText);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {

        }

        private void BtnCopyAll_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, MainContent);
            
        }
    }
}
