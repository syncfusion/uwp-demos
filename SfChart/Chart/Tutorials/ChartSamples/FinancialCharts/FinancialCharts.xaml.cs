#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class FinancialCharts : SampleLayout
    {
        HiLoChartViewModel hiLowChart;
        public FinancialCharts()
        {
            hiLowChart = new HiLoChartViewModel();
            this.InitializeComponent();
        }

        private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedIndex == 1 && financialChart != null)
            {
                HiLoSeries hiLoSeries = new HiLoSeries();
                hiLoSeries.XBindingPath = "Date";
                hiLoSeries.High = "High";
                hiLoSeries.Low = "Low";
                hiLoSeries.ItemsSource = hiLowChart.StockPriceDetails;
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.PrimaryAxis.PlotOffset = 10;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Clear();
                financialChart.Series.Add(hiLoSeries);
            }
            else if (comboBox.SelectedIndex == 0 && financialChart != null)
            {
                HiLoOpenCloseSeries hiLoOpenClose = new HiLoOpenCloseSeries();
                hiLoOpenClose.XBindingPath = "Date";
                hiLoOpenClose.High = "High";
                hiLoOpenClose.Low = "Low";
                hiLoOpenClose.Open = "Open";
                hiLoOpenClose.Close = "Close";
                hiLoOpenClose.ItemsSource = hiLowChart.StockPriceDetails;
                financialChart.Series.Clear();
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Add(hiLoOpenClose);

            }
            else if (comboBox.SelectedIndex == 2 && financialChart != null)
            {
                CandleSeries chartSeries = new CandleSeries();
                chartSeries.XBindingPath = "Date";
                chartSeries.High = "High";
                chartSeries.Low = "Low";
                chartSeries.Open = "Open";
                chartSeries.Close = "Close";
                chartSeries.ItemsSource = hiLowChart.StockPriceDetails;
                chartSeries.BearFillColor = new SolidColorBrush(Colors.Red);
                chartSeries.BullFillColor = new SolidColorBrush(Colors.Green);
                financialChart.Series.Clear();
                financialChart.PrimaryAxis.ZoomFactor = 1;
                financialChart.SecondaryAxis.ZoomFactor = 1;
                financialChart.Series.Add(chartSeries);
            }
        }

        public override void Dispose()
        {
            if (this.DataContext is HiLoChartViewModel)
                (this.DataContext as HiLoChartViewModel).Dispose();

            if (this.financialChart != null)
            {
                foreach (var series in this.financialChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.financialChart = null;
            }

            if (this.grid.DataContext != null)
                this.grid.DataContext = null;
            this.grid.Resources = null;

            base.Dispose();
        }
    }

    public class HiLoChartViewModel : IDisposable
    {
        public HiLoChartViewModel()
        {
            this.StockPriceDetails = new ObservableCollection<StockPriceData>();
            DateTime date = new DateTime(2012, 1, 1);
            this.StockPriceDetails.Add(new StockPriceData()
            {
                Date = date.AddDays(1),
                Open = 873.8,
                High = 878.85,
                Low = 855.5,
                Close = 860.5
            });
            this.StockPriceDetails.Add(new StockPriceData()
            {
                Date = date.AddDays(2),
                Open = 861,
                High = 868.4,
                Low = 835.2,
                Close = 843.45
            });
            this.StockPriceDetails.Add(new StockPriceData()
            {
                Date = date.AddDays(3),
                Open = 846.15,
                High = 853,
                Low = 838.5,
                Close = 847.5
            });
            this.StockPriceDetails.Add(new StockPriceData()
            {
                Date = date.AddDays(4),
                Open = 846,
                High = 860.75,
                Low = 841,
                Close = 855
            });
            this.StockPriceDetails.Add(new StockPriceData()
            {
                Date = date.AddDays(5),
                Open = 841,
                High = 845,
                Low = 827.85,
                Close = 838.65
            });
            this.StockPriceDetails.Add(new StockPriceData()
            {
                Date = date.AddDays(6),
                Open = 846,
                High = 874.5,
                Low = 841,
                Close = 860.75
            });
            this.StockPriceDetails.Add(new StockPriceData()
            {
                Date = date.AddDays(7),
                Open = 865,
                High = 872,
                Low = 865,
                Close = 868.9
            });
        }
        public ObservableCollection<StockPriceData> StockPriceDetails
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.StockPriceDetails != null)
                this.StockPriceDetails.Clear();
        }
    }

    public class StockPriceData
    {
        public DateTime TimeStamp
        {
            get;
            set;
        }

        public double Last
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string Level
        {
            get;
            set;
        }

        public double NumLevel
        {
            get;
            set;
        }

        public double High
        {
            get;
            set;
        }

        public double Low
        {
            get;
            set;
        }

        public double Open
        {
            get;
            set;
        }

        public double Close
        {
            get;
            set;
        }
    }
}
