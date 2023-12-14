using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Convertir un nombre en degrés décimaux depuis les unités internes
        /// </summary>
        /// <param name="angle">Angle en unités internes</param>
        /// <returns></returns>
        public static double ConvertToDegrees(this double angle)
        {
#if REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020
            return UnitUtils.ConvertFromInternalUnits(angle, DisplayUnitType.DUT_DECIMAL_DEGREES);

#elif REVIT2021 || REVIT2022 || REVIT2023
            return UnitUtils.ConvertFromInternalUnits(angle, UnitTypeId.Degrees);
#endif
        }

        /// <summary>
        /// Convertir un nombre en mètres depuis les unités internes
        /// </summary>
        /// <param name="number">Longueur en unités interne</param>
        /// <returns></returns>
        public static double ConvertToMeters(this double number)
        {
#if REVIT2019 || REVIT2020
            return UnitUtils.ConvertFromInternalUnits(number, DisplayUnitType.DUT_METERS);
#elif REVIT2021 || REVIT2022 || REVIT2023
            return UnitUtils.ConvertFromInternalUnits(number, UnitTypeId.Meters);
#endif
        }

        /// <summary>
        /// Convertir un nombre en unités internes depuis des mètres
        /// </summary>
        /// <param name="number">Longueur en mètres</param>
        /// <returns></returns>
        public static double ConvertFromMeters(this double number)
        {
#if REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020
            return UnitUtils.ConvertToInternalUnits(number, DisplayUnitType.DUT_METERS);
#elif REVIT2021 || REVIT2022 || REVIT2023
            return UnitUtils.ConvertToInternalUnits(number, UnitTypeId.Meters);
#endif
        }

        public static bool IsAlmostEquals(this double a, double b)
        {
            return Math.Abs(a - b) < 1e-9;
        }
    }
}
