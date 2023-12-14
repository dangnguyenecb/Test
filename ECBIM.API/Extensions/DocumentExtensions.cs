using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class DocumentExtensions
    {
/*        public static List<Element> GetAllModelElements(this Document document)
        {
            Categories categories = document.Settings.Categories;
            List<Element> model_elements = new List<Element>();

            foreach (Category cat in categories)
            {
                if (cat.CategoryType == CategoryType.Model && (cat.SubCategories.Size > 0 || cat.CanAddSubcategory))
                {
                    FilteredElementCollector Cat_Elements_Collector = new FilteredElementCollector(document).OfCategoryId(cat.Id).WhereElementIsNotElementType();

                    foreach (Element el in Cat_Elements_Collector)
                    {
                        model_elements.Add(el);
                    }
                }
            }
            return model_elements;
        }*/

        public static List<Element> GetAllModelElements(this Document document, bool excludeNestedFamilies = true)
        {
            Categories categories = document.Settings.Categories;
            List<Element> modelElements = new List<Element>();

            foreach (Category cat in categories)
            {
                if (cat.CategoryType == CategoryType.Model && (cat.SubCategories.Size > 0 || cat.CanAddSubcategory))
                {
                    var catElements = new FilteredElementCollector(document).OfCategoryId(cat.Id).WhereElementIsNotElementType();

                    if (catElements.Count() > 0 && catElements.First() is FamilyInstance && excludeNestedFamilies)
                    {
                        var familyInstances = catElements.OfClass(typeof(FamilyInstance))
                                                         .Cast<FamilyInstance>()
                                                         .Where(el => el.SuperComponent == null);

                        foreach (var el in familyInstances)
                        {
                            modelElements.Add(el);
                        }
                    }
                    else
                    {
                        foreach (Element el in catElements)
                        {
                            modelElements.Add(el);
                        }
                    }
                }
            }
            return modelElements;
        }

        /// <summary>
        /// Obtenir la liste des familles in-situ d'un document
        /// </summary>
        /// <param name="doc">Document</param>
        /// <returns>Liste des familles in-situ</returns>
        public static List<InPlaceFamily> GetInPlaceFamilies(this Document doc)
        {
            return new FilteredElementCollector(doc).OfClass(typeof(Family))
                                                    .Cast<Family>()
                                                    .Where(f => f.IsInPlace)
                                                    .Select(f => new InPlaceFamily(f))
                                                    .ToList();
        }

        /// <summary>
        /// Obtenir tous les éléments d'une famille utilisateur présents dans un document
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="family">Famille</param>
        /// <returns>Liste des Id des éléments de la famille</returns>
        public static List<ElementId> GetFamilyElements(this Document doc, Family family)
        {

            List<ElementId> elementIds = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance))
                                                                          .Cast<FamilyInstance>()
                                                                          .Where(el => el.Symbol.Family.Id.Equals(family.Id))
                                                                          .Select(el => el.Id)
                                                                          .ToList();
            return elementIds;
        }

        /// <summary>
        /// Obtenir la liste des phases d'un document
        /// </summary>
        /// <param name="document">Document</param>
        /// <returns>Liste des phases</returns>
        public static List<Phase> GetPhases(this Document document)
        {
            List<Phase> phases = new List<Phase>();

            foreach (Phase phase in document.Phases)
            {
                phases.Add(phase);
            }

            return phases;
        }

        public static SharedParameterElement GetProjectParameter(this Document doc, string name)
        {
            SharedParameterElement p = new FilteredElementCollector(doc).OfClass(typeof(SharedParameterElement))
                                                                        .Cast<SharedParameterElement>()
                                                                        .Where(i => i.Name.Equals(name))
                                                                        .FirstOrDefault();

            return p;

        }

        public static List<BuiltInCategory> GetUsedBuiltInCategories(this Document doc)
        {
            return GetUsedCategories(doc).Select(i => i.GetBuiltInCategory()).ToList();
        }

        public static List<Category> GetUsedCategories(this Document doc)
        {
            Categories categories = doc.Settings.Categories;
            List<Category> usedCategories = new List<Category>();

            foreach (Category cat in categories)
            {
                if (cat.CategoryType == CategoryType.Model && (cat.SubCategories.Size > 0 || cat.CanAddSubcategory))
                {
                    var count = new FilteredElementCollector(doc).OfCategoryId(cat.Id)
                                                                             .WhereElementIsNotElementType()
                                                                             .Count();

                    if (count > 0)
                    {
                        usedCategories.Add(cat);
                    }
                }
            }

            return usedCategories;
        }

        /// <summary>
        /// Obtenir la liste des vues (Plan, Section et 3D) présentent dans le projet
        /// </summary>
        /// <param name="document"></param>
        /// <param name="Schedules">True pour inclure les nomenclatures. Par défaut : False</param>
        /// <returns>Liste des vues</returns>
        public static List<View> GetViews(this Document document, bool Schedules = false)
        {

            List<View> views = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Views)
                                                                             .WhereElementIsNotElementType()
                                                                             .Cast<View>()
                                                                             .Where(view => !view.IsTemplate)
                                                                             .ToList();

            if (Schedules)
            {

                List<View> schedules = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Schedules)
                                                                  .WhereElementIsNotElementType()
                                                                  .ToElements()
                                                                  .Cast<ViewSchedule>()
                                                                  .Where(view => !view.IsTemplate)
                                                                  .Where(view => !view.IsTitleblockRevisionSchedule)
                                                                  .Cast<View>()
                                                                  .ToList();
                views.AddRange(schedules);
            }

            return views;
        }

        /// <summary>
        /// Obtenir la liste des vues d'un projet du type donné
        /// </summary>
        /// <param name="document">Document</param>
        /// <param name="viewType">Type de vue</param>
        /// <returns>Liste des vues</returns>
        public static List<View> GetViews(this Document document, Type viewType)
        {
            List<View> viewSheets = new FilteredElementCollector(document).OfClass(viewType)
                                                                          .WhereElementIsNotElementType()
                                                                          .Cast<View>()
                                                                          .Where(view => !view.IsTemplate)
                                                                          .ToList();
            return viewSheets;
        }

        /// <summary>
        /// Obtenir la liste des vues d'un projet du type donné
        /// </summary>
        /// <typeparam name="T">Type de vue</typeparam>
        /// <param name="document">Document</param>
        /// <returns>Liste des vues</returns>
        public static List<T> GetViews<T>(this Document document) where T : View
        {
            List<T> viewSheets = new FilteredElementCollector(document).OfClass(typeof(T))
                                                                       .WhereElementIsNotElementType()
                                                                       .Cast<T>()
                                                                       .Where(view => !view.IsTemplate)
                                                                       .ToList();
            return viewSheets;
        }

        /// <summary>
        /// Obtenir un type à partir du nom de sa famille et de son nom
        /// </summary>
        /// <param name="document"></param>
        /// <param name="familyName"></param>
        /// <param name="symbolName"></param>
        /// <param name="familySymbol"></param>
        /// <returns>True si un element est trouvé</returns>
        public static bool TryGetFamilySymbol(this Document document, string familyName, string symbolName, out FamilySymbol familySymbol)
        {
            familySymbol = null;

            try
            {
                familySymbol = new FilteredElementCollector(document).OfClass(typeof(FamilySymbol))
                                                                      .Cast<FamilySymbol>()
                                                                      .Where(f => f.FamilyName.Equals(familyName))
                                                                      .First(f => f.Name.Equals(symbolName));
                return true;
            }
            catch { }

            return false;
        }
    }
}
