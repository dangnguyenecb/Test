using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ECBIM.API.Extensions
{
    public static class UIDocumentExtensions
    {
        /// <summary>
        /// Obtenir les éléments sélectionnés d'une classe donnée dans l'UI
        /// </summary>
        /// <param name="uidoc">UI Document</param>
        /// <param name="elementClass">Classe des éléments voulus</param>
        /// <returns>Liste des éléments</returns>
        public static List<Element> GetSelectedElementsOfClass<T>(this UIDocument uidoc)
        {
            List<Element> selection = uidoc.Selection.GetElementIds()
                                                     .Select(id => uidoc.Document.GetElement(id))
                                                     .Where(el => el.GetType().Equals(typeof(T)))
                                                     .ToList();

            return selection;
        }

        /// <summary>
        /// Obtenir les éléments d'une catégorie donnée dans la sélection UI
        /// </summary>
        /// <param name="uidoc">UI Document</param>
        /// <param name="bic">BuiltInCategory des éléments voulus</param>
        /// <returns>Liste des éléments</returns>
        public static List<Element> GetSelectedElementsOfCategory(this UIDocument uidoc, BuiltInCategory bic)
        {
            List<Element> selection = uidoc.Selection.GetElementIds()
                                                     .Select(id => uidoc.Document.GetElement(id))
                                                     .Where(el => el.GetBuiltInCategory().Equals(bic))
                                                     .ToList();

            return selection;
        }

        /// <summary>
        /// Obtenir les éléments de catégories données dans la sélection UI
        /// </summary>
        /// <param name="uidoc">UI Document</param>
        /// <param name="bics">Liste des BuiltInCategory des éléments voulus</param>
        /// <returns>Liste des éléments</returns>
        public static List<Element> GetSelectedElementsOfCategories(this UIDocument uidoc, List<BuiltInCategory> bics)
        {
            List<Element> selection = uidoc.Selection.GetElementIds()
                                                     .Select(id => uidoc.Document.GetElement(id))
                                                     .Where(el => bics.Contains(el.GetBuiltInCategory()))
                                                     .ToList();

            return selection;
        }

        /// <summary>
        /// Obtenir la feuille de la vue active du document ou la feuille sélectionnée
        /// </summary>
        /// <param name="uidoc">UI Document</param>
        /// <returns>Feuille</returns>
        public static ViewSheet GetViewSheetSource(this UIDocument uidoc)
        {
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveGraphicalView;

            List<ElementId> uiSelection = uidoc.Selection.GetElementIds().ToList();
            if (activeView.GetType().Equals(typeof(ViewSheet)))
            {
                return activeView as ViewSheet;
            }
            else if (uiSelection.Count == 1 && doc.GetElement(uiSelection.First()).GetType().Equals(typeof(ViewSheet)))
            {
                return doc.GetElement(uiSelection.First()) as ViewSheet;
            }

            return null;
        }

        /// <summary>
        /// Obtenir la vue active du document ou la feuille sélectionnée
        /// <br>Uniquement pour les vues type Plan, Section ou 3D</br>
        /// </summary>
        /// <param name="uidoc">UI Document</param>
        /// <returns>Vue</returns>
        public static View GetViewSource(this UIDocument uidoc)
        {
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveGraphicalView;
            List<Type> allowedViewTypes = new List<Type>() 
            { 
                typeof(View3D), 
                typeof(ViewPlan), 
                typeof(ViewSection) 
            };

            List<ElementId> uiSelection = uidoc.Selection.GetElementIds().ToList();

            if (allowedViewTypes.Contains(activeView.GetType()))
            {
                return activeView;
            }
            else if (uiSelection.Count == 1 && allowedViewTypes.Contains(doc.GetElement(uiSelection.First()).GetType()))
            {
                return doc.GetElement(uiSelection.First()) as View;
            }

            return null;
        }

        /// <summary>
        /// Obtenir la vue active du document ou la feuille sélectionnée
        /// <br>Uniquement pour les vues type Plan, Section ou 3D</br>
        /// </summary>
        /// <param name="uidoc">UI Document</param>
        /// <returns>Vue</returns>
        public static T GetViewSource<T>(this UIDocument uidoc) where T : View
        {
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveGraphicalView;

            List<ElementId> uiSelection = uidoc.Selection.GetElementIds().ToList();
            if (activeView.GetType().Equals(typeof(T)))
            {
                return activeView as T;
            }
            else if (uiSelection.Count == 1 && doc.GetElement(uiSelection.First()).GetType().Equals(typeof(T)))
            {
                return doc.GetElement(uiSelection.First()) as T;
            }

            return null;
        }

/*        public static void SelectInPlaceFamilies(this UIDocument uidoc)
        {
            Document doc = uidoc.Document;
            List<ElementId> selection = new List<ElementId>();
            List<Family> families = doc.GetInPlaceFamilies();
            
            foreach (Family family in families)
            {
                var number = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance))
                                                              .WhereElementIsNotElementType()
                                                              .Cast<FamilyInstance>()
                                                              .Where(el => el.Symbol.Family.Id.Equals(family.Id))
                                                              .ToList();

                selection.AddRange(number.Select(el => el.Id));
            }

            uidoc.Selection.SetElementIds(selection);
        }*/
    }
}
