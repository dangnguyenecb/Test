using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    /// Classe regroupant toutes les méthodes personnalisées
    /// </summary>
    public static class FamilyExtensions
    {
        /// <summary>
        /// Obtenir les paramètres partagés d'une famille (FamilyParameter)
        /// </summary>
        /// <param name="family">Famille</param>
        /// <returns>Liste des FamilyParameter</returns>
        public static List<FamilyParameter> GetFamilyParameters(this Family family)
        {
            Document familyDocument = family.Document.EditFamily(family);

            FamilyManager familyManager = familyDocument.FamilyManager;

            List<FamilyParameter> familyParameters = familyManager.Parameters.Cast<FamilyParameter>().ToList();

            return familyParameters;
        }
    }
}

