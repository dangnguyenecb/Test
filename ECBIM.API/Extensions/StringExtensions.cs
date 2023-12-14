using System.Linq;
using System.Text;

namespace ECBIM.API.Extensions
{
    public static class StringExtensions
    {

        public static bool ContainsInsensitiveCase(this string source, string str)
        {
            return source.ToLower().Contains(str.ToLower());
        }

        /// <summary>
        /// Vérifier si une chaîne de caractère contient un nombre
        /// </summary>
        /// <param name="str">Chaîne de caractère à vérifier</param>
        /// <returns>True si la chaîne contient un nombre</returns>
        public static bool ContainsNumber(this string str)
        {
            foreach (char c in str)
            {
                if (char.IsNumber(c))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Formater une chaîne de caractère en supprimant les caractères non pris en charge par Revit
        /// </summary>
        /// <param name="str">Chaîne à formater</param>
        /// <returns>Chaîne formatée</returns>
        public static string FormatForRevit(this string str)
        {
            foreach (char c in Constants.ForbiddenChars)
            {
                str = str.Replace(c.ToString(), string.Empty);
            }

            return str;
        }

        /// <summary>
        /// Obtenir le dernier chiffre d'une chaîne de caractère
        /// </summary>
        /// <param name="str">Chaîne de caractère à vérifier</param>
        /// <returns>Chiffre</returns>
        public static int GetLastNumber(this string str)
        {
            if (str.ContainsNumber())
            {
                StringBuilder sb_number = new StringBuilder();
                bool numberFound = false;

                for (int i = str.Length - 1; i >= 0; i--)
                {
                    char c = str[i];

                    if (char.IsNumber(c))
                    {
                        sb_number.Insert(0, c);
                        numberFound = true;
                    }
                    else if (numberFound)
                    {
                        break;
                    }
                }

                return int.Parse(sb_number.ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string GetSingular(this string str)
        {
            return str.Last().Equals('s')? str.Remove(str.Length - 1) : str;
        }

        /// <summary>
        /// Vérifier si une chaîne de caractère contient un caractère interdit
        /// </summary>
        /// <param name="name">Chaîne de caractère à vérifier</param>
        /// <returns></returns>
        public static bool IsValidForRevit(this string str)
        {
            foreach (char c in Constants.ForbiddenChars)
            {
                if (str.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }


        public static string Center(this string str, int length, char c)
        {
            int count = str.Length+2;
            int n = (length - str.Length - 2) / 2;
            string symbol = new string(c, n);

            return $"{symbol} {str} {symbol}";
        }
    }
}
