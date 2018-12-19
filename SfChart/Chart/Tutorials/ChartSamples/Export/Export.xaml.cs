#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Export : SampleLayout
    {
        public Export()
        {
            this.InitializeComponent();
            splineAdornment.Symbol = UI.Xaml.Charts.ChartSymbol.Custom;
            splineAdornment.ShowLabel = true;
            splineAdornment.ShowMarker = true;
            splineAdornment.SymbolTemplate = grid.Resources["adornShape"] as DataTemplate;
            splineAdornment.LabelTemplate = grid.Resources["adornment"] as DataTemplate;
                
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            exportChart.Save();
        }
    }
    public class ClimateDataViewModel
    {
        public ObservableCollection<DataModel> ClimateData { get; set; }

        public ClimateDataViewModel()
        {
            ClimateData = new ObservableCollection<DataModel>();
            ClimateData.Add(new DataModel() { Month = "Jan", Temperature = 33 });
            ClimateData.Add(new DataModel() { Month = "Feb", Temperature = 37 });
            ClimateData.Add(new DataModel() { Month = "Mar", Temperature = 39 });
            ClimateData.Add(new DataModel() { Month = "Apr", Temperature = 43 });
            ClimateData.Add(new DataModel() { Month = "May", Temperature = 45 });
            ClimateData.Add(new DataModel() { Month = "Jun", Temperature = 43 });
            ClimateData.Add(new DataModel() { Month = "Jul", Temperature = 41 });
            ClimateData.Add(new DataModel() { Month = "Aug", Temperature = 40 });
            ClimateData.Add(new DataModel() { Month = "Sep", Temperature = 39 });
            ClimateData.Add(new DataModel() { Month = "Oct", Temperature = 39 });
            ClimateData.Add(new DataModel() { Month = "Nov", Temperature = 34 });
            ClimateData.Add(new DataModel() { Month = "Dec", Temperature = 33 });
        }
    }

    public class DataModel
    {
        public string Month { get; set; }
        public double Temperature { get; set; }
    }
}
