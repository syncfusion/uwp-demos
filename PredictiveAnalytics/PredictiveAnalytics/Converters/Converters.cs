using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Syncfusion.UI.Xaml.Charts;

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    public class ColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value != null && (value is ChartAdornment))
            {
                ChartAdornment pieAdornment = value as ChartAdornment;
                int index = pieAdornment.Series.Adornments.IndexOf(pieAdornment);
                SolidColorBrush brush = pieAdornment.Series.ColorModel.GetBrush(index) as SolidColorBrush;
                return ApplyLight(brush.Color);
            }
            else if (value != null && (value is string))
            {
                string sampleStatus = value.ToString();

                if (sampleStatus == "NEW")
                {
                    return new SolidColorBrush(Color.FromArgb(255, 20, 168, 142));
                }
                else if (sampleStatus == "PREVIEW")
                {
                    return new SolidColorBrush(Color.FromArgb(255, 246, 141, 56));
                }
                else if (sampleStatus == "UPDATED")
                {
                    return new SolidColorBrush(Color.FromArgb(255, 38, 139, 181));
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        private SolidColorBrush ApplyLight(Color color)
        {
            return new SolidColorBrush(Color.FromArgb(color.A, (byte)(color.R * 0.9), (byte)(color.G * 0.9), (byte)(color.B * 0.9)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return targetType;
        }
    }

    public class LabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ChartAdornment)
            {
                ChartAdornment pieAdornment = value as ChartAdornment;
                return String.Format((pieAdornment.Item as ClusterDetails).ID + "\nsize : " + pieAdornment.YData);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                string sampleStatus = value.ToString();

                if (sampleStatus == "NEW")
                {
                    return "NEW";
                }
                else if (sampleStatus == "PREVIEW")
                {
                    return "PREVIEW";
                }
                else if (sampleStatus == "UPDATED")
                {
                    return "UPDATED";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return targetType;
        }
    }

    public class RichTextBlockHelper
    {
        public static Paragraph GetContent(DependencyObject obj)
        {
            return (Paragraph)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, Paragraph value)
        {
            obj.SetValue(ContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text and This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
       DependencyProperty.RegisterAttached("Content", typeof(Paragraph),
       typeof(RichTextBlockHelper),
       new PropertyMetadata(null, OnContentChanged));

        private static void OnContentChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
			var control = sender as RichTextBlock;
            Paragraph paragraph = e.NewValue as Paragraph;
            if (control != null && paragraph != null)
            {
                control.Blocks.Clear();                
                control.Blocks.Add(paragraph);
            } 
        }

    }

    class GridColumnSizerExt : Syncfusion.UI.Xaml.Grid.GridColumnSizer
    {
        public GridColumnSizerExt(Syncfusion.UI.Xaml.Grid.SfDataGrid dataGrid)
            : base(dataGrid)
        {

        }

        protected override void SetStarWidth(double remainingColumnWidth, IEnumerable<Syncfusion.UI.Xaml.Grid.GridColumn> remainingColumns)
        {
            if (remainingColumnWidth >= 150)
            {
                base.SetStarWidth(remainingColumnWidth, remainingColumns);
                return;
            }

            foreach (var column in remainingColumns)
            {
                this.SetNoneWidth(column, column.ActualWidth);
            }
        }

        protected override double SetNoneWidth(Syncfusion.UI.Xaml.Grid.GridColumn column, double width)
        {

            if (width <= 150 || double.IsNaN(width))
            {
                var autowidth = CalculateAutoFitWidth(column, true);
                return base.SetNoneWidth(column, autowidth);
            }

            return base.SetNoneWidth(column, width);
        }
    }
}
