using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Mockup.Utility
{
    class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool val = (bool)value;
            if (val)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Visibility val = (Visibility)value;
            if (val == Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Inverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public static class Ext
    {
        public static Color ToColor(this string value)
        {
            Color c;
            var match = Regex.Match(value, "#([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})");
            int a = Convert.ToInt32(match.Groups[1].Value, 16);
            int r = Convert.ToInt32(match.Groups[2].Value, 16);
            int g = Convert.ToInt32(match.Groups[3].Value, 16);
            int b = Convert.ToInt32(match.Groups[4].Value, 16);
            c = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
            return c;
        }

        public static DoubleCollection Clone(this DoubleCollection item)
        {
            DoubleCollection col = new DoubleCollection();
            foreach (var dou in item)
            {
                col.Add(dou);
            }
            return col;
        }
    }

    class PickerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
