using System;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ControlQGO.ViewModels;

namespace ControlQGO.Views
{
    /// <summary>
    /// Logique d'interaction pour ExempleFormulaireEiffage.xaml
    /// </summary>
    public partial class ExempleEiffageView : Window
    {
        public ExempleEiffageViewModel ViewModel { get; set; }

        public ExempleEiffageView(ExternalCommandData commandData)
        {
            InitializeComponent();
            this.DataContext = new ExempleEiffageViewModel(commandData);
            ViewModel = this.DataContext as ExempleEiffageViewModel;
        }
    }
}
