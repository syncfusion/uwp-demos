using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Syncfusion.UI.Xaml.Grid;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using System.Globalization;
using Syncfusion.SampleBrowser.UWP.SfDataGrid;
using Windows.UI.Xaml.Markup;

namespace DataGrid
{

#if !SFDATAGRID_STORE
    /// <summary>
    /// This class represents the color based on the range
    /// </summary>
    class RangeConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to SolidColorBrush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var range = value as int?;
            if (range > 35)
                return new SolidColorBrush(Colors.Orange);
            else
                return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }

    /// <summary>
    /// This class represents the SelectionForegroundConverter
    /// </summary>
    class SelectionForegroundConverter : IValueConverter
    {
        /// <summary>
        /// Convert visibility to Foreground color
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && (Visibility)value == Visibility.Visible)
                return 1;
            return 0.68;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
#endif

    /// <summary>
    /// This class represents the ChangeForegroundConverter
    /// </summary>
    class ChangeForegroundConverter : IValueConverter
    {
        /// <summary>
        ///  Convert double to SolidColorBrush
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            var data = value as double?;
            if (data != null && data > 0)
                return new SolidColorBrush(Colors.Green);
            return new SolidColorBrush(Colors.Red);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// This class represents the StyleConverterforQS2
    /// </summary>
    internal class StyleConverterforQS2 : IValueConverter
    {

        /// <summary>
        /// Convert value to brush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double _value;
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                _value = double.Parse(value.ToString(), System.Globalization.NumberStyles.Currency);
                if (_value < 4000.00 && _value > 1000.00)
                {
                    LinearGradientBrush Brush1 = new LinearGradientBrush();

                    GradientStop gradientStop1 = new GradientStop();
                    gradientStop1.Color = Colors.White;
                    gradientStop1.Offset = 0;
                    Brush1.GradientStops.Add(gradientStop1);

                    GradientStop gradientStop2 = new GradientStop();
                    gradientStop2.Color = (Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), "#FED06A");
                    gradientStop2.Offset = 1;
                    Brush1.GradientStops.Add(gradientStop2);
                    return Brush1;
                }
            }
            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the StyleConverterforQS3
    /// </summary>
    internal class StyleConverterforQS3 : IValueConverter
    {
        /// <summary>
        /// Convert value to brush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double _value;
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                _value = double.Parse(value.ToString(), NumberStyles.Currency);
                if (_value < 6600.00 && _value > 1000.00)
                {
                    LinearGradientBrush Brush2 = new LinearGradientBrush();

                    GradientStop gradientStop1 = new GradientStop();
                    gradientStop1.Color = Colors.White;
                    gradientStop1.Offset = 0;
                    Brush2.GradientStops.Add(gradientStop1);

                    GradientStop gradientStop2 = new GradientStop();
                    gradientStop2.Color = (Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), "#94C3FF");
                    gradientStop2.Offset = 1;
                    Brush2.GradientStops.Add(gradientStop2);
                    return Brush2;
                }
            }
            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the StyleConverterforQS4
    /// </summary>
    internal class StyleConverterforQS4 : IValueConverter
    {
        /// <summary>
        /// Convert value to brush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double _value;
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                _value = double.Parse(value.ToString(), NumberStyles.Currency);
                if (_value < 6600 && _value > 1000)
                {
                    LinearGradientBrush Brush3 = new LinearGradientBrush();

                    GradientStop gradientStop1 = new GradientStop();
                    gradientStop1.Color = Colors.White;
                    gradientStop1.Offset = 0;
                    Brush3.GradientStops.Add(gradientStop1);

                    GradientStop gradientStop2 = new GradientStop();
                    gradientStop2.Color = (Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), "#B5AC99");
                    gradientStop2.Offset = 1;
                    Brush3.GradientStops.Add(gradientStop2);
                    return Brush3;
                }
            }
            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

#if !SFDATAGRID_STORE
    /// <summary>
    /// This class represents the StyleConverterforDiscount
    /// </summary>
    internal class StyleConverterforDiscount : IValueConverter
    {
        /// <summary>
        /// Convert value to brush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double _value;
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                _value = double.Parse(value.ToString(), NumberStyles.Currency);
                if (_value > 5)
                {
                    LinearGradientBrush Brush1 = new LinearGradientBrush();

                    GradientStop gradientStop1 = new GradientStop();
                    gradientStop1.Color = Colors.White;
                    gradientStop1.Offset = 0;
                    Brush1.GradientStops.Add(gradientStop1);

                    GradientStop gradientStop2 = new GradientStop();
                    gradientStop2.Color = (Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), "#FED06A");
                    gradientStop2.Offset = 1;
                    Brush1.GradientStops.Add(gradientStop2);
                    return Brush1;
                }
            }
            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This class represents the StyleConverterforUnitPrice
    /// </summary>
    internal class StyleConverterforUnitPrice : IValueConverter
    {
        /// <summary>
        /// Convert value to brush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double _value;
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                _value = double.Parse(value.ToString(), NumberStyles.Currency);

                if (_value > 30)
                {
                    LinearGradientBrush Brush2 = new LinearGradientBrush();

                    GradientStop gradientStop1 = new GradientStop();
                    gradientStop1.Color = Colors.White;
                    gradientStop1.Offset = 0;
                    Brush2.GradientStops.Add(gradientStop1);

                    GradientStop gradientStop2 = new GradientStop();
                    gradientStop2.Color = (Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), "#94C3FF");
                    gradientStop2.Offset = 1;
                    Brush2.GradientStops.Add(gradientStop2);
                    return Brush2;
                }
            }
            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }

    /// <summary>
    /// This class represents the RowStyelConverter
    /// </summary>
    public class RowStyelConverter : IValueConverter
    {
        /// <summary>
        /// Convert value to brush
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var rating = (int)value;
            if (rating == 5)
                return new SolidColorBrush(Colors.Transparent);
            Brush brush = rating < 5 ? new SolidColorBrush((Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), "#FFFFD356")) :
                                      new SolidColorBrush((Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), "#FF70FCA0"));
            if (rating > 5)
            {
                rating = rating - 5;
                brush.Opacity = Math.Abs((double)rating * 2) / 10;
            }
            else
                brush.Opacity = Math.Abs((double)rating * 2) / 10;
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
#endif
}
