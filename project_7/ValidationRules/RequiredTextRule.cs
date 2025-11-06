using System.Globalization;
using System.Windows.Controls;

namespace project_7.ValidationRules
{
    public class RequiredTextRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Поле должно быть заполнено.");
            }

            return ValidationResult.ValidResult;
        }
    }
}