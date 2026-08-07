using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Editors
{
    public class FormatStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is ComboBoxItem)
            {
                ComboBoxItem item = value as ComboBoxItem;
                return item.Tag.ToString();
            }
            return "N";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is ComboBoxItem)
            {
                ComboBoxItem item = value as ComboBoxItem;
                return item.Tag.ToString();
            }
            return "N";
        }
    }
}
