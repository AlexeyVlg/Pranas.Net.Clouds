using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Pranas.Client.GoogleDrive.Demo.Converters
{
    /// <summary>
    /// Represents converter a boolen value into visibility value.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            var ep = parameter as string;

            if (string.IsNullOrEmpty(ep))
            {
                result = Visibility.Hidden;
            }
            else
            {
                var eparams = ep.Split('|');

                if (eparams.Length != 2)
                {
                    result = Visibility.Hidden;
                }
                else
                {
                    var cnd = (bool)value;

                    if (!Enum.TryParse(eparams[cnd ? 0 : 1], true, out result))
                    {
                        result = Visibility.Hidden;
                    }
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
