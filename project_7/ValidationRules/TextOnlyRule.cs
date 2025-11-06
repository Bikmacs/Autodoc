using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace project_7.ValidationRules
{
    public class TextOnlyRule : ValidationRule
    {
        private static readonly Regex TextRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ\s-]+$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Поле должно быть заполнено.");
            }

            if (!TextRegex.IsMatch(input))
            {
                return new ValidationResult(false, "Поле должно содержать только буквы");
            }

            return ValidationResult.ValidResult;
        }
    }
}