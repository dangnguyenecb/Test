using System.Globalization;
using System.Windows.Controls;
using ECBIM.API.Utilities;

namespace ECBIM.APIUI
{
    public class PositiveIntegerValidation : ValidationRule
    {
        public bool PositiveOrNull { get; set; }

        public PositiveIntegerValidation()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;

            return EiffageUtilities.TryParsePositiveInt(str, PositiveOrNull, out _)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Saisir un entier positif");
        }
    }
}
