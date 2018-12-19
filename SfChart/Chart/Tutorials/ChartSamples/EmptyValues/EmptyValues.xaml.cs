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

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public sealed partial class EmptyValues : SampleLayout
    {
        public EmptyValues()
        {
            this.InitializeComponent();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                csChartAdornmentInfo.ShowLabel = true;
                csChartAdornmentInfo.FontSize = 13;
                csChartAdornmentInfo.Foreground = new SolidColorBrush(Colors.White);
                csChartAdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
                csChartAdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
                csChartAdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            }
        }

        private void OnEmptyPointInteriorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;

            if (EmptyPointChart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointInterior = new SolidColorBrush(Colors.Blue);
                }
                else if (select.SelectedIndex == 1)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointInterior = new SolidColorBrush(Colors.Green);
                }
                else if (select.SelectedIndex == 2)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointInterior = new SolidColorBrush(Colors.Orange);
                }
                else if (select.SelectedIndex == 3)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointInterior = new SolidColorBrush(Colors.Purple);
                }
                else if (select.SelectedIndex == 4)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointInterior = new SolidColorBrush(Colors.RoyalBlue);
                }
            }
        }

        private void OnEmptyPointStylesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (EmptyPointChart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointStyle = EmptyPointStyle.Interior;
                }
                else if (select.SelectedIndex == 1)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointStyle = EmptyPointStyle.Symbol;
                }
                else if (select.SelectedIndex == 2)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointStyle = EmptyPointStyle.SymbolAndInterior;
                }
            }
        }

        private void OnEmptyPointValuesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox select = sender as ComboBox;
            if (EmptyPointChart != null)
            {
                if (select.SelectedIndex == 0)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointValue = EmptyPointValue.Zero;
                }
                else if (select.SelectedIndex == 1)
                {
                    (EmptyPointChart.Series[0] as ChartSeries).EmptyPointValue = EmptyPointValue.Average;

                }
            }
        }

        public override void Dispose()
        {
            if ((this.DataContext as EmptypointViewModel) != null)
                (this.DataContext as EmptypointViewModel).Dispose();

            if (this.EmptyPointChart != null)
            {
                foreach (var series in this.EmptyPointChart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.EmptyPointChart = null;
            }

            if(this.mainGrid.DataContext != null)
                this.mainGrid.DataContext = null;
            this.mainGrid.Resources = null;

            base.Dispose();
        }
    }

    public class EmptypointViewModel : IDisposable
    {
        public List<Model> Data
        {
            get;
            set;
        }

        public Array EmptyPointStyles
        {
            get
            {
                return Enum.GetValues(typeof(EmptyPointStyle));
            }
        }

        public Array EmptyPointValues
        {
            get
            {
                return Enum.GetValues(typeof(EmptyPointValue));
            }
        }

        public EmptypointViewModel()
        {
            Data = new List<Model>();

            Data.Add(new Model("1984", double.NaN));
            Data.Add(new Model("1985", 2));
            Data.Add(new Model("1986", 1));
            Data.Add(new Model("1987", 3));
            Data.Add(new Model("1988", null));
            Data.Add(new Model("1989", 2));
            Data.Add(new Model("1990", 1));
            Data.Add(new Model("1991", 1));
            Data.Add(new Model("1992", null));
            Data.Add(new Model("1993", 4));

            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo.HorizontalAlignment = HorizontalAlignment.Center;
            AdornmentInfo.VerticalAlignment = VerticalAlignment.Center;
            AdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo.ShowLabel = true;
            AdornmentInfo.FontSize = 12;
            AdornmentInfo.Foreground = new SolidColorBrush(Colors.White);
        }
        public ChartAdornmentInfo AdornmentInfo { get; set; }

        public void Dispose()
        {
            if (this.Data != null)
                this.Data.Clear();
        }
    }

    public class Model
    {
        public string Year
        {
            get;
            set;
        }

        public double? Count
        {
            get;
            set;
        }

        public Model(string year, double? count)
        {
            Year = year;
            Count = count;
        }
    }
}
