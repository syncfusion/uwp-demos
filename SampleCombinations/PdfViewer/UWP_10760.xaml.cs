#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

extern alias syncfusion;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.IO;
using System.Reflection;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10760 : Page
    {
        private IRandomAccessStream randomStream;
        public UWP_10760()
        {
            this.InitializeComponent();
            this.DataContext = pdfViewer;
            pdfViewer.AnnotationAdded += PdfViewer_AnnotationAdded;
            pdfViewer.AnnotationSelected += PdfViewer_AnnotationSelected;
            pdfViewer.AnnotationMovedOrResized += PdfViewer_AnnotationMovedOrResized;
            pdfViewer.AnnotationRemoved += PdfViewer_AnnotationRemoved;
            pdfViewer.PagePointerPressed += PdfViewer_PagePointerPressed;
            this.Loaded += MainPage_Loaded;
        }

        private void PdfViewer_PagePointerPressed(object sender, PagePointerEventArgs e)
        {

        }

        private void PdfViewer_AnnotationRemoved(object sender, AnnotationRemovedEventArgs e)
        {

        }

        private void PdfViewer_AnnotationMovedOrResized(object sender, AnnotationMovedOrResizedEventArgs e)
        {

        }

        private void PdfViewer_AnnotationSelected(object sender, AnnotationSelectedEventArgs e)
        {

        }

        private void PdfViewer_AnnotationAdded(object sender, AnnotationAddedEventArgs e)
        {

        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.7d8f581c-0edd-40a4-97b4-75d0cdb4bd60.pdf");
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
            pdfViewer.LoadDocument(ldoc);


        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            Image customImage = new Image();


            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                randomStream = await file.OpenAsync(FileAccessMode.Read);

            }
            if (randomStream != null)
            {
                StackPanel grid = new StackPanel();
                grid.Height = 100;
                grid.Width = 100;
                BitmapImage bitmap = new BitmapImage();
                bitmap.DecodePixelHeight = 200;
                bitmap.DecodePixelWidth = 200;
                await bitmap.SetSourceAsync(randomStream);
                customImage.Source = bitmap;
                if (customImage.Source != null)
                    grid.Children.Add(customImage);
                Point point = new Point()
                {
                    X = (float)(ParentGrid.ActualWidth / 2 - grid.Width / 2),
                    Y = (float)ParentGrid.ActualHeight / 2 - grid.Width / 2
                };
                pdfViewer.AddStamp(grid, pdfViewer.PageNumber, point);
            }
        }
    }
}
