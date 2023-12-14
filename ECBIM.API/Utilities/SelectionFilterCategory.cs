using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using ECBIM.API.Extensions;

namespace ECBIM.API.Utilities
{
    public class SelectionFilterCategory : ISelectionFilter
    {
        private readonly BuiltInCategory cat;

        public SelectionFilterCategory(BuiltInCategory bic)
        {
            cat = bic;
        }

        public bool AllowElement(Element elem)
        {
            if (elem.GetBuiltInCategory() == cat)
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
