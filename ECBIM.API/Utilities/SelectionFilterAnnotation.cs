using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace ECBIM.API.Utilities
{
    public class SelectionFilterAnnotation : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem.Category.CategoryType == CategoryType.Annotation)
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
