using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using ECBIM.API.Extensions;

namespace ECBIM.API.Utilities
{
    public class SelectionFilterModel : ISelectionFilter
    {
        private readonly bool get_lines;

        public SelectionFilterModel(bool lines)
        {
            get_lines = lines;
        }

        public bool AllowElement(Element elem)
        {
            if (get_lines && elem.Category.CategoryType == CategoryType.Model)
            {
                return true;
            }
            else if (elem.Category.CategoryType == CategoryType.Model && elem.GetBuiltInCategory() != BuiltInCategory.OST_Lines)
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
