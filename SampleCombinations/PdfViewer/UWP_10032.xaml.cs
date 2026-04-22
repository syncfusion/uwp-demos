#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
extern alias syncfusion;

using syncfusion::Syncfusion.Pdf.Graphics;
using syncfusion::Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10032 : Page
    {
        public UWP_10032()
        {
            this.InitializeComponent();
        }
        private void SampleGrid_Loaded(object sender, RoutedEventArgs e)
        {
            pdfViewer.ItemsSource = CreateDocument();
        }

        private MemoryStream CreateDocument()
        {
            //Create a new PDF document.

            PdfDocument document = new PdfDocument();

            //Add a page to the document.

            PdfPage page = document.Pages.Add();

            //Create the font.

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12f);

            //Create the Text Web Link.

            PdfTextWebLink textLink = new PdfTextWebLink();

            //Set the hyperlink

            textLink.Url = "http://www.syncfusion.com";

            //Set the link text

            textLink.Text = ConstructExtensionLinkText();

            //Set the font

            textLink.Font = font;

            //Draw the hyperlink in PDF page

            textLink.DrawTextWebLink(page, new PointF(10, 40));

            var stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);
            return stream;
        }

        private string ConstructExtensionLinkText()
        {
            var dotsLine = new string('a', 50);

            var textWithExtraSpaces =
                $"{dotsLine}\n" +
                $"{dotsLine}\n" +
                $"{dotsLine}\n" +
                $"{dotsLine}\n" +
                $"{dotsLine}\n" +
                $"{dotsLine}";
            return textWithExtraSpaces;
        }
    }
}
