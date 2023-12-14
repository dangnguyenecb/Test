using System.Reflection;
using System.Windows;
using Autodesk.Revit.UI;
using ECBIM.API.Utilities;
using ControlQGO.Commands;

namespace ControlQGO
{
    public static class InterfaceSetup
    {
        private static string ThisAssemblyPath { get; } = Assembly.GetExecutingAssembly().Location;

        public static void Initialize(UIControlledApplication application, string tabName)
        {
            RibbonPanel aboutPanel = application.CreateRibbonPanel(tabName, "A propos");
            aboutPanel.AddAboutButtons();
        }

        private static void AddAboutButtons(this RibbonPanel panel)
        {
            PushButtonData aboutData = new PushButtonData("ShowAboutCommand", "A propos", ThisAssemblyPath, typeof(AboutCommand).ToString())
            {
                LargeImage = ResourceImage.GetLargeIcon("A_propos.png")              
            };
            panel.AddItem(aboutData);
        }
    }
}
