using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
namespace ECBIM.API.Extensions
{
    public static class CategoryExtensions
    {
        public static BuiltInCategory GetBuiltInCategory(this Category cat)
        {
            foreach (BuiltInCategory bic in System.Enum.GetValues(typeof(BuiltInCategory)))
            {
                if (new ElementId(bic) == cat.Id)
                {
                    return bic;
                }
            }

            return new BuiltInCategory();
        }
    }
}
