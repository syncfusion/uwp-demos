using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public class ChartTechnicalIndicatorBehavior : ChartBehavior
    {

        // ComboBox IndicatorSource1=new ComboBox();

        internal ComboBox IndicatorSource2 = new ComboBox();

        // TextBlock TextSource1 = new TextBlock();

        TextBlock TextSource2 = new TextBlock();

        public ObservableCollection<ChartSeries> indicatorsCollection = new ObservableCollection<ChartSeries>();

        public ChartTechnicalIndicatorBehavior()
        {


        }

        string[] technicalIndicators = {"--Add Indicators--","AverageTrueRangeIndicator","AccumulationDistributionIndicator", "BollingerBandIndicator", "ExponentialAverageIndicator",
                                              "MACDTechnicalIndicator","MomentumTechnicalIndicator","RSITechnicalIndicator","SimpleAverageIndicator","StochasticTechnicalIndicator","TriangularAverageIndicator"};

        public string[] TechnicalIndicators
        {
            get
            {
                return technicalIndicators;
            }
        }

        protected override void AttachElements()
        {
            //IndicatorSource1.Width = 160;
            //IndicatorSource1.Height = 35;

            IndicatorSource2.Width = 160;
            IndicatorSource2.Height = 35;

            //IndicatorSource1.ItemsSource = TechnicalIndicators;

            //IndicatorSource1.SelectedIndex = 0;

            IndicatorSource2.ItemsSource = TechnicalIndicators;

            IndicatorSource2.SelectedIndex = 0;

            IndicatorSource2.SelectionChanged += IndicatorSource2_SelectionChanged;

            //IndicatorSource1.SelectionChanged += IndicatorSource1_SelectionChanged;

            //this.AdorningCanvas.Children.Add(IndicatorSource1);

            this.AdorningCanvas.Children.Add(IndicatorSource2);

            //this.AdorningCanvas.Children.Add(TextSource1);

            this.AdorningCanvas.Children.Add(TextSource2);

            this.ChartArea.SizeChanged += ChartArea_SizeChanged;
        }

        private FinancialTechnicalIndicator Addindicator(string value, int rowIndex)
        {
            FinancialTechnicalIndicator indic;
            switch (value)
            {
                case "AccumulationDistributionIndicator":
                    indic = new AccumulationDistributionIndicator();
                    break;

                case "AverageTrueRangeIndicator":
                    indic = new AverageTrueRangeIndicator();
                    break;

                case "BollingerBandIndicator":
                    indic = new BollingerBandIndicator();
                    break;
                case "ExponentialAverageIndicator":
                    indic = new ExponentialAverageIndicator();
                    break;

                case "MACDTechnicalIndicator":
                    indic = new MACDTechnicalIndicator();
                    break;
                case "MomentumTechnicalIndicator":
                    indic = new MomentumTechnicalIndicator();
                    break;
                case "RSITechnicalIndicator":
                    indic = new RSITechnicalIndicator();
                    break;
                case "SimpleAverageIndicator":

                    indic = new SimpleAverageIndicator();
                    break;
                case "StochasticTechnicalIndicator":
                    indic = new StochasticTechnicalIndicator();
                    break;
                case "TriangularAverageIndicator":
                    indic = new TriangularAverageIndicator();
                    break;
                default:
                    return null;
            }

            indic.SeriesName = rowIndex == 0 ? this.ChartArea.VisibleSeries[1].Name : this.ChartArea.VisibleSeries[0].Name;
            ChartSeries Series = this.ChartArea.VisibleSeries[indic.SeriesName] as ChartSeries;
            indic.XBindingPath = "TimeStamp";
            indic.High = "High";
            indic.Low = "Low";
            indic.Open = "Open";
            indic.Close = "Last";
            indic.Volume = "Volume";
            Binding binding = new Binding();
            binding.Path = new PropertyPath("ItemsSource");
            binding.Source = Series;
            binding.Mode = BindingMode.TwoWay;
            indic.SetBinding(FinancialTechnicalIndicator.ItemsSourceProperty, binding);

            if (rowIndex == 0)
            {
                var supportAxes = this.ChartArea.VisibleSeries[1] as ISupportAxes;
                if (supportAxes != null)
                    indic.YAxis = supportAxes.ActualYAxis as RangeAxisBase;
                UI.Xaml.Charts.SfChart.SetRow(indic.YAxis, 1);
            }
            else
            {
                indic.YAxis = this.ChartArea.SecondaryAxis;
                UI.Xaml.Charts.SfChart.SetRow(indic.YAxis, 0);
            }
            return indic;
        }

        void IndicatorSource2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var indicator = Addindicator((string)IndicatorSource2.SelectedItem, 1);

            if (indicator != null)
            {
                this.ChartArea.TechnicalIndicators.Clear();
                this.ChartArea.TechnicalIndicators.Add(indicator);
                NumericalAxis axis = new NumericalAxis();
                axis.OpposedPosition = true;
                axis.ShowGridLines = false;
                axis.Visibility = Visibility.Collapsed;
                indicator.YAxis = axis;
                UI.Xaml.Charts.SfChart.SetRow(indicator.YAxis, 0);
            }
        }

        private List<int> rmIndexes = new List<int>();

        //void IndicatorSource1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var indicator = Addindicator((string)IndicatorSource1.SelectedItem, 0);
        //    if (indicator != null)
        //    {
        //        indicatorsCollection.Add(indicator);
        //        foreach (var item in indicatorsCollection)
        //        {
        //            ISupportAxes indicatorAxis = item as ISupportAxes;
        //            if (this.ChartArea.TechnicalIndicators.Contains(item))
        //            {
        //                var index = SfChart.GetRow(indicatorAxis.ActualYAxis);
        //                if (index == 1)
        //                {
        //                    this.ChartArea.TechnicalIndicators.Remove(item);
        //                    rmIndexes.Add(indicatorsCollection.IndexOf(item));
        //                }
        //            }
        //            else
        //            {
        //                var index = SfChart.GetRow(indicatorAxis.ActualYAxis);
        //                if (index == 1)
        //                {
        //                    NumericalAxis axis = new NumericalAxis();
        //                    axis.OpposedPosition = true;
        //                    axis.Visibility = Visibility.Collapsed;
        //                    indicatorAxis.YAxis = axis;
        //                    SfChart.SetRow(indicatorAxis.YAxis, 1);
        //                    this.ChartArea.TechnicalIndicators.Add(item);
        //                }
        //            }
        //        }
        //        foreach (var item in rmIndexes)
        //        {
        //            indicatorsCollection.RemoveAt(item);
        //        }
        //        rmIndexes.Clear();
        //    }
        //}


        protected override void OnPointerReleased(global::Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            //  this.IndicatorSource1.Visibility = Visibility.Visible;
            this.IndicatorSource2.Visibility = Visibility.Visible;
        }

        void ChartArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            double[] top = new double[5];

            var left = this.ChartArea.SeriesClipRect.Left;

            var right = this.ChartArea.SeriesClipRect.Right;


            for (int i = 0; i < this.ChartArea.RowDefinitions.Count; i++)
            {
                top[i] = this.ChartArea.RowDefinitions[i].RowTop;
            }

            var areaBorder = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(this.ChartArea, 0), 0) as Border;
            //areaBorder.Padding = new Thickness(15, this.ChartArea.SeriesClipRect.Top + IndicatorSource1.ActualHeight, 15, 15);

            //Canvas.SetLeft(IndicatorSource1, right - IndicatorSource1.ActualWidth - 48);
            //Canvas.SetTop(IndicatorSource1, top[1]-IndicatorSource1.ActualHeight-22);
            //Canvas.SetZIndex(IndicatorSource1, 100);

            Canvas.SetLeft(IndicatorSource2, right - IndicatorSource2.ActualWidth - 48);
            Canvas.SetTop(IndicatorSource2, top[0] - IndicatorSource2.ActualHeight + 22);
            Canvas.SetZIndex(IndicatorSource2, 100);

            //Canvas.SetLeft(TextSource1, left + TextSource1.ActualWidth);
            //Canvas.SetTop(TextSource1, top[1] - TextSource1.ActualHeight - 40);
            //Canvas.SetZIndex(TextSource1, 80);

            Canvas.SetLeft(TextSource2, left + TextSource2.ActualWidth);
            Canvas.SetTop(TextSource2, top[0] - TextSource2.ActualHeight - 17);
            Canvas.SetZIndex(TextSource2, 80);

            TextSource2.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x33, 0x33));
            //TextSource1.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x33, 0x33));

            TextSource2.FontSize = 35;

            //TextSource1.FontSize = 35;

            Binding binding = new Binding();
            binding.Source = this.ChartArea;
            binding.Path = new PropertyPath("DataContext");
            binding.Mode = BindingMode.TwoWay;
            binding.Converter = new TextBlockConverter();
            //TextSource1.SetBinding(TextBlock.TextProperty, binding);
            TextSource2.SetBinding(TextBlock.TextProperty, binding);
        }
    }

    public class TextBlockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Stocks data = value as Stocks;
            return data.PreviousClose;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
