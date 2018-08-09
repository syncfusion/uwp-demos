using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BI.PivotChart.Converter
{
    public class CustomAdornmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Syncfusion.UI.Xaml.Charts.ChartPieAdornment)
            {
                var adornment = value as Syncfusion.UI.Xaml.Charts.ChartPieAdornment;
                (adornment.Series as Syncfusion.UI.Xaml.Charts.PieSeries).PieCoefficient = 0.5;
                if (parameter != null) 
                {
                    SolidColorBrush interior = (adornment.Interior as SolidColorBrush);
                    if (parameter.ToString() == "Background")
                        return new SolidColorBrush(Color.FromArgb(200, interior.Color.R, interior.Color.G, interior.Color.B));
                    else
                        return interior;
                }
                else
                {
                    var label = string.Empty;
                    double totalValues = 0d;
                    if (adornment != null)
                    {
                        label = (adornment.Item as Syncfusion.UI.Xaml.PivotChart.PivotChartPoint).BindingX;
                        if (adornment.Series.ItemsSource != null)
                        {
                            foreach (var item in adornment.Series.ItemsSource)
                                totalValues += (item as Syncfusion.UI.Xaml.PivotChart.PivotChartPoint).BindingY;
                        }
                        label += " - (" + Math.Round(((adornment.Item as Syncfusion.UI.Xaml.PivotChart.PivotChartPoint).BindingY / totalValues) * 100, 2).ToString() + "%" + ")";
                    }
                    return label;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
