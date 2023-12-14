using System;
using System.Reflection;
using System.IO;
using System.Windows;

namespace ECBIM.API.Utilities
{
    public class Locator
    {
        private string _path;

        public Locator()
        {

        }

        public void InstallLocator(string path)
        {
            _path = path;
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
        }

        private Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            int position = args.Name.IndexOf(",");
            if (position > -1)
            {
                try
                {
                    string assemblyName = args.Name.Substring(0, position);
                    string assemblyFullPath = string.Empty;

                    //look in main folder
                    assemblyFullPath = _path + "\\" + assemblyName + ".dll";

                    if (File.Exists(assemblyFullPath))
                    {                        
                        return Assembly.LoadFrom(assemblyFullPath);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return null;
        }
    }
}
