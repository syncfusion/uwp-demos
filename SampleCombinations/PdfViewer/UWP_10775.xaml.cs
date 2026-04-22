#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
extern alias syncfusion;

using syncfusion::Syncfusion.Pdf.Interactive;
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
    public sealed partial class UWP_10775 : Page
    {
        PdfBookmarkBase bookmarks = null;
        public UWP_10775()
        {
            this.InitializeComponent();
        }
        private async void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            Stream fileStream;
            picker.FileTypeFilter.Add(".pdf");
            var file = await picker.PickSingleFileAsync();

            var stream = await file.OpenAsync(FileAccessMode.Read, StorageOpenOptions.None);
            fileStream = stream.AsStreamForRead();

            PdfLoadedDocument pdfloadeddoc = new PdfLoadedDocument(fileStream);

            await pdfViewer.LoadDocumentAsync(pdfloadeddoc);

            bookmarks = pdfViewer.LoadedDocument.Bookmarks;
        }

        private void ContinuousPageViewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (pdfViewer.PageView != Syncfusion.Windows.PdfViewer.PdfViewerPageView.ContinuousPageView)
                pdfViewer.PageView = Syncfusion.Windows.PdfViewer.PdfViewerPageView.ContinuousPageView;
        }

        private void SinglePageViewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (pdfViewer.PageView != Syncfusion.Windows.PdfViewer.PdfViewerPageView.SinglePageView)
                pdfViewer.PageView = Syncfusion.Windows.PdfViewer.PdfViewerPageView.SinglePageView;


        }

        private void GobBtn_Click(object sender, RoutedEventArgs e)
        {
            PdfBookmark bk = bookmarks[9];

            pdfViewer.GoToBookmark(bk);
        }
    }
}
