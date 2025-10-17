#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
    public sealed partial class CrossHairDemo : SampleLayout
    {
        public CrossHairDemo()
        {
            Resources.MergedDictionaries.Add(SfChart.Resources.ColorModelResource.Resource);
            this.InitializeComponent();
        }

        public override void Dispose()
        {
            if ((this.DataContext as CurrencyData) != null)
                (this.DataContext as CurrencyData).Dispose();

            if (this.crossHair != null)
            {
                foreach (var series in this.crossHair.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.crossHair = null;
            }

            this.MainGrid.Resources = null;

            base.Dispose();
        }
    }

    public class CurrencyData : IDisposable
    {
        public CurrencyData()
        {
            CurrencyDetails = new ObservableCollection<CurrencyDetail>();

            Random rand = new Random();
            double value = 100;
            DateTime dateTime = new DateTime(1895, 1, 1);

            for (int i = 1; i < 600; i++)
            {
                if (rand.NextDouble() > 0.5)
                    value += rand.NextDouble();
                else
                    value -= rand.NextDouble();

                CurrencyDetails.Add(new CurrencyDetail() { Date = dateTime.AddMonths(i), CurrencyValue = value });
            }
        }
        public ObservableCollection<CurrencyDetail> CurrencyDetails { get; set; }

        public void Dispose()
        {
            if (this.CurrencyDetails != null)
                this.CurrencyDetails.Clear();
        }
    }

    public class CurrencyDetail
    {
        public DateTime Date { get; set; }
        public double CurrencyValue { get; set; }
    }
}
