using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class ViewExtensions
    {
        /// <summary>
        /// Modifier la visibilité d'une catégorie dans une vue
        /// </summary>
        /// <param name="view">Vue dans laquelle modifier la visibilité</param>
        /// <param name="bic">BuiltInCategory à modifier</param>
        /// <param name="hide">True : Masquer, False : Afficher</param>
        /// <returns></returns>
        public static bool ChangeCategoryVisibility(this View view, BuiltInCategory bic, bool hide)
        {
            ElementId catId = new ElementId(bic);

            if (view.CanCategoryBeHidden(catId))
            {
                view.SetCategoryHidden(catId, hide);
                return true;
            }

            return false;
        }

    }
}
