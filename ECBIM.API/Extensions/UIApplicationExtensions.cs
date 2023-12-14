using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
//using System.Windows.Forms;
using Microsoft.Win32;

namespace ECBIM.API.Extensions
{
    public static class UIApplicationExtensions
    {
        public static Document OpenRevitDocument(this UIApplication UiApplication)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Revit files (*.rvt)|*.rvt",
                InitialDirectory = UiApplication.ActiveUIDocument.Document.PathName
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ModelPath filePath = ModelPathUtils.ConvertUserVisiblePathToModelPath(openFileDialog.FileName);
                OpenOptions options = new OpenOptions()
                {
                    DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets,
                };
                return UiApplication.Application.OpenDocumentFile(filePath, options);                
            }
            else
            {
                return null;
            }
        }
    }
}
