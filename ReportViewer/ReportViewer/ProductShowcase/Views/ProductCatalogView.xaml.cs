using Common;
using Syncfusion.UI.Xaml.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.ReportViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductCatalogView : SampleLayout, IDisposable
    {
        ReportViewerSampleHelper SampleView
        {
            get;
            set;
        }

        public ProductCatalogView()
        {
            this.InitializeComponent();
            this.SampleView = new ProductCatalogDemo(this.reportViewer);
            this.Loaded += ReportParametersDemo_Loaded;
        }

        async void ReportParametersDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Common.DeviceFamily.GetDeviceFamily() != Common.Devices.Desktop)
            {
                grd_controlPanel.Margin = new Thickness(0, 0, 0, 20);
            }

            this.reportViewer.ReportLoaded += reportViewer_ReportLoaded;
            this.reportViewer.ViewButtonClick += reportViewer_ViewButtonClick;

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
            {
                if (this.SampleView != null)
                {
                    this.SampleView.LoadReport();
                }
            }));

        }

        void reportViewer_ViewButtonClick(object sender, CancelEventArgs args)
        {
            this.SampleView.UpdateDataSet();
        }

        void reportViewer_ReportLoaded(object sender, EventArgs e)
        {
            this.SampleView.SetParameter();
            this.SampleView.UpdateDataSet();
        }

        public override void Dispose()
        {
            SampleView = null;
            this.reportViewer.ReportLoaded -= reportViewer_ReportLoaded;
            this.reportViewer.ViewButtonClick -= reportViewer_ViewButtonClick;
            this.Loaded -= ReportParametersDemo_Loaded;

            if (this.reportViewer.DataSources != null)
            {
                foreach (var dataDataSource in this.reportViewer.DataSources)
                {
                    IList list = dataDataSource.Value as IList;

                    if (list != null)
                    {
                        list.Clear();
                    }
                }
                this.reportViewer.DataSources.Clear();
            }

            this.reportViewer.Reset();
            this.reportViewer.Dispose();
        }
    }
}
