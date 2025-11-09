using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace project_7.ValidationRules
{
    public class PhoneNumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Номер телефона обязателен для заполнения.");

            var simvls = Regex.Replace(input, @"[^\d]", "");

            if (!Regex.IsMatch(simvls, @"^\d{10,11}$"))
                return new ValidationResult(false, "Введите корректный номер телефона (10–11 цифр).");

            return ValidationResult.ValidResult;
        }
    }
}
