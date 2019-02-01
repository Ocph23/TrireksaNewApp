using ModelsShared.Models;
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


    public class VisibilityBoleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            bool isTrue = (bool)value;
            if (isTrue)
                return Visibility.Visible;
            else
            {
                int paramValue = 0;
                var param = Int32.TryParse(parameter.ToString(), out paramValue);
                if (paramValue > 0)
                    return Visibility.Collapsed;
                return Visibility.Hidden;
            }
                

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }


    public class CustomerAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            customer data = (customer)value;
            if(data!=null)
            return string.Format("{0}\r{1}\rTlp:{2}/{3}/{4}", data.Address, data.City.CityName,data.Phone1,data.Phone2,data.Handphone);
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }



}
