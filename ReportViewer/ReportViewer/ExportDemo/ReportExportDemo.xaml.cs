//using SampleBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using System.Reflection;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Windows.Storage;
using Common;
using Syncfusion.UI.Xaml.Reports;
using Syncfusion.ReportWriter;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ReportWriterSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReportExportDemo : SampleLayout, IDisposable
    {
        public ReportExportDemo()
        {
            this.InitializeComponent();
            this.Loaded += ReportExportDemo_Loaded;
        }

        void ReportExportDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Common.DeviceFamily.GetDeviceFamily() != Common.Devices.Desktop)
            {
                grd_controlPanel.Margin = new Thickness(0, 0, 0, 20);
                pagePanel.Margin = new Thickness(20, 5, 0, 0);
            }
            //if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            //{
            //    //backButton.Visibility = Visibility.Collapsed;
            //    pagePanel.Margin = new Thickness(10, 10, 0, 0);
            //    title_bar.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            //}
        }

        async void Button_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker fileSavePicker = new FileSavePicker();
            WriterFormat format = WriterFormat.PDF;

            if (pdf.IsChecked == true)
            {
                fileSavePicker.FileTypeChoices.Add("PDF", new List<string> { ".pdf" });
                fileSavePicker.DefaultFileExtension = ".pdf";
                format = WriterFormat.PDF;
            }
            else if (excel.IsChecked == true)
            {
                fileSavePicker.FileTypeChoices.Add("Excel", new List<string> { ".xls" });
                fileSavePicker.DefaultFileExtension = ".xls";
                format = WriterFormat.Excel;
            }
            else if (word.IsChecked == true)
            {
                fileSavePicker.FileTypeChoices.Add("Word", new List<string> { ".doc" });
                fileSavePicker.DefaultFileExtension = ".doc";
                format = WriterFormat.Word;
            }
            else if (html.IsChecked == true)
            {
                fileSavePicker.FileTypeChoices.Add("Html", new List<string> { ".html" });
                fileSavePicker.DefaultFileExtension = ".html";
                format = WriterFormat.HTML;
            }

            fileSavePicker.SuggestedFileName = "ExportReport";
            var savedItem = await fileSavePicker.PickSaveFileAsync();

            if (savedItem != null)
            {
                MemoryStream exportFileStream = new MemoryStream();
                Assembly assembly = typeof(Syncfusion.SampleBrowser.UWP.ReportViewer.MainPage).GetTypeInfo().Assembly;
                Stream reportStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.ReportViewer.ReportViewer.ReportElement.Table Summaries.rdlc");

                ReportDataSourceCollection datas = new ReportDataSourceCollection();
                datas.Add(new ReportDataSource { Name = "Sales", Value = Syncfusion.SampleBrowser.UWP.ReportViewer.TableSummaries.SalesDetails.GetData() });

                ReportWriter writer = new ReportWriter(reportStream, datas);
                writer.ExportMode = Syncfusion.ReportWriter.ExportMode.Local;
                writer.ExportCompleted += Writer_ExportCompleted;
                await writer.SaveASync(exportFileStream, format);

                try
                {
                    using (IRandomAccessStream stream = await savedItem.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        // Write compressed data from memory to file
                        using (Stream outstream = stream.AsStreamForWrite())
                        {
                            byte[] buffer = exportFileStream.ToArray();
                            outstream.Write(buffer, 0, buffer.Length);
                            outstream.Flush();
                        }
                    }
                    exportFileStream.Dispose();
                }
                catch { }
            }
        }

        private void Writer_ExportCompleted(object sender, byte[] e)
        {
            MessageDialog msgDialog = new MessageDialog("Report exporting completed successfully");
            msgDialog.ShowAsync();
        }
        public override void Dispose()
        {
            this.Loaded -= ReportExportDemo_Loaded;
        }
    }
}
