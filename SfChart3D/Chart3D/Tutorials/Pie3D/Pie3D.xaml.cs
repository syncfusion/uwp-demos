using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart3D
{
    public sealed partial class Pie3D : SampleLayout
    {
        public Pie3D()
        {
            this.InitializeComponent();
            psChartAdornmentInfo3D.SegmentLabelContent = LabelContent.LabelContentPath;
            psChartAdornmentInfo3D.VerticalAlignment = VerticalAlignment.Center;
            psChartAdornmentInfo3D.HorizontalAlignment = HorizontalAlignment.Center;
            psChartAdornmentInfo3D.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            psChartAdornmentInfo3D.ShowLabel = true;
            psChartAdornmentInfo3D.LabelTemplate = grid.Resources["labelTemplate1"] as DataTemplate;
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                ScatterChart.Margin = new Thickness(-12,-10,-5,-10);
                ScatterChart.Tilt = -6;
                ScatterChart.Rotation = 25;
            }
            else
            {
                ScatterChart.Margin = new Thickness(70, 20, 75, 25);
            }
        }

        public override void Dispose()
        {
            if (this.grid.DataContext is ViewModel)
                (this.grid.DataContext as ViewModel).Dispose();

            if (this.ScatterChart != null)
            {
                foreach (var series in this.ScatterChart.Series)
                {
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                }
                this.ScatterChart = null;
            }

            this.grid.Resources = null;

            base.Dispose();
        }
    }

    public class ViewModel : IDisposable
    {
        public ViewModel()
        {
            this.Expenditure = new List<Model>();
            Expenditure.Add(new Model() { Expense = "E-Mail", Amount = 20d });
            Expenditure.Add(new Model() { Expense = "Skype", Amount = 23d });
            Expenditure.Add(new Model() { Expense = "Phone", Amount = 12d });
            Expenditure.Add(new Model() { Expense = "Sms", Amount = 19d });
            Expenditure.Add(new Model() { Expense = "Facebook", Amount = 10d });
            Expenditure.Add(new Model() { Expense = "Twitter", Amount = 10d });
            Expenditure.Add(new Model() { Expense = "LinkedIn", Amount = 9d });

        }

        public IList<Model> Expenditure
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.Expenditure != null)
                this.Expenditure.Clear();
        }
    }

    public class Model
    {
        public string Expense
        {
            get;
            set;
        }

        public double Amount
        {
            get;
            set;
        }
    }

    public class ColorConverter : IValueConverter
    {
        private SolidColorBrush ApplyLight(Color color)
        {
            return new SolidColorBrush(Color.FromArgb(color.A, (byte)(color.R * 0.9), (byte)(color.G * 0.9), (byte)(color.B * 0.9)));
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                ChartAdornment pieAdornment = value as ChartAdornment;
                int index = pieAdornment.Series.Adornments.IndexOf(pieAdornment);
                var seriesInterior = pieAdornment.Series.Interior as SolidColorBrush;
                var brushes = pieAdornment.Series.ColorModel.CustomBrushes;
                seriesInterior = seriesInterior == null ? brushes == null ? pieAdornment.Series.ColorModel.GetMetroBrushes()[index] as SolidColorBrush : brushes[index] as SolidColorBrush : seriesInterior;
                return ApplyLight(seriesInterior.Color);
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
            ChartAdornment pieAdornment = value as ChartAdornment;
            int index = pieAdornment.Series.Adornments.IndexOf(pieAdornment);
            CategoryDataViewModel view = pieAdornment.Series.DataContext as CategoryDataViewModel;
            var path = (pieAdornment.Series as XyDataSeries3D).YBindingPath;
            var yValue = "";
            if (path == "Plastic")
            {
                yValue = view.CategoricalDatas[index].Plastic.ToString();
            }
            else if (path == "Iron")
            {
                yValue = view.CategoricalDatas[index].Iron.ToString();
            }
            else if (path == "Metal")
            {
                yValue = view.CategoricalDatas[index].Metal.ToString();
            }
            return path.ToUpper() + " : " + yValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class LabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            return String.Format("{0} %", pieAdornment.YData);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
