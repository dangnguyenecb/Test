using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class WallExtensions
    {
        public static XYZ GetWallDirection(this Wall wall)
        {
            LocationCurve wallLocationCurve = wall.Location as LocationCurve;
            Line wallLine = wallLocationCurve.Curve as Line;
            return wallLine.GetEndPoint(1) - wallLine.GetEndPoint(0);
        }

/*        public static double GetHeight(this Wall wall)
        {
            BoundingBoxXYZ boundingBox = wall.get_BoundingBox(null);
            return boundingBox.Max.Z - boundingBox.Min.Z;
        }*/
    }
}
