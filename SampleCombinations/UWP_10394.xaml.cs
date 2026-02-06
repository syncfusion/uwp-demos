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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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
    public sealed partial class UWP_10394 : Page
    {
        public UWP_10394()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.GIS SuccinctlyTextMarkup.pdf");
            pdfViewer.LoadDocument(docStream);
            pdfViewer.AnnotationSelected += PdfViewer_AnnotationSelected;
        }

        private void PdfViewer_AnnotationSelected(object sender, AnnotationSelectedEventArgs e)
        {
           if(e.Annotation is TextMarkupAnnotation)
            {
                pdfViewer.RemoveAnnotation(e.Annotation);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.CreateFileAsync("sample.fdf", CreationCollisionOption.ReplaceExisting);
            MemoryStream memoryStream = new MemoryStream();
            pdfViewer.LoadedDocument.ExportAnnotations(memoryStream, Pdf.Parsing.AnnotationDataFormat.Fdf);
            if(memoryStream != null)
            {
                IRandomAccessStream fileStream = await sampleFile.OpenAsync(FileAccessMode.ReadWrite);
                Stream st = fileStream.AsStreamForWrite();
                st.SetLength(0);
                st.Write((memoryStream as MemoryStream).ToArray(), 0, (int)memoryStream.Length);
                st.Flush();
                st.Dispose();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.GIS SuccinctlyTextMarkupAnnotation.pdf");
            pdfViewer.LoadDocument(docStream);

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            var pdfAnnotationFile = await storageFolder.GetFileAsync("sample.fdf");
            var stream = await pdfAnnotationFile.OpenStreamForReadAsync();
            if (stream.Length == 0)
                return;
            var loadedDocument = pdfViewer.LoadedDocument;
            loadedDocument.ImportAnnotations(stream, Pdf.Parsing.AnnotationDataFormat.Fdf);
            var st = new MemoryStream();
            await loadedDocument.SaveAsync(st);
            loadedDocument.Close(true);
            Pdf.Parsing.PdfLoadedDocument lDocument = new Pdf.Parsing.PdfLoadedDocument(st);
            pdfViewer.LoadDocument(lDocument);
        }      
    }
}
