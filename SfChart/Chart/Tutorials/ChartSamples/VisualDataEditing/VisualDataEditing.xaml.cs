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
    public sealed partial class VisualDataEditing : SampleLayout
    {
        public VisualDataEditing()
        {
            this.InitializeComponent();

            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.visualData.Series[0].ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
                this.visualData.Series[1].ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            }
        }

        private void XySegmentDraggingBase_OnDragDelta(object sender, DragDelta e)
        {
            var info = e as XySegmentDragEventArgs;
            if (info == null) return;
            info.Cancel = info.NewYValue < 1;
        }

        private void XySegmentDraggingBase_OnDragStart(object sender, ChartDragStartEventArgs e)
        {
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                e.Cancel = !(ReferenceEquals(e.BaseXValue, "2009") || ReferenceEquals(e.BaseXValue, "2010") ||
                       ReferenceEquals(e.BaseXValue, "2011") || ReferenceEquals(e.BaseXValue, "2012"));
            }
            else
            {
                e.Cancel = !(ReferenceEquals(e.BaseXValue, "2011") || ReferenceEquals(e.BaseXValue, "2012"));
            }
        }

        private void XySegmentDraggingBase_OnSegmentMouseOver(object sender, XySegmentEnterEventArgs e)
        {
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
                e.CanDrag = (ReferenceEquals(e.XValue, "2009") || ReferenceEquals(e.XValue, "2010") ||
                         ReferenceEquals(e.XValue, "2011") || ReferenceEquals(e.XValue, "2012"));
            else
                e.CanDrag = (ReferenceEquals(e.XValue, "2011") || ReferenceEquals(e.XValue, "2012"));
        }

        public override void Dispose()
        {
            if ((this.MainGrid.DataContext as SalesAnalysisViewModel) != null)
                (this.MainGrid.DataContext as SalesAnalysisViewModel).Dispose();

            if (this.visualData != null)
            {
                foreach (var series in this.visualData.Series)
                    series.ClearValue(ChartSeriesBase.ItemsSourceProperty);
                this.visualData = null;
            }

            this.MainGrid.Resources = null;

            base.Dispose();
        }

        private void visualData_Loaded(object sender, global::Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTemplate labelTemplate = MainGrid.Resources["adornmentTemplate"] as DataTemplate;
            ChartAdornmentInfo cai = new ChartAdornmentInfo();
            ChartAdornmentInfo cai1 = new ChartAdornmentInfo();

            cai.ShowLabel = true;
            cai.SegmentLabelContent = LabelContent.LabelContentPath;
            cai.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            cai.LabelTemplate = labelTemplate;
            cai1.Symbol = ChartSymbol.Ellipse;

            columnSeries.AdornmentsInfo = cai;
            lineSeries.AdornmentsInfo = cai1;
        }
    }

    public class SalesAnalysisViewModel : IDisposable
    {
        public ObservableCollection<SalesAnalysisModel> SalesData
        {
            get;
            set;
        }

        public SalesAnalysisViewModel()
        {
            ResourceFac = new ResourceFactory();
            AdornmentInfo = new ChartAdornmentInfo();
            AdornmentInfo1 = new ChartAdornmentInfo();

            AdornmentInfo.AdornmentsPosition = AdornmentsPosition.TopAndBottom;
            AdornmentInfo.SegmentLabelContent = LabelContent.LabelContentPath;
            AdornmentInfo.ShowLabel = true;
            AdornmentInfo.LabelTemplate = ResourceFac.labelTemplate117;

            AdornmentInfo1.Symbol = ChartSymbol.Ellipse;
            AdornmentInfo1.SymbolHeight = 20;
            AdornmentInfo1.SymbolWidth = 20;

            SalesData = GetRandomData();

        }
        public ResourceFactory ResourceFac { get; set; }
        public ChartAdornmentInfo AdornmentInfo { get; set; }
        public ChartAdornmentInfo AdornmentInfo1 { get; set; }

        private ObservableCollection<SalesAnalysisModel> GetRandomData()
        {
            ObservableCollection<SalesAnalysisModel> datas = new ObservableCollection<SalesAnalysisModel>();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                datas.Add(new SalesAnalysisModel("2005", 50, 6184, ""));
                datas.Add(new SalesAnalysisModel("2006", 56, 6384, ""));
                datas.Add(new SalesAnalysisModel("2007", 59, 6765, ""));
                datas.Add(new SalesAnalysisModel("2008", 63, 7343, ""));
                datas.Add(new SalesAnalysisModel("2009", 74, 8266, "Predicted No. Of Customers"));
                datas.Add(new SalesAnalysisModel("2010", 77, 8623, "Predicted No. Of Customers"));
                datas.Add(new SalesAnalysisModel("2011", 80, 8723, "Predicted No. Of Customers"));
                datas.Add(new SalesAnalysisModel("2012", 85, 8823, "Predicted No. Of Customers"));
            }
            else
            {
                datas.Add(new SalesAnalysisModel("2008", 63, 7343, ""));
                datas.Add(new SalesAnalysisModel("2009", 74, 8266, ""));
                datas.Add(new SalesAnalysisModel("2010", 77, 8623, ""));
                datas.Add(new SalesAnalysisModel("2011", 80, 8723, "Predicted No. Of Customers"));
                datas.Add(new SalesAnalysisModel("2012", 85, 8823, "Predicted No. Of Customers"));
            }
            return datas;
        }
        public void Dispose()
        {
            if (this.SalesData != null)
                this.SalesData.Clear();
        }
    }

    public class SalesAnalysisModel : INotifyPropertyChanged
    {

        private string text;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        private double income;

        public double Income
        {
            get
            {
                return income;
            }
            set
            {
                income = value;
            }
        }

        private double noOfCustomer;

        public double NoOfCustomer
        {
            get
            {
                return noOfCustomer;
            }
            set
            {
                noOfCustomer = value;
            }
        }

        private string year;

        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }

        public SalesAnalysisModel(string year, double count, double income, string text)
        {
            Year = year;
            NoOfCustomer = count;
            Income = income;
            Text = text;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Math.Round((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
