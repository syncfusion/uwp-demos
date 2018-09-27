using Common;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.UI.Xaml.TreeGrid.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfExporting : SampleLayout, IDisposable
    {
        public PdfExporting()
        {
            this.InitializeComponent();
            this.treeGrid.CurrentCellRequestNavigate += TreeGrid_CurrentCellRequestNavigate;
        }
        private async void TreeGrid_CurrentCellRequestNavigate(object sender, UI.Xaml.Grid.CurrentCellRequestNavigateEventArgs args)
        {
            var URI = "http://en.wikipedia.org/wiki/" + args.NavigateText;
            await Launcher.LaunchUriAsync(new Uri(URI));
        }
        private async void OnExportToPdf(object sender, RoutedEventArgs e)
        {
            var options = new TreeGridPdfExportingOptions()
            {
                AutoRowHeight = (bool)autoRowHeight.IsChecked,
                AutoColumnWidth = (bool)autoColumnWidth.IsChecked,
                ExportFormat = (bool)exportFormat.IsChecked,
                CanExportHyperLink = (bool)exportLink.IsChecked,
                RepeatHeaders = (bool)repeatHeader.IsChecked,
                FitAllColumnsInOnePage = (bool)fitAllColumns.IsChecked               
            };

            if ((bool)customizeColumns.IsChecked)
                options.CellsExportingEventHandler = CellsExportingEventHandler;

            if ((bool)pageHeaderandFooter.IsChecked)
            {
                options.PageHeaderFooterEventHandler = PdfHeaderFooterEventHandler;
            }

            var pdfDocument = treeGrid.ExportToPdf(options);
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "Sample"
            };


            savePicker.FileTypeChoices.Add("Pdf Files(.pdf)", new List<string>() { ".pdf" });

            var storageFile = await savePicker.PickSaveFileAsync();

            if (storageFile != null)
            {
                await pdfDocument.SaveAsync(storageFile);

                options.CellsExportingEventHandler = null;
                options.PageHeaderFooterEventHandler = null;
                var msgDialog = new MessageDialog("Do you want to view the Document?", "File has been created successfully.");

                var yesCmd = new UICommand("Yes");
                var noCmd = new UICommand("No");
                msgDialog.Commands.Add(yesCmd);
                msgDialog.Commands.Add(noCmd);
                var cmd = await msgDialog.ShowAsync();
                if (cmd == yesCmd)
                {
                    // Launch the saved file
                    bool success = await Launcher.LaunchFileAsync(storageFile);
                }
            }
        }

        #region ExportToPdf Event Handlers
        static async void PdfHeaderFooterEventHandler(object sender, TreeGridPdfHeaderFooterEventArgs e)
        {
            var width = e.PdfPage.GetClientSize().Width;

            PdfPageTemplateElement header = new PdfPageTemplateElement(width, 38);
            e.PdfDocumentTemplate.Top = header;

            PdfPageTemplateElement footer = new PdfPageTemplateElement(width, 30);
            e.PdfDocumentTemplate.Bottom = footer;           
            var uri = new Uri("ms-appx:///Images/Header.png", UriKind.RelativeOrAbsolute);
            var srcfile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var stream = await srcfile.OpenStreamForReadAsync();
            header.Graphics.DrawImage(PdfImage.FromStream(stream), 0, 0, width / 3f, 34);

            uri = new Uri("ms-appx:///Images/Footer.png", UriKind.RelativeOrAbsolute);
            srcfile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            stream = await srcfile.OpenStreamForReadAsync();
            footer.Graphics.DrawImage(PdfImage.FromStream(stream), 0, 0, width, 25);
            stream.Dispose();
        }
        static void CellsExportingEventHandler(object sender, TreeGridCellPdfExportingEventArgs e)
        {
            if (e.CellType == TreeGridCellType.RecordCell && e.ColumnName == "Title")
            {
                var cellstyle = new PdfGridCellStyle();
                cellstyle.BackgroundBrush = PdfBrushes.LightGreen;
                e.PdfGridCell.Style = cellstyle;
            }
        }

        #endregion
        public sealed override void Dispose()
        {            
            this.Resources.Clear();
            this.treeGrid.CurrentCellRequestNavigate -= TreeGrid_CurrentCellRequestNavigate;
            this.treeGrid.ItemsSource = null;
            this.treeGrid.Dispose();
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
