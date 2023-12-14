using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class BuiltInCategoryExtensions
    {
        public static Category GetCategory(this BuiltInCategory bic, Document doc)
        {
            foreach (Category cat in doc.Settings.Categories)
            {
                if (cat.Id == new ElementId(bic))
                {
                    return cat;
                }
            }

            return null;
        }

        public static string GetName(this BuiltInCategory bic, Document doc)
        {
            Category cat = GetCategory(bic, doc);
            return cat.Name;
        }
    }
}
