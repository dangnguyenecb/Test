using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using ECBIM.APIUI;
using ControlQGO.Views;

namespace ControlQGO.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    internal class AboutCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            ExempleEiffageView form = new ExempleEiffageView(commandData);

            form.ShowDialog();

            //CustomDialog dcd = new CustomDialog("Fenêtre perso")
            //{
            //    MainInstruction = "Instruction principale",
            //    MainIcon = Icon.Valid,
            //    MainContent = "Contenu de la fenêtre",
            //    Buttons = Button.Yes | Button.No
            //};

            //var result = dcd.ShowCustomDialog();

            //if (result == CustomDialogResult.Yes)
            //{
            //    ConsoleAPI.WriteLine("Résultat : Oui");
            //}
            //else
            //{
            //    ConsoleAPI.WriteLine("Résultat : Non");
            //}

            ConsoleAPI.Dispose();
            return Result.Succeeded;
        }
    }
}
