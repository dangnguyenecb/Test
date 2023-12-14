using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using ECBIM.API.Extensions;
using System.Text;
using ECBIM.API;
using System.Reflection;
using System;
using System.IO;

namespace ECBIM.API.Utilities
{
    public static class EiffageUtilities
    {
        /// <summary>
        /// Obtenir la configuration d'un gabarit de vue à partir de son nom, avec :
        ///    - Préfixe
        ///    - Numéro
        ///    - Suffixe
        /// </summary>
        /// <param name="str">Nom du gabarit</param>
        /// <returns>ViewTemplateConfiguration</returns>
/*        public static TextWithNumberConfiguration GetTextWithNumberConfiguration(string str)
        {
            string prefix = string.Empty;
            StringBuilder sb_number = new StringBuilder();
            StringBuilder sb_suffix = new StringBuilder();
            bool numberFound = false;

            if (str.ContainsNumber())
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    char c = str[i];

                    if (char.IsNumber(c))
                    {
                        sb_number.Insert(0, c);
                        numberFound = true;
                    }
                    else if (!char.IsNumber(c) && !numberFound)
                    {
                        sb_suffix.Insert(0, c);
                    }
                    else
                    {
                        prefix = str.Substring(0, i + 1);
                        break;
                    }
                }

                var result = new TextWithNumberConfiguration(str)
                {
                    Prefix = prefix,
                    Suffix = sb_suffix.ToString()
                };

                if (int.TryParse(sb_number.ToString(), out int number ))
                {
                    result.Number = number;
                }

                return result;
            }

            return new TextWithNumberConfiguration(str)
            {
                Prefix = str
            };
        }*/

        /// <summary>
        /// Convertit une chaîne de caractère en entier positif ou nul. Si la chaîne n'est pas convertible, renvoie 0.
        /// </summary>
        /// <param name="str">Chaîne à convertir en entier.</param>
        /// <param name="positiveOrNull">True pour positif ou nul. False pour strictement positif.</param>
        /// <returns>Entier correspondant à la chaîne ou 0 si la chaîne est vide.</returns>
        public static int ParsePositiveInt(string str, bool positiveOrNull)
        {
            if (TryParsePositiveInt(str, positiveOrNull, out int integer))
            {
                return integer;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convertit une chaîne de caractère en entier positif ou nul. Si la chaîne n'est pas convertible, renvoie n.
        /// </summary>
        /// <param name="str">Chaîne à convertir en entier.</param>
        /// <param name="positiveOrNull">True pour positif ou nul. False pour strictement positif.</param>
        /// <param name="n">Valeur par défaut</param>
        /// <returns>Entier correspondant à la chaîne ou n si la chaîne est vide.</returns>
        public static int ParsePositiveInt(string str, bool positiveOrNull, int n)
        {
            if (TryParsePositiveInt(str, positiveOrNull, out int integer))
            {
                return integer;
            }
            else
            {
                return n;
            }
        }

        /// <summary>
        /// Convertit une chaîne de caractère en entier positif ou nul. Une valeur de retour indique si la conversion a réussi.
        /// </summary>
        /// <param name="str">Chaîne à convertir en entier.</param>
        /// <param name="positiveOrNull">True pour positif ou nul. False pour strictement positif.</param>
        /// <param name="integer">Entier de retour si la conversion a réussi.</param>
        /// <returns>True si la conversion a réussi. Sinon False.</returns>
        public static bool TryParsePositiveInt(string str, bool positiveOrNull, out int integer)
        {
            if (int.TryParse(str, out integer))
            {
                if (positiveOrNull == true && integer >= 0)
                {
                    return true;
                }

                if (positiveOrNull == false && integer > 0)
                {
                    return true;
                }
            }
            else if (str.Equals(string.Empty))
            {
                integer = 0;
                return true;
            }

            return false;
        }

        public static string GetAssemblyPathByCodeBase()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
        }
    }
}

