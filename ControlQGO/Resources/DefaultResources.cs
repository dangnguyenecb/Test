using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Eiffage.RevitPlugin.Debug.Host.Resources
{
    public static class DefaultResources
    {
        private static string _commandIconsPath = "CommandIcons";
        public static string _assemblyFolderPath { get { return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }
        private static string _resourcesPath { get { return System.IO.Path.Combine(_assemblyFolderPath, "Resources"); } }
        private static string AboutIconPath => System.IO.Path.Combine(_resourcesPath, _commandIconsPath, "About.png");

        public static BitmapImage AboutIcon { get { return new BitmapImage(new Uri(DefaultResources.AboutIconPath)); } }

    }
}
