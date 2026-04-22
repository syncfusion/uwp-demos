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
using System.IO;
using System.Reflection;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10860 : Page
    {
        Stream fileStream;
        StorageFile file;
        public UWP_10860()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(UWP_10820).GetTypeInfo().Assembly;
            var docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.GIS Succinctly.pdf");
            pdfViewer.LoadDocument(docStream);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Stream stream = pdfViewer.Save();
            //pdfViewer.LoadDocument(stream);

            using (var ms = new MemoryStream())
            {
                pdfViewer.LoadedDocument.ExportAnnotations(ms, AnnotationDataFormat.XFdf);
                //var bytes = ms.ToArray();
                ms.Seek(0, SeekOrigin.Begin);

                var stream = await file.OpenAsync(FileAccessMode.Read);
                var pdfLoadedDocument = new PdfLoadedDocument(stream.AsStreamForRead());
                pdfViewer.LoadDocument(pdfLoadedDocument);

                pdfViewer.Unload();
                stream = await file.OpenAsync(FileAccessMode.Read);
                pdfLoadedDocument = new PdfLoadedDocument(stream.AsStreamForRead());
                pdfLoadedDocument.ImportAnnotations(ms, AnnotationDataFormat.XFdf);
                MemoryStream memoryStream = new MemoryStream();
                pdfLoadedDocument.Save(memoryStream);
                memoryStream.Position = 0;
                pdfViewer.LoadDocument(memoryStream);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            pdfViewer.InkAnnotationCommand.Execute(true);
        }
    }
}
