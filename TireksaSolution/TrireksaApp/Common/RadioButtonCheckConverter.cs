using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TrireksaApp.Common
{
    public class RadioButtonCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true)?parameter : Binding.DoNothing;
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = Int32.Parse(parameter.ToString());
            bool isTrue = (bool)value;
            if ((param == 1 && !isTrue) || (param == 0 && isTrue))
                return Visibility.Visible;
            else
                return Visibility.Hidden;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }


}
