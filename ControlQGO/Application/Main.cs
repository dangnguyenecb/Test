using Autodesk.Revit.UI;
using Eiffage.RevitPlugin.Debug.Host.Resources;
using System.Reflection;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
using ECBIM.API.Utilities;
using System.IO;
using System.Windows;

namespace ControlQGO
{
    public class Main : IExternalApplication
    {
        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            Locator loc = new Locator();
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            loc.InstallLocator(currentPath);

            //string assembly = Assembly.GetExecutingAssembly().Location;
            var tabName = "Control QGO";
            application.CreateRibbonTab(tabName);

            InterfaceSetup.Initialize(application, tabName);

            return Result.Succeeded;
        }

        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}