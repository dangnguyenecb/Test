using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class XYZExtensions
    {
        public static XYZ Flat(this XYZ point)
        {
            return new XYZ(point.X, point.Y, 0);
        }

        public static XYZ ToMetric(this XYZ point)
        {
#if REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020
            double X = UnitUtils.ConvertFromInternalUnits(point.X, DisplayUnitType.DUT_METERS);
            double Y = UnitUtils.ConvertFromInternalUnits(point.Y, DisplayUnitType.DUT_METERS);
            double Z = UnitUtils.ConvertFromInternalUnits(point.Z, DisplayUnitType.DUT_METERS);
#elif REVIT2021 || REVIT2022 || REVIT2023
            double X = UnitUtils.ConvertFromInternalUnits(point.X, UnitTypeId.Meters);
            double Y = UnitUtils.ConvertFromInternalUnits(point.Y, UnitTypeId.Meters);
            double Z = UnitUtils.ConvertFromInternalUnits(point.Z, UnitTypeId.Meters);
#endif
            return new XYZ(X, Y, Z);
        }
    }
}
