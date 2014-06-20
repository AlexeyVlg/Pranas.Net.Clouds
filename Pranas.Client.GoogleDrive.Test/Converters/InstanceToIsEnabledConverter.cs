using System;
using System.Globalization;
using System.Windows.Data;

namespace Pranas.Client.GoogleDrive.Test.Converters
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
