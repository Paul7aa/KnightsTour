using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Knights_Tour.ValidationRules
{
    public class RowValidationRule : ValidationRule
    {
        public int MaxSize { get; set; }
        public int MinSize { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string size = value as string;
            int intSize;
            bool parseSuccessful = Int32.TryParse(size, out intSize);

            if (!parseSuccessful || intSize > MaxSize || intSize < MinSize)
                return new ValidationResult(false, "Must be a number between in [3,16]");
            else
                return new ValidationResult(true, null);
        }
    }
}
