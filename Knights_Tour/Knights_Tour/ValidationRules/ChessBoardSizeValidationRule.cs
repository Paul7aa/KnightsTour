using System;
using System.Globalization;
using System.Windows.Controls;

namespace Knights_Tour.ValidationRules
{
    public class ChessBoardSizeValidationRule : ValidationRule
    {
        public int MaxSize { get; set; }
        public int MinSize { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string size = value as string;
            int intSize;
            bool parseSuccessful = Int32.TryParse(size, out intSize);

            if (!parseSuccessful || intSize > MaxSize || intSize < MinSize || intSize % 2 != 0)
                return new ValidationResult(false, "Must be an even number between in [6,16]");
            else
                return new ValidationResult(true, null);
        }
    }
}
