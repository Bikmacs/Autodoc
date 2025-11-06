using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace project_7.ValidationRules
{
    class RequiredRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? " ").ToString().Trim();
            if(input == string.Empty)
            {
                return new ValidationResult(false, "Поле не может быть пустым");
            }

            if (!int.TryParse(input, out int result))
            {
                return new ValidationResult(false, "поле быть целым числом.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
