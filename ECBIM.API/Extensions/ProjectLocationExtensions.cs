using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using ECBIM.API;

namespace ECBIM.API.Extensions
{
    public static class ProjectLocationExtensions
    {
        /// <summary>
        /// Obtenir les coordonnées de l'emplacement partagé dans les coordonnées locales
        /// </summary>
        /// <param name="projectLocation"></param>
        /// <returns></returns>
        public static XYZ GetLocalPosition(this ProjectLocation projectLocation)
        {
            return projectLocation.GetLocalPosition(new XYZ());
        }

        /// <summary>
        /// Obtenir les coordonnées d'un point partagé dans les coordonnées locales
        /// </summary>
        /// <param name="projectLocation"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static XYZ GetLocalPosition(this ProjectLocation projectLocation, XYZ point)
        {
            Transform transform = projectLocation.GetTotalTransform();
            return transform.OfPoint(point);
        }

        /// <summary>
        /// Obtenir les coordonnées de l'origine interne dans les coordonnées partagées
        /// </summary>
        /// <param name="projectLocation"></param>
        /// <returns></returns>
        public static XYZ GetSharedPosition(this ProjectLocation projectLocation)
        {
            return projectLocation.GetSharedPosition(new XYZ());
        }

        /// <summary>
        /// Obtenir les coordonnées d'un point projet dans les coordonnées partagées
        /// </summary>
        /// <param name="projectLocation"></param>
        /// <returns></returns>
        public static XYZ GetSharedPosition(this ProjectLocation projectLocation, XYZ point)
        {
            ProjectPosition position = projectLocation.GetProjectPosition(point);
            return new XYZ(position.EastWest, position.NorthSouth, position.Elevation);
        }

        public static XYZ GetVectorInSharedPosition(this ProjectLocation projectLocation, XYZ vector)
        {
            XYZ origin = new XYZ();
            XYZ end = origin + vector;
            ProjectPosition p0 = projectLocation.GetProjectPosition(origin);
            XYZ newOrigin = new XYZ(p0.EastWest, p0.NorthSouth, p0.Elevation);  // A voir si on garde l'élévation
            ProjectPosition p1 = projectLocation.GetProjectPosition(end);
            XYZ newEnd = new XYZ(p1.EastWest, p1.NorthSouth, p1.Elevation);     // A voir si on garde l'élévation

            return newEnd - newOrigin;

        }
        /// <summary>
        /// Afficher les coordonnées des origines locales et partagé d'un emplacement.
        /// </summary>
        /// <param name="location"></param>
        public static void PrintOrigins(this ProjectLocation location)
        {
            XYZ localOrigin = location.GetSharedPosition();
            XYZ sharedOrigin = location.GetSharedPosition();

            //ConsoleAPI.WriteLine("Emplacement partagé : " + location.Name);
            //ConsoleAPI.WriteEmptyLine();
            //ConsoleAPI.WriteLine("Coordonnées de l'origine interne dans l'emplacement partagé :");
            /*            ConsoleAPI.WriteLine("Est : " + UnitUtils.ConvertFromInternalUnits(localOrigin.X, UnitTypeId.Meters) + "m");            
                        ConsoleAPI.WriteLine("Nord : " + UnitUtils.ConvertFromInternalUnits(localOrigin.Y, UnitTypeId.Meters) + "m");
                        ConsoleAPI.WriteLine("Elévation : " + UnitUtils.ConvertFromInternalUnits(localOrigin.Z, UnitTypeId.Meters) + "m");*/
            //ConsoleAPI.WriteEmptyLine();
            //ConsoleAPI.WriteLine("Coordonnées de l'emplacement partagé dans le projet :");
            /*            ConsoleAPI.WriteLine("X : " + UnitUtils.ConvertFromInternalUnits(sharedOrigin.X, UnitTypeId.Meters) + "m");
                        ConsoleAPI.WriteLine("Y : " + UnitUtils.ConvertFromInternalUnits(sharedOrigin.Y, UnitTypeId.Meters) + "m");
                        ConsoleAPI.WriteLine("Z : " + UnitUtils.ConvertFromInternalUnits(sharedOrigin.Z, UnitTypeId.Meters) + "m");*/
            //ConsoleAPI.WriteEmptyLine();
            //ConsoleAPI.Dispose();
        }
    }
}
