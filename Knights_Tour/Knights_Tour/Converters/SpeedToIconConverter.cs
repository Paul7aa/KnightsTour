using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Knights_Tour.Converters
{
    public class SpeedToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int speed = System.Convert.ToInt16(value);

            switch (speed)
            {
                case 1:
                    return MaterialDesignThemes.Wpf.PackIconKind.Speedometer;
                case 500:
                    return MaterialDesignThemes.Wpf.PackIconKind.SpeedometerMedium;
                case 1000:
                    return MaterialDesignThemes.Wpf.PackIconKind.SpeedometerSlow;
                default:
                    return DependencyProperty.UnsetValue;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
