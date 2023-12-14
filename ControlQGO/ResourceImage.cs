using System;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;


namespace ControlQGO
{
    public static class ResourceImage
    {
        private static readonly string _CommandIconsPath = "CommandIcons";
        private static readonly string _CommandLargeIconsPath = "CommandLargeIcons";

        public static BitmapImage GetIcon(string imageName)
        {

            string assemblyFolderPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string resourcesPath = System.IO.Path.Combine(assemblyFolderPath, "Resources");
            string iconPath = System.IO.Path.Combine(resourcesPath, _CommandIconsPath, imageName);
            BitmapImage icon = new BitmapImage(new Uri(iconPath));

            return icon;
        }

        public static BitmapImage GetLargeIcon(string imageName)
        {

            string _AssemblyFolderPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string _ResourcesPath = System.IO.Path.Combine(_AssemblyFolderPath, "Resources");
            string iconPath = System.IO.Path.Combine(_ResourcesPath, _CommandLargeIconsPath, imageName);
            BitmapImage icon = new BitmapImage(new Uri(iconPath));

            return icon;
        }

        public static string GetAssembly()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
