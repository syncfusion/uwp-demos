#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public class LineChartViewModel : IDisposable
    {

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

        public Array LegendPosition
        {
            get
            {
                return Enum.GetValues(typeof(LegendPosition));
            }
        }

        public Array ChartDock
        {
            get
            {
                return Enum.GetValues(typeof(ChartDock));
            }
        }

        public Array VerticalAlignment
        {
            get
            {
                return Enum.GetValues(typeof(VerticalAlignment));
            }
        }

        public Array HorizontalAlignment
        {
            get
            {
                return Enum.GetValues(typeof(HorizontalAlignment));
            }
        }

        public LineChartViewModel()
        {
            this.power = new ObservableCollection<Power>();
            DateTime yr = new DateTime(2002, 5, 1);
            power.Add(new Power() { Year = yr.AddYears(0), IND = 28, GER = 31, UK = 36, FRA = 39, AUS = 45 });
            power.Add(new Power() { Year = yr.AddYears(1), IND = 24, GER = 28, UK = 32, FRA = 36, AUS = 40 });
            power.Add(new Power() { Year = yr.AddYears(2), IND = 26, GER = 30, UK = 34, FRA = 40, AUS = 45 });
            power.Add(new Power() { Year = yr.AddYears(3), IND = 27, GER = 36, UK = 42, FRA = 44, AUS = 49 });
            power.Add(new Power() { Year = yr.AddYears(4), IND = 32, GER = 36, UK = 40, FRA = 45, AUS = 61 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                power.Add(new Power() { Year = yr.AddYears(5), IND = 35, GER = 39, UK = 45, FRA = 48, AUS = 69 });
                power.Add(new Power() { Year = yr.AddYears(6), IND = 30, GER = 37, UK = 50, FRA = 46, AUS = 74 });
            }

            AdornmentFactory = new ResourceFactory();

            AdornmentInfo1 = new ChartAdornmentInfo();
            AdornmentInfo2 = new ChartAdornmentInfo();
            AdornmentInfo3 = new ChartAdornmentInfo();
            AdornmentInfo5 = new ChartAdornmentInfo();
            AdornmentInfo6 = new ChartAdornmentInfo();
            AdornmentInfo7 = new ChartAdornmentInfo();

            AdornmentInfo1.Symbol = ChartSymbol.Custom;
            AdornmentInfo1.ShowLabel = true;
            AdornmentInfo1.LabelTemplate = AdornmentFactory.labelTemplate1;

            AdornmentInfo2.Symbol = ChartSymbol.Custom;
            AdornmentInfo2.ShowLabel = true;
            AdornmentInfo2.LabelTemplate = AdornmentFactory.labelTemplate2;

            AdornmentInfo3.Symbol = ChartSymbol.Custom;
            AdornmentInfo3.ShowLabel = true;
            AdornmentInfo3.LabelTemplate = AdornmentFactory.labelTemplate3;
            
            AdornmentInfo5.ShowLabel = true;
            AdornmentInfo5.LabelTemplate = AdornmentFactory.labelTemplate4;

            AdornmentInfo6.ShowLabel = true;
            AdornmentInfo6.Symbol = ChartSymbol.Custom;
            AdornmentInfo6.SymbolInterior = new SolidColorBrush(Colors.Green);
            AdornmentInfo6.LabelTemplate = AdornmentFactory.labelTemplate5;

            AdornmentInfo7.ShowLabel = true;
            AdornmentInfo7.Symbol = ChartSymbol.Custom;
            AdornmentInfo7.SymbolInterior = new SolidColorBrush(Colors.Blue);
            AdornmentInfo7.LabelTemplate = AdornmentFactory.labelTemplate6;
        }

        public ResourceFactory AdornmentFactory { get; set; }

        public ChartAdornmentInfo AdornmentInfo1 { get; set; }

        public ChartAdornmentInfo AdornmentInfo2 { get; set; }

        public ChartAdornmentInfo AdornmentInfo3 { get; set; }

        public ChartAdornmentInfo AdornmentInfo5 { get; set; }

        public ChartAdornmentInfo AdornmentInfo6 { get; set; }

        public ChartAdornmentInfo AdornmentInfo7 { get; set; }

        public ObservableCollection<Power> power
        {
            get;
            set;
        }


        public void Dispose()
        {
            if ((power as ObservableCollection<Power>) != null)
            {
                (power as ObservableCollection<Power>).Clear();
            }

        }
    }

    public class ScatterViewModel : IDisposable
    {
        public ScatterViewModel()
        {
            this.DataPoints = new ObservableCollection<ScatterModel>();
            DataPoints.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.333f, WaitingTime = 74 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.283f, WaitingTime = 62 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 85 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.883f, WaitingTime = 55 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 88 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 85 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.950f, WaitingTime = 51 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 85 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.917f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.200f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 47 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.167f, WaitingTime = 52 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 62 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.600f, WaitingTime = 52 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 51 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 47 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.450f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.067f, WaitingTime = 69 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 74 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.967f, WaitingTime = 55 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.850f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.433f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.300f, WaitingTime = 73 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.467f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.367f, WaitingTime = 66 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.033f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 74 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 52 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 48 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.833f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 59 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.783f, WaitingTime = 90 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 58 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 58 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 73 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.317f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 64 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.100f, WaitingTime = 53 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.633f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 59 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 75 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.716f, WaitingTime = 90 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.833f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.733f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.883f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.717f, WaitingTime = 71 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.667f, WaitingTime = 64 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.317f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.233f, WaitingTime = 59 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 48 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.817f, WaitingTime = 60 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.400f, WaitingTime = 92 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.067f, WaitingTime = 65 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 73 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.033f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.967f, WaitingTime = 56 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 71 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.983f, WaitingTime = 62 });
            DataPoints.Add(new ScatterModel() { Eruptions = 5.067f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 60 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.567f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.883f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.600f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.133f, WaitingTime = 75 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.100f, WaitingTime = 70 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.633f, WaitingTime = 65 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.067f, WaitingTime = 73 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.933f, WaitingTime = 88 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.950f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.517f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.167f, WaitingTime = 48 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 86 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.200f, WaitingTime = 60 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 90 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 50 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.817f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 63 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.300f, WaitingTime = 72 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.667f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.750f, WaitingTime = 75 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 51 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.900f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.483f, WaitingTime = 62 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.367f, WaitingTime = 88 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.100f, WaitingTime = 49 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.050f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 47 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.783f, WaitingTime = 52 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.850f, WaitingTime = 86 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.683f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.733f, WaitingTime = 75 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.300f, WaitingTime = 59 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.900f, WaitingTime = 89 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.700f, WaitingTime = 59 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.633f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.317f, WaitingTime = 50 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 85 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.817f, WaitingTime = 59 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 87 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.617f, WaitingTime = 53 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.067f, WaitingTime = 69 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.967f, WaitingTime = 56 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 88 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.767f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.917f, WaitingTime = 45 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.267f, WaitingTime = 55 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.650f, WaitingTime = 90 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 45 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.800f, WaitingTime = 56 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 89 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 46 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.383f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 51 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.933f, WaitingTime = 86 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.033f, WaitingTime = 53 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.733f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.233f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.233f, WaitingTime = 60 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.817f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.983f, WaitingTime = 59 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.633f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.017f, WaitingTime = 49 });
            DataPoints.Add(new ScatterModel() { Eruptions = 5.100f, WaitingTime = 96 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 53 });
            DataPoints.Add(new ScatterModel() { Eruptions = 5.033f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.400f, WaitingTime = 65 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.567f, WaitingTime = 71 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 70 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 93 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.800f, WaitingTime = 53 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.967f, WaitingTime = 89 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.200f, WaitingTime = 45 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 86 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 58 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.500f, WaitingTime = 66 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.367f, WaitingTime = 63 });
            DataPoints.Add(new ScatterModel() { Eruptions = 5.000f, WaitingTime = 88 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.933f, WaitingTime = 52 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.617f, WaitingTime = 93 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.917f, WaitingTime = 49 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.083f, WaitingTime = 57 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.333f, WaitingTime = 68 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.333f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 73 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.417f, WaitingTime = 50 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 85 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.167f, WaitingTime = 74 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 55 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.767f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.033f, WaitingTime = 51 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.433f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 46 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.183f, WaitingTime = 55 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.833f, WaitingTime = 57 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.100f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.966f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.233f, WaitingTime = 81 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.500f, WaitingTime = 87 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.366f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 51 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.667f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.100f, WaitingTime = 60 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.133f, WaitingTime = 91 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 53 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.600f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.783f, WaitingTime = 46 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.367f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.850f, WaitingTime = 84 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.933f, WaitingTime = 49 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.383f, WaitingTime = 71 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.700f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 49 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 75 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.417f, WaitingTime = 64 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.233f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.400f, WaitingTime = 53 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.800f, WaitingTime = 94 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 55 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 76 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.867f, WaitingTime = 50 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.267f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.750f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.483f, WaitingTime = 75 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.000f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.117f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.267f, WaitingTime = 78 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.917f, WaitingTime = 70 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.550f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.083f, WaitingTime = 70 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.417f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.183f, WaitingTime = 86 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.217f, WaitingTime = 50 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 90 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.883f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 1.850f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.283f, WaitingTime = 77 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.950f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.333f, WaitingTime = 64 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 75 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.350f, WaitingTime = 47 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.933f, WaitingTime = 86 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.900f, WaitingTime = 63 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.583f, WaitingTime = 85 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.833f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.083f, WaitingTime = 57 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.367f, WaitingTime = 82 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.133f, WaitingTime = 67 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.350f, WaitingTime = 74 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.200f, WaitingTime = 54 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.567f, WaitingTime = 73 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.500f, WaitingTime = 73 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.150f, WaitingTime = 88 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.817f, WaitingTime = 80 });
            DataPoints.Add(new ScatterModel() { Eruptions = 3.917f, WaitingTime = 71 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.450f, WaitingTime = 83 });
            DataPoints.Add(new ScatterModel() { Eruptions = 2.000f, WaitingTime = 56 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.283f, WaitingTime = 79 });
            DataPoints.Add(new ScatterModel() { Eruptions = 4.767f, WaitingTime = 78 });
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                DataPoints.Add(new ScatterModel() { Eruptions = 4.533f, WaitingTime = 84 });
                DataPoints.Add(new ScatterModel() { Eruptions = 1.850f, WaitingTime = 58 });
                DataPoints.Add(new ScatterModel() { Eruptions = 4.250f, WaitingTime = 83 });
                DataPoints.Add(new ScatterModel() { Eruptions = 1.983f, WaitingTime = 43 });
                DataPoints.Add(new ScatterModel() { Eruptions = 2.250f, WaitingTime = 60 });
                DataPoints.Add(new ScatterModel() { Eruptions = 4.750f, WaitingTime = 75 });
                DataPoints.Add(new ScatterModel() { Eruptions = 4.117f, WaitingTime = 81 });
                DataPoints.Add(new ScatterModel() { Eruptions = 2.150f, WaitingTime = 46 });
                DataPoints.Add(new ScatterModel() { Eruptions = 4.417f, WaitingTime = 90 });
                DataPoints.Add(new ScatterModel() { Eruptions = 1.817f, WaitingTime = 46 });
                DataPoints.Add(new ScatterModel() { Eruptions = 4.467f, WaitingTime = 74 });
            }
        }
        public ObservableCollection<ScatterModel> DataPoints
        {
            get;
            set;
        }


        public void Dispose()
        {
            if (this.DataPoints != null)
                this.DataPoints.Clear();
        }
    }

    public class Power
    {
        public DateTime Year
        {
            get;
            set;
        }

        public double IND
        {
            get;
            set;
        }

        public double GER
        {
            get;
            set;
        }

        public double UK
        {
            get;
            set;
        }

        public double FRA
        {
            get;
            set;
        }

        public double AUS
        {
            get;
            set;
        }

    }

    public class ScatterModel
    {
        public float Eruptions
        {
            get;
            set;
        }
        public int WaitingTime
        {
            get;
            set;
        }
    }

    public class WaterfallModel
    {
        public double Value { get; set; }

        public string Category { get; set; }

        public bool IsSummary { get; set; }
    }

    public class WaterfallViewModelMobile
    {
        public WaterfallViewModelMobile()
        {
            this.RevenueDetails = new ObservableCollection<WaterfallModel>();

            RevenueDetails.Add(new WaterfallModel() { Category = "Income", Value = 47 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Development", Value = -6 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Other revenue", Value = 10 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Intermediate sum", Value = 0, IsSummary = true });
            RevenueDetails.Add(new WaterfallModel() { Category = "Income tax", Value = -5 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Net profit", Value = 0, IsSummary = true });

            WaterfallAdornment = new ResourceFactory();
            AdornmentsInfo = new ChartAdornmentInfo()
            {
                LabelTemplate = WaterfallAdornment.labelTemplate24,
                AdornmentsPosition = AdornmentsPosition.TopAndBottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                ShowLabel = true,
                ShowMarker = true
            };
        }

        public ObservableCollection<WaterfallModel> RevenueDetails { get; set; }

        public ResourceFactory WaterfallAdornment { get; set; }

        public ChartAdornmentInfo AdornmentsInfo { get; set; }

        public void Dispose()
        {
            if (this.RevenueDetails != null)
                this.RevenueDetails.Clear();
        }
    }

    public class WaterfallViewModel
    {
        public WaterfallViewModel()
        {
            this.RevenueDetails = new ObservableCollection<WaterfallModel>();

            RevenueDetails.Add(new WaterfallModel() { Category = "Income", Value = 4711 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Marketing and sales", Value = -427 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Research", Value = -588 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Development", Value = -688 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Other revenue", Value = 1030 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Intermediate sum", Value = 4711, IsSummary = true });
            RevenueDetails.Add(new WaterfallModel() { Category = "Administrative", Value = -780 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Other expense", Value = -361 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Income tax", Value = -695 });
            RevenueDetails.Add(new WaterfallModel() { Category = "Net profit", Value = -695, IsSummary = true });
        }

        public ObservableCollection<WaterfallModel> RevenueDetails { get; set; }

        public void Dispose()
        {
            if (this.RevenueDetails != null)
                this.RevenueDetails.Clear();
        }
    }

    public class HistogramViewModel
    {
        public HistogramViewModel()
        {
            this.Product = new ObservableCollection<HistogramModel>();

            Product.Add(new HistogramModel() { Price = 3 });
            Product.Add(new HistogramModel() { Price = 4 });
            Product.Add(new HistogramModel() { Price = 6 });
            Product.Add(new HistogramModel() { Price = 7 });
            Product.Add(new HistogramModel() { Price = 8 });
            Product.Add(new HistogramModel() { Price = 11 });
            Product.Add(new HistogramModel() { Price = 13 });
            Product.Add(new HistogramModel() { Price = 12 });
            Product.Add(new HistogramModel() { Price = 14 });
            Product.Add(new HistogramModel() { Price = 17 });
            Product.Add(new HistogramModel() { Price = 18 });
            Product.Add(new HistogramModel() { Price = 19 });
            Product.Add(new HistogramModel() { Price = 16 });
            Product.Add(new HistogramModel() { Price = 20 });
            Product.Add(new HistogramModel() { Price = 21 });
            Product.Add(new HistogramModel() { Price = 22 });
            Product.Add(new HistogramModel() { Price = 23 });
            Product.Add(new HistogramModel() { Price = 23 });
            Product.Add(new HistogramModel() { Price = 22 });
            Product.Add(new HistogramModel() { Price = 23 });
            Product.Add(new HistogramModel() { Price = 26 });
            Product.Add(new HistogramModel() { Price = 28 });
            Product.Add(new HistogramModel() { Price = 26 });
            Product.Add(new HistogramModel() { Price = 26 });
            Product.Add(new HistogramModel() { Price = 27 });
            Product.Add(new HistogramModel() { Price = 31 });
            Product.Add(new HistogramModel() { Price = 33 });
            Product.Add(new HistogramModel() { Price = 31 });
            Product.Add(new HistogramModel() { Price = 33 });
            Product.Add(new HistogramModel() { Price = 38 });
            Product.Add(new HistogramModel() { Price = 37 });
            Product.Add(new HistogramModel() { Price = 39 });
            Product.Add(new HistogramModel() { Price = 42 });
            Product.Add(new HistogramModel() { Price = 44 });

            AdornmentFactory = new ResourceFactory();
            AdornmentInfo4 = new ChartAdornmentInfo();
            AdornmentInfo4.AdornmentsPosition = AdornmentsPosition.Top;
            AdornmentInfo4.ShowLabel = true;
            AdornmentInfo4.LabelTemplate = AdornmentFactory.labelTemplate7;
            AdornmentInfo4.HorizontalAlignment = global::Windows.UI.Xaml.HorizontalAlignment.Center;
            AdornmentInfo4.VerticalAlignment = global::Windows.UI.Xaml.VerticalAlignment.Center;
        }
        public ResourceFactory AdornmentFactory { get; set; }

        public ChartAdornmentInfo AdornmentInfo4 { get; set; }

        public ObservableCollection<HistogramModel> Product { get; set; }

        public void Dispose()
        {
            if (this.Product != null)
                this.Product.Clear();
        }
    }

    public class HistogramModel
    {
        public double Price { get; set; }

        public double Value { get; set; }

    }
    
    public class BoxWhiskerViewModel
    {
        public BoxWhiskerViewModel()
        {
            BoxWhiskerData = new ObservableCollection<BoxWhiskerModel>();
            DateTime date = new DateTime(2017,1,1);

            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "Development", Age = new List<double> { 22, 22, 23, 25, 25, 25, 26, 27, 27, 28, 28, 29, 30, 32, 34, 32, 34, 36, 35, 38 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "Testing", Age = new List<double> { 22, 33, 23, 25, 26, 28, 29, 30, 34, 33, 32, 31, 50 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "HR", Age = new List<double> { 22, 24, 25, 30, 32, 34, 36, 38, 39, 41, 35, 36, 40, 56 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "Finance", Age = new List<double> { 26, 27, 28, 30, 32, 34, 35, 37, 35, 37, 45 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "R&D", Age = new List<double> { 26, 27, 29, 32, 34, 35, 36, 37, 38, 39, 41, 43, 58 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "Sales", Age = new List<double> { 27, 26, 28, 29, 29, 29, 32, 35, 32, 38, 53 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "Inventry", Age = new List<double> { 21, 23, 24, 25, 26, 27, 28, 30, 34, 36, 38 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "Graphics", Age = new List<double> { 26, 28, 29, 30, 32, 33, 35, 36, 52 } });
            BoxWhiskerData.Add(new BoxWhiskerModel() { Department = "Training", Age = new List<double> { 28, 29, 30, 31, 32, 34, 35, 36 } });
        }
        public ObservableCollection<BoxWhiskerModel> BoxWhiskerData {get; set;}

        public void Dispose()
        {
            if (this.BoxWhiskerData != null)
                this.BoxWhiskerData.Clear();
        }
    }   

    public class BoxWhiskerModel
    {
        public string Department { get; set; }

        public List<double> Age { get; set; }
    }

    public class LoadDetail
    {
        public DateTime Date { get; set; }
        public double Load { get; set; }
    }

    public class LoadDetailViewModel
    {
        public LoadDetailViewModel()
        {
            GenerateData();
        }

        private void GenerateData()
        {
            LoadDetails = new ObservableCollection<LoadDetail>();
            var randam = new Random();
            var value = 70d;
            var date = new DateTime(2015, 4, 1);

            for (int i = 1; i < 1000; i++)
            {
                if (randam.NextDouble() > 0.5)
                {
                    value += randam.NextDouble();
                }
                else
                {
                    value -= randam.NextDouble();
                }

                var commidity = new LoadDetail() { Load = value, Date = date.AddDays(i) };
                LoadDetails.Add(commidity);
            }
        }

        public void Dispose()
        {
            if (this.LoadDetails != null)
                this.LoadDetails.Clear();
        }

        public ObservableCollection<LoadDetail> LoadDetails { get; set; }
    }

    public class WheatProductionViewModel
    {
        public ChartAdornmentInfo SplineRangeAreaAdornmentInfo { get; set; }

        public ResourceFactory AdornmentFactory { get; set; }


        public WheatProductionViewModel()
        {
            this.Products = new ObservableCollection<WheatProductionModel>();

            Products.Add(new WheatProductionModel() { Month = "Jan", LowMetric = 20, HighMetric = 53 });
            Products.Add(new WheatProductionModel() { Month = "Feb", LowMetric = 22, HighMetric = 76 });
            Products.Add(new WheatProductionModel() { Month = "Mar", LowMetric = 30, HighMetric = 80 });
            Products.Add(new WheatProductionModel() { Month = "Apr", LowMetric = 26, HighMetric = 50 });
            Products.Add(new WheatProductionModel() { Month = "May", LowMetric = 36, HighMetric = 68 });
            Products.Add(new WheatProductionModel() { Month = "Jun", LowMetric = 20, HighMetric = 90 });

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                Products.Add(new WheatProductionModel() { Month = "Jul", LowMetric = 40, HighMetric = 73 });
                Products.Add(new WheatProductionModel() { Month = "Aug", LowMetric = 22, HighMetric = 76 });
                Products.Add(new WheatProductionModel() { Month = "Sep", LowMetric = 30, HighMetric = 80 });
                Products.Add(new WheatProductionModel() { Month = "Oct", LowMetric = 26, HighMetric = 50 });
                Products.Add(new WheatProductionModel() { Month = "Nov", LowMetric = 36, HighMetric = 68 });
                Products.Add(new WheatProductionModel() { Month = "Dec", LowMetric = 20, HighMetric = 90 });
            }
            else
            {
                SplineRangeAreaAdornmentInfo = new ChartAdornmentInfo()
                {
                    AdornmentsPosition = AdornmentsPosition.TopAndBottom,                    
                    Symbol = ChartSymbol.Ellipse,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    ShowLabel = true,
                };

                AdornmentFactory = new ResourceFactory();
                SplineRangeAreaAdornmentInfo.LabelTemplate = AdornmentFactory.splineRangeAreaAdornment;
            }
        }

        public ObservableCollection<WheatProductionModel> Products { get; set; }
    }

    public class WheatProductionModel
    {
        public string Month { get; set; }

        public double LowMetric { get; set; }

        public double HighMetric { get; set; }
    }
}
