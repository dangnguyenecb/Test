using System.Globalization;
using System.Windows.Controls;
using ECBIM.API.Extensions;

namespace ECBIM.APIUI
{
    public class RevitStringValidation : ValidationRule
    {
        public RevitStringValidation()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = value as string;

            return str.IsValidForRevit()
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Le texte ne peut pas contenir un des caractères suivants : \\ : { } [ ] | ; < > ? ' ~");
        }
    }
}
