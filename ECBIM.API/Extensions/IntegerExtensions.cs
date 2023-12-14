namespace ECBIM.API.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Formater un entier en chaîne de caractère avec un certains nombre de numéros (ex : 002)
        /// </summary>
        /// <param name="n">Entier à formater</param>
        /// <param name="digitNumber">Nombre de numéros à afficher</param>
        /// <returns>Chaîne de caractère du nombre formaté</returns>
        public static string FormatStringDigit(this int n, int digitNumber)
        {
            string specifier = "D" + digitNumber.ToString();
            string stringDigit = n.ToString(specifier);

            return stringDigit;
        }
    }
}
