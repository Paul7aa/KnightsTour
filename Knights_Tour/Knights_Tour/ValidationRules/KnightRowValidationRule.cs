using Knights_Tour.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Knights_Tour.ValidationRules
{
    public class KnightRowValidationRule : ValidationRule
    {
        public bool IsRow { get; set; }
        public int MinSize { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var dataitem = (MainWindowViewModel)((BindingExpression)value).DataItem;
            string size = (IsRow)? dataitem.KnightX.ToString() : dataitem.KnightY.ToString();
            int intSize = 0;
            char charSize = 'A';

            int max = (IsRow) ? Wrapper.MaxSize : Wrapper.MaxSize + 64;

            bool parseSuccessful = (IsRow) ? Int32.TryParse(size, out intSize) : Char.TryParse(size?.ToUpper(), out charSize);

            if (!parseSuccessful || ((IsRow) ? intSize : charSize) > max || ((IsRow)?intSize : charSize) < MinSize)
                return new ValidationResult(false, ((IsRow) ? "Must be a number in [1," : "Must be a letter in [A,") +
                    ((IsRow) ? this.Wrapper.MaxSize : Char.ToString((char)(max))) + "]");
            else
                return new ValidationResult(true, null);
        }

        public Wrapper Wrapper 
        {
            get; set; 
        }
    }

    public class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty MaxSizeProperty =
             DependencyProperty.Register("MaxSize", typeof(int),
             typeof(Wrapper), new PropertyMetadata(0));

        public int MaxSize
        {
            get { return (int)GetValue(MaxSizeProperty); }
            set { SetValue(MaxSizeProperty, value); }
        }
    }
}
