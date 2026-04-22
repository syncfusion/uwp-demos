#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
extern alias syncfusion;

using syncfusion::Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10793 : Page
    {
        private PdfLoadedDocument pdfloadeddoc;
        public UWP_10793()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            Stream fileStream;
            picker.FileTypeFilter.Add(".pdf");
            //StorageFile file = await picker.PickSingleFileAsync();  // this does not help
            var file = await picker.PickSingleFileAsync();

            var stream = await file.OpenAsync(FileAccessMode.Read);
            fileStream = stream.AsStreamForRead();

            pdfloadeddoc = new PdfLoadedDocument(fileStream);

            await pdfViewer.LoadDocumentAsync(pdfloadeddoc);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PdfLoadedDocument pdfDoc = pdfViewer.LoadedDocument;

            pdfViewer.Unload(false);

            pdfViewer.LoadDocumentAsync(pdfDoc);
        }
    }
}
