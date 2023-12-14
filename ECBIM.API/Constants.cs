using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace ECBIM.API
{
    public static class Constants
    {
        /// <summary>
        /// Liste des caractères interdits dans les nommages Revit
        /// </summary>
        public static readonly List<char> ForbiddenChars = new List<char>()
        { '\\', ':', '{', '}', '[', ']', '|', ';', '<', '>', '?', '`', '~' };

        /// <summary>
        /// Liste des types de vues autorisés pour la duplication de feuilles
        /// </summary>
        public static readonly List<Type> DuplicationSheetViewTypes = new List<Type>()
        { typeof(View3D), typeof(ViewPlan), typeof(ViewSection), typeof(ViewSchedule)};
    }
}
