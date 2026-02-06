#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
extern alias syncfusion;

using syncfusion::Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP10449 : Page
    {
        public UWP10449()
        {
            this.InitializeComponent();
        }
        PdfLoadedDocument document = new PdfLoadedDocument();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.GIS Succinctly.pdf");
            document = new PdfLoadedDocument(fileStream);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var imageStream = await RenderPage(document, 1);
            var ms = stopwatch.ElapsedMilliseconds;
            if (imageStream == null || imageStream?.Length == 0)
            {
                MessageDialog message = new MessageDialog("Export as image is not working");                
                await message.ShowAsync();
            }
            if (ms > 500)
            {
                MessageDialog message = new MessageDialog("Export as image takes more time");
            }
        }

        internal static async Task<Stream> RenderPage(PdfLoadedDocument doc, int page)
        {
            if ((doc == null) || (page < 0))
                return null;

            SfPdfViewerControl sfPdfViewer = new SfPdfViewerControl();

            try
            {
                sfPdfViewer.LoadDocument(doc);
                return await sfPdfViewer.ExportAsImage(page);
            }
            catch (Exception e)
            {
                // s_log.ErrorFormat("Cannot render page {0} due to {1}", page, e.Message);
                return null;
            }
        }
    }
}