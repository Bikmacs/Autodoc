using System.Globalization;
using System.Windows.Controls;

namespace project_7.ValidationRules
{
    public class IdRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, "Идентификатор обязателен для заполнения.");

            if (!int.TryParse(input, out int id))
                return new ValidationResult(false, "Идентификатор должен содержать только цифры.");

            if (id <= 0)
                return new ValidationResult(false, "Идентификатор должен быть больше нуля.");

            return ValidationResult.ValidResult;
        }
    }
}
