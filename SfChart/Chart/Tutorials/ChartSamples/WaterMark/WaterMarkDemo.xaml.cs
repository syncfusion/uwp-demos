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
    public sealed partial class WaterMarkDemo : SampleLayout
    {
        double size = 30;
        public WaterMarkDemo()
        {
            SneakerDetails sneaker = new SneakerDetails();
            this.InitializeComponent();
            this.DataContext = sneaker.SneakersDetail;
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                lineSeries.SetValue(ChartTooltip.VerticalAlignmentProperty, VerticalAlignment.Top);
                lineSeries.SetValue(ChartTooltip.VerticalOffsetProperty, size);
            }
        }

        public override void Dispose()
        {
            if ((this.MainGrid.DataContext as SneakerDetails) != null)
                (this.MainGrid.DataContext as SneakerDetails).Dispose();

            if (this.waterMark != null)
            {
                foreach (var series in this.waterMark.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.waterMark = null;
            }

            this.MainGrid.Resources = null;

            base.Dispose();
        }

        private void waterMark_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTemplate labelTemplate1 = MainGrid.Resources["adornmentTemplate"] as DataTemplate;
            ChartAdornmentInfo chartAdornmentInfo1 = new ChartAdornmentInfo();

            chartAdornmentInfo1.Symbol = ChartSymbol.Ellipse;
            chartAdornmentInfo1.ShowLabel = true;
            chartAdornmentInfo1.LabelTemplate = labelTemplate1;

            lineSeries.AdornmentsInfo = chartAdornmentInfo1;
        }
    }

    public class SneakerDetails : IDisposable
    {

        public SneakerDetails()
        {
            this.SneakersDetail = new ObservableCollection<Sneaker>();

            SneakersDetail.Add(new Sneaker() { Brand = "Adidas", NoOfItems = 25 });
            SneakersDetail.Add(new Sneaker() { Brand = "Nike", NoOfItems = 17 });
            SneakersDetail.Add(new Sneaker() { Brand = "Reebok", NoOfItems = 34 });
            SneakersDetail.Add(new Sneaker() { Brand = "Fila", NoOfItems = 18 });
            SneakersDetail.Add(new Sneaker() { Brand = "Puma", NoOfItems = 10 });
            SneakersDetail.Add(new Sneaker() { Brand = "Fastrack", NoOfItems = 27 });
            SneakersDetail.Add(new Sneaker() { Brand = "Yepme", NoOfItems = 17 });
            SneakersDetail.Add(new Sneaker() { Brand = "Zovi", NoOfItems = 10 });
            SneakersDetail.Add(new Sneaker() { Brand = "Woodland", NoOfItems = 22 });

            ResourceFac = new ResourceFactory();
            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo.ShowLabel = true;
            AdornmentInfo.Symbol = ChartSymbol.Ellipse;
            AdornmentInfo.LabelTemplate = ResourceFac.labelTemplate19;
        }
        public ResourceFactory ResourceFac { get; set; }
        public ChartAdornmentInfo AdornmentInfo { get; set; }

        public ObservableCollection<Sneaker> SneakersDetail
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this.SneakersDetail != null)
                this.SneakersDetail.Clear();
        }
    }

    public class Sneaker
    {
        public double NoOfItems
        {
            get;
            set;
        }

        public string Brand
        {
            get;
            set;
        }
    }
}
