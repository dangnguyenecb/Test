using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using ECBIM.API.Extensions;
using System.Text;
using ECBIM.API;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using System;

namespace ECBIM.API.Utilities
{
    public static class RevitUtilities
    {
        /// <summary>
        /// Appliquer les valeurs de paramètres d'un élement de référence à des éléments cible
        /// </summary>
        /// <param name="reference">Element de référence</param>
        /// <param name="targets">Liste des éléments cibles</param>
        /// <param name="ignoredBuiltInParameters">Liste de BuiltInParameters à ignorer</param>
        /// <param name="ignoredParameters">Liste de paramètres à ignorer</param>
        public static void ExtendParameterValues(Element reference, IEnumerable<Element> targets, List<BuiltInParameter> ignoredBuiltInParameters = null, List<string> ignoredParameters = null)
        {
            Dictionary<Definition, object> referenceParameters = reference.GetOrderedParametersValues();
            ignoredParameters = ignoredParameters ?? new List<string>();
            ignoredBuiltInParameters = ignoredBuiltInParameters ?? new List<BuiltInParameter>();

            foreach (var kvp in referenceParameters)
            {
                InternalDefinition intDef = kvp.Key as InternalDefinition;
                if (ignoredBuiltInParameters.Contains(intDef.BuiltInParameter))
                {
                    ignoredParameters.Add(intDef.Name);
                }
            }

            if (ignoredParameters.Count > 0)
            {
                Dictionary<Definition, object> newDict = referenceParameters.Where(kvp => !ignoredParameters.Contains(kvp.Key.Name))
                                                                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                SetOrderedParametersValues(targets, newDict);
            }
            else
            {
                SetOrderedParametersValues(targets, referenceParameters);
            }
        }

        public static void ExtendParameterValues(Element reference, IEnumerable<Element> targets, List<Guid> parametersGuid)
        {
            foreach (Element element in targets)
            {
                ExtendParameterValues(reference, element, parametersGuid);
            }
/*                foreach (Guid guid in parametersGuid)
                {
                    Parameter refParameter = reference.get_Parameter(guid);


                    switch (refParameter.StorageType)
                    {
                        case StorageType.ElementId:
                            element.get_Parameter(guid).Set(refParameter.AsElementId());
                            break;
                        case StorageType.Integer:
                            element.get_Parameter(guid).Set(refParameter.AsInteger());
                            break;
                        case StorageType.Double:
                            element.get_Parameter(guid).Set(refParameter.AsDouble());
                            break;
                        case StorageType.String:
                            element.get_Parameter(guid).Set(refParameter.AsString());
                            break;
                    }
                }
            }*/
        }

        public static void ExtendParameterValues(Element reference, Element target, List<Guid> parametersGuid)
        {
            foreach (Guid guid in parametersGuid)
            {
                Parameter refParameter = reference.get_Parameter(guid);

                switch (refParameter.StorageType)
                {
                    case StorageType.ElementId:
                        target.get_Parameter(guid).Set(refParameter.AsElementId());
                        break;
                    case StorageType.Integer:
                        target.get_Parameter(guid).Set(refParameter.AsInteger());
                        break;
                    case StorageType.Double:
                        target.get_Parameter(guid).Set(refParameter.AsDouble());
                        break;
                    case StorageType.String:
                        target.get_Parameter(guid).Set(refParameter.AsString());
                        break;
                }
            }
        }


        /// <summary>
        /// Appliquer des valeurs de paramètres à des éléments à partir d'un dictionnaire
        /// </summary>
        /// <param name="targets">Liste des éléments cibles</param>
        /// <param name="parameterValues">Dictionnaire des paramètres</param>
        public static void SetOrderedParametersValues(IEnumerable<Element> targets, Dictionary<Definition, object> parameterValues)
        {
            foreach (Element el in targets)
            {
                foreach (KeyValuePair<Definition, object> kvp in parameterValues)
                {
                    Definition definition = kvp.Key;
                    var value = kvp.Value;
                    try
                    {
                        Parameter param = el.get_Parameter(definition);

                        switch (param.StorageType)
                        {
                            case StorageType.ElementId:
                                param.Set(value as ElementId);
                                break;
                            case StorageType.Integer:
                                param.Set((int)value);
                                break;
                            case StorageType.Double:
                                param.Set((double)value);
                                break;
                            case StorageType.String:
                                param.Set(value as string);
                                break;
                        }
                    }
                    catch { }
                }
            }
        }

        public static string OpenExcelDocument(UIApplication UiApplication)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel (*.xls)|*.xls;*.xlsx;*.xlsm",
                InitialDirectory = UiApplication.ActiveUIDocument.Document.PathName
            };

            if (openFileDialog.ShowDialog().Value)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        public static Document OpenRevitDocument(UIApplication UiApplication)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Revit files (*.rvt)|*.rvt",
                InitialDirectory = UiApplication.ActiveUIDocument.Document.PathName
            };

            if (openFileDialog.ShowDialog().Value)
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

        public static UIDocument OpenRevitDocumentInUi(UIApplication UiApplication)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Revit files (*.rvt)|*.rvt",
                InitialDirectory = UiApplication.ActiveUIDocument.Document.PathName
            };

            if (openFileDialog.ShowDialog().Value)
            {
                ModelPath filePath = ModelPathUtils.ConvertUserVisiblePathToModelPath(openFileDialog.FileName);
                OpenOptions options = new OpenOptions()
                {
                    DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets,
                };

                return UiApplication.OpenAndActivateDocument(filePath, options, true);
            }

            return null;
        }
    }
}
