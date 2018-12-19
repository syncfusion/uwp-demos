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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
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
    public sealed partial class ScatterDataEditing : SampleLayout
    {
        public ScatterDataEditing()
        {
            this.InitializeComponent();

            (this.scatterChart.PrimaryAxis as DateTimeAxis).Minimum = new DateTime(2015, 1, 1);
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                (this.scatterChart.PrimaryAxis as DateTimeAxis).Maximum = new DateTime(2015, 5, 1);
                this.scatterChart.Series[0].ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
                this.scatterChart.Series[1].ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            }
            else
                (this.scatterChart.PrimaryAxis as DateTimeAxis).Maximum = new DateTime(2015, 7, 1);
        }

        private void enableDraggingCheck_Operated(object sender, RoutedEventArgs e)
        {
            foreach (var series in this.scatterChart.Series)
                (series as XySegmentDraggingBase).EnableSegmentDragging = (bool)(sender as CheckBox).IsChecked;
        }

        private void dragDirectionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.scatterChart == null) return;
            int index = (sender as ComboBox).SelectedIndex;

           switch(index)
            {
                case 0:
                    foreach (ScatterSeries series in this.scatterChart.Series)
                        series.DragDirection = DragType.X;
                    break;

                case 1:
                    foreach (ScatterSeries series in this.scatterChart.Series)
                        series.DragDirection = DragType.Y;
                    break;

                default:
                    foreach (ScatterSeries series in this.scatterChart.Series)
                        series.DragDirection = DragType.XY;
                    break;
            }
        }       
    }
    
    public class SalePredictionModel
    {
        private DateTime month;

        public DateTime Month
        {
            get { return month; }
            set { month = value; }
        }


        private double saleCount;

        public double SaleCount
        {
            get { return saleCount; }
            set { saleCount = value; }
        }

        public SalePredictionModel(DateTime actualMonth, double actualSaleCount)
        {
            month = actualMonth;
            saleCount = actualSaleCount;
        }
    }

    public class SalePredictionViewModel
    {
        public SalePredictionViewModel()
        {
            var date = new DateTime(2015, 1, 1);

            Data1 = new ObservableCollection<SalePredictionModel>();

            Data1.Add(new SalePredictionModel(date.AddMonths(1), 698));
            Data1.Add(new SalePredictionModel(date.AddMonths(2), 903));
            Data1.Add(new SalePredictionModel(date.AddMonths(3), 807));

            Data2 = new ObservableCollection<SalePredictionModel>();

            Data2.Add(new SalePredictionModel(date.AddMonths(1), 948));
            Data2.Add(new SalePredictionModel(date.AddMonths(2), 1203));
            Data2.Add(new SalePredictionModel(date.AddMonths(3), 1107));

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                Data1.Add(new SalePredictionModel(date.AddMonths(4), 1058));
                Data1.Add(new SalePredictionModel(date.AddMonths(5), 954));

                Data2.Add(new SalePredictionModel(date.AddMonths(4), 1408));
                Data2.Add(new SalePredictionModel(date.AddMonths(5), 1154));
            }
        }

        public ObservableCollection<SalePredictionModel> Data1 { get; set; }
        public ObservableCollection<SalePredictionModel> Data2 { get; set; }
    }
}
