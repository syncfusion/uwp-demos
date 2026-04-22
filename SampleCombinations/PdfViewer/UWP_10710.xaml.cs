#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class UWP_10710 : Page
    {
        public UWP_10710()
        {
            this.InitializeComponent();
        }
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Assembly assembly = typeof(UWP_10710).GetTypeInfo().Assembly;
                Stream fileStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.Windows Store Apps Succinctly.pdf");
                await pdfViewer.LoadDocumentAsync(fileStream);
            }
            catch (Exception ex)
            {

            }
        }

        private void pdfViewerControl_HyperlinkPointerPressed(object sender, Syncfusion.Windows.PdfViewer.HyperlinkEventArgs e)
        {
            //args.Handled = false;
            try
            {
                SfPdfViewerControl currentpdfViewerControl = (SfPdfViewerControl)sender;
                // Navigates to the hyperlink page in the PDF document.
                GotoPDFPage(currentpdfViewerControl, e, "PDFFile");
                //args.Handled = true;
                Task.Delay(100);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                string exceptiontext = ex.Message;
            }
            //args.Handled = true;
        }

        public void GotoPDFPage(SfPdfViewerControl currentpdfViewerControl, HyperlinkEventArgs args, String fileLoadedName)
        {
            try
            {
                // Gets or sets a value indicating whether the hyperlink navigation was handled.
                //args.Handled = false;
                // Gets the hyperlink being clicked.
                string uri = args.URI;
                // Gets the current page index of the hyperlink.
                int pageIndex = args.PageIndex;
                //Gets the destination page index of the hyperlink.
                int destinationPageIndex = args.DestinationPageIndex;
                if (destinationPageIndex != 0)
                {
                    destinationPageIndex = destinationPageIndex + 1;
                    if (fileLoadedName == "MTS ET.pdf" && destinationPageIndex > 183)
                    {
                        destinationPageIndex = 183;
                    }
                }
                //Gets the bounds of the hyperlink is being clicked.
                RectangleF hyperlinkBound = args.Bounds;
                //Gets the whether the tapped hyper link is a Web Link or Document Link.
                HyperlinkType linkType = args.HyperlinkType;
                // Goto the destibnation page
                //currentpdfViewerControl.GotoPage(destinationPageIndex);
                pdfViewer.GotoPage(destinationPageIndex);
                //args.Handled = true;
            }
            catch (Exception ex)
            {
                string exceptiontext = ex.Message;
            }
        }

        private void Unload_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.Unload(true);
        }
    }
}
