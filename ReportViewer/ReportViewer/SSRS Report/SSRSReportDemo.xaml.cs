//using SampleBrowser;
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
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Common;
using Windows.Networking.Connectivity;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.ReportViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SSRSReportDemo : SampleLayout, IDisposable
    {
        ReportViewerSampleHelper SampleView
        {
            get;
            set;
        }

        string FolderPath
        {
            get;
            set;
        }

        public SSRSReportDemo()
        {
            this.InitializeComponent();
            this.Loaded += SSRSReportDemo_Loaded;
        }

        async void SSRSReportDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Common.DeviceFamily.GetDeviceFamily() != Common.Devices.Desktop)
            {
                grd_controlPanel.Margin = new Thickness(0, 0, 0, 20);
            }

            this.Loaded -= SSRSReportDemo_Loaded;
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
            {
                if (IsConnectedToInternet())
                {
                    this.reportViewer.ReportPath = @"/SSRSSamples/Territory Sales";
                    this.reportViewer.ReportServerUrl = @"http://104.207.134.201/reportserver";
                    this.reportViewer.ReportServiceURL = @"http://ssrs.syncfusion.com/Reporting/api/SSRSReport/";
                    this.reportViewer.ProcessingMode = ProcessingMode.Remote;
                    this.reportViewer.ReportServerCredential = new System.Net.NetworkCredential("ssrs", "RDLReport1");
                    this.reportViewer.RefreshReport();
                    this.loadMessage.Text = "Connecting to Report Server...";
                    this.loadMessage.Visibility = Visibility.Visible;
                    this.reportViewer.RenderingCompleted += ReportViewer_RenderingCompleted;
                    this.reportViewer.ReportLoaded += ReportViewer_ReportLoaded;
                }
                else
                {
                    var result = new MessageDialog("Internet connection is required to run this sample.");
                    Task.Run(async () => await result.ShowAsync());
                }
            }));
        }

        private void ReportViewer_ReportLoaded(object sender, EventArgs e)
        {
            List<DataSourceCredentials> crdentials = new List<DataSourceCredentials>();

            foreach (var dataSource in reportViewer.GetDataSources())
            {
                DataSourceCredentials credn = new DataSourceCredentials();
                credn.Name = dataSource.Name;
                credn.UserId = "ssrs1";
                credn.Password = "RDLReport1";
                crdentials.Add(credn);
            }

            this.reportViewer.SetDataSourceCredentials(crdentials);
        }

        private void ReportViewer_RenderingCompleted(object sender, EventArgs e)
        {
            this.loadMessage.Visibility = Visibility.Collapsed;
        }

        public static bool IsConnectedToInternet()
        {
            ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }

        void reportViewer_ViewButtonClick(object sender, CancelEventArgs args)
        {
            SampleView.UpdateDataSet();
        }

        public override void Dispose()
        {
            SampleView = null;
            this.reportViewer.ViewButtonClick -= reportViewer_ViewButtonClick;
            this.reportViewer.RenderingCompleted -= ReportViewer_RenderingCompleted;
            this.reportViewer.ReportLoaded -= ReportViewer_ReportLoaded;
            this.Loaded -= SSRSReportDemo_Loaded;

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
