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
    public sealed partial class ErrorBars : SampleLayout
    {
        double value;
        public ErrorBars()
        {
            this.InitializeComponent();
        }

        private void OnVerticalTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (sfchart == null || string.IsNullOrEmpty(text.Text) || !double.TryParse(text.Text, out value))
                return;
            (sfchart.Series[1] as ErrorBarSeries).VerticalErrorValue = Convert.ToDouble(text.Text);
        }

        private void OnHorizontalTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (sfchart == null || string.IsNullOrEmpty(text.Text) || !double.TryParse(text.Text, out value))
                return;
            (sfchart.Series[1] as ErrorBarSeries).HorizontalErrorValue = Convert.ToDouble(text.Text);
        }

        private void OnTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            if (sfchart != null)
                if (combo != null)
                    switch (combo.SelectedIndex)
                    {
                        case 0:
                            (sfchart.Series[1] as ErrorBarSeries).Type = ErrorBarType.Fixed;
                            break;
                        case 1:
                            (sfchart.Series[1] as ErrorBarSeries).Type = ErrorBarType.Percentage;
                            break;
                        case 2:
                            (sfchart.Series[1] as ErrorBarSeries).Type = ErrorBarType.StandardDeviation;
                            break;
                        case 3:
                            (sfchart.Series[1] as ErrorBarSeries).Type = ErrorBarType.StandardErrors;
                            break;
                        default:
                            (sfchart.Series[1] as ErrorBarSeries).Type = ErrorBarType.Custom;
                            break;
                    }
        }

        private void OnModeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            if (sfchart == null) return;
            switch (combo.SelectedIndex)
            {
                case 0:
                    (sfchart.Series[1] as ErrorBarSeries).Mode = ErrorBarMode.Horizontal;
                    break;
                case 1:
                    (sfchart.Series[1] as ErrorBarSeries).Mode = ErrorBarMode.Vertical;
                    break;
                default:
                    (sfchart.Series[1] as ErrorBarSeries).Mode = ErrorBarMode.Both;
                    break;
            }
        }

        public override void Dispose()
        {
            if ((this.DataContext as EnergyProductionDataSample) != null)
                (this.DataContext as EnergyProductionDataSample).Dispose();

            if (this.sfchart != null)
            {
                foreach (var series in this.sfchart.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.sfchart = null;
            }

            this.mainGrid.Resources = null;

            base.Dispose();
        }
    }

    public class EnergyProductionDataSample : IDisposable
    {
        public EnergyProductionDataSample()
        {
            EnergyProductions = new ObservableCollection<EnergyProduction>();
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 1,
                Region = "America",
                Country = "Canada",
                Coal = 400,
                Oil = 100,
                Gas = 175,
                Nuclear = 225,
                VerticalErrorValue = 20,
                HorizontalErrorValue = 5
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 2,
                Region = "Asia",
                Country = "China",
                Coal = 925,
                Oil = 200,
                Gas = 350,
                Nuclear = 400,
                VerticalErrorValue = 30,
                HorizontalErrorValue = 3
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 3,
                Region = "Europe",
                Country = "Russia",
                Coal = 550,
                Oil = 200,
                Gas = 250,
                Nuclear = 475,
                VerticalErrorValue = 28,
                HorizontalErrorValue = 2
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 4,
                Region = "Asia",
                Country = "Australia",
                Coal = 450,
                Oil = 100,
                Gas = 150,
                Nuclear = 175,
                VerticalErrorValue = 20,
                HorizontalErrorValue = 1
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 5,
                Region = "America",
                Country = "United States",
                Coal = 800,
                Oil = 250,
                Gas = 475,
                Nuclear = 575,
                VerticalErrorValue = 40,
                HorizontalErrorValue = 2.5
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 6,
                Region = "Europe",
                Country = "France",
                Coal = 375,
                Oil = 150,
                Gas = 350,
                Nuclear = 275,
                VerticalErrorValue = 55,
                HorizontalErrorValue = 0.5
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 7,
                Region = "Europe",
                Country = "Itly",
                Coal = 289,
                Oil = 150,
                Gas = 350,
                Nuclear = 275,
                VerticalErrorValue = 15,
                HorizontalErrorValue = 0.11
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 8,
                Region = "Asia",
                Country = "India",
                Coal = 654,
                Oil = 150,
                Gas = 350,
                Nuclear = 275,
                VerticalErrorValue = 20,
                HorizontalErrorValue = 0.4
            });
            EnergyProductions.Add(new EnergyProduction
            {
                ID = 9,
                Region = "Asia",
                Country = "China",
                Coal = 765,
                Oil = 180,
                Gas = 450,
                Nuclear = 375,
                VerticalErrorValue = 65,
                HorizontalErrorValue = 0.2
            });
        }

        public ObservableCollection<EnergyProduction> EnergyProductions
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.EnergyProductions != null)
                this.EnergyProductions.Clear();

        }
    }

    public class EnergyProduction
    {
        public double ID
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string Region
        {
            get;
            set;
        }

        public string Year
        {
            get;
            set;
        }

        public double Nuclear
        {
            get;
            set;
        }

        public double Coal
        {
            get;
            set;
        }

        public double Oil
        {
            get;
            set;
        }

        public double Gas
        {
            get;
            set;
        }

        public double HorizontalErrorValue
        {
            get;
            set;
        }

        public double VerticalErrorValue
        {
            get;
            set;
        }
    }
}
