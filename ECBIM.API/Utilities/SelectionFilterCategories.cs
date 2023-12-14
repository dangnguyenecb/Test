using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using ECBIM.API.Extensions;

namespace ECBIM.API.Utilities
{
    public class SelectionFilterCategories : ISelectionFilter
    {
        private readonly List<BuiltInCategory> cats;
        public SelectionFilterCategories(List<BuiltInCategory> categories)
        {
            cats = categories;
        }
        public bool AllowElement(Element elem)
        {
            if (cats.Contains(elem.GetBuiltInCategory()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
