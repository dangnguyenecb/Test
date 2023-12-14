using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace ECBIM.API.Extensions
{
    public static class ElementExtensions
    {
        /// <summary>
        /// Récupérer la BuiltInCategory d'un élément
        /// </summary>
        /// <param name="element">Elément</param>
        /// <returns>BuiltInCategory</returns>
        static public BuiltInCategory GetBuiltInCategory(this Element element)
        {
            BuiltInCategory ost = BuiltInCategory.INVALID;
            foreach (BuiltInCategory bic in System.Enum.GetValues(typeof(BuiltInCategory)))
            {
                if (new ElementId(bic) == element.Category.Id)
                {
                    ost = bic;
                }
            }
            return ost;
        }

        /// <summary>
        /// Obtenir le type (FamilySymbol) d'un élément
        /// </summary>
        /// <param name="element">Element</param>
        /// <returns>Famille</returns>
        static public FamilySymbol GetFamilySymbol(this Element element)
        {
            try
            {
                FamilyInstance familyInstance = element as FamilyInstance;
                return familyInstance.Symbol;
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Obtenir un dictionnaire des paramètres d'occurence d'un élement
        /// Clef : Définition du paramètre
        /// Valeur : Valeur du paramètre
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        static public Dictionary<Definition, object> GetOrderedParametersValues(this Element element)
        {
            Dictionary<Definition, object> result = new Dictionary<Definition, object>();
            IList<Parameter> parameters = element.GetOrderedParameters();

            foreach (Parameter param in parameters)
            {
                try
                {
                    switch (param.StorageType)
                    {
                        case StorageType.ElementId:
                            result[param.Definition] = param.AsElementId();
                            break;
                        case StorageType.Integer:
                            result[param.Definition] = param.AsInteger();
                            break;
                        case StorageType.Double:
                            result[param.Definition] = param.AsDouble();
                            break;
                        case StorageType.String:
                            result[param.Definition] = param.AsString();
                            break;
                    }
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// Obtenir le paramère d'un élément à partir d'un nom de paramètre
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="parameterName">Nom du paramètre</param>
        /// <param name="parameter">Paramètre obtenu</param>
        /// <returns></returns>
        public static bool TryGetParameter(this Element element, string parameterName, out Parameter parameter)
        {
            parameter = element.LookupParameter(parameterName);

            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Obtenir la liste des noms de paramètres communs à plusieurs éléments
        /// </summary>
        /// <param name="elements">Liste des éléments</param>
        /// <returns>Liste des noms de paramètres communs</returns>
        public static List<string> GetCommonParameters(this IEnumerable<Element> elements)
        {
            List<string> commonParameters = new List<string>();

            commonParameters.AddRange(elements.First().GetOrderedParameters().Select(p => p.Definition.Name));

            foreach (Element el in elements)
            {
                var elementParameters = el.GetOrderedParameters().Select(p => p.Definition.Name);
                commonParameters = commonParameters.Intersect(elementParameters).ToList();
            }

            return commonParameters;
        }
    }
}
