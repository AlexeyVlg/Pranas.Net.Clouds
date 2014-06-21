using System;
using System.Globalization;
using System.Windows.Data;

namespace Pranas.Client.GoogleDrive.Demo.Converters
{
    public class InstanceToIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
