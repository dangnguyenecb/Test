using System.Globalization;
using System.Windows.Controls;
using ECBIM.API.Utilities;

namespace ECBIM.APIUI
{
    public class PositiveOrNullIntegerValidation : ValidationRule
    {
        public PositiveOrNullIntegerValidation()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;

            return EiffageUtilities.TryParsePositiveInt(str, true, out _)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Saisir un entier supérieur ou égal à 0");
        }
    }
}
