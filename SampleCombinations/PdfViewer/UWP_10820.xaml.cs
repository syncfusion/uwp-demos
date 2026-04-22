#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
extern alias syncfusion;

using syncfusion::Syncfusion.Pdf.Interactive;
using Syncfusion.Windows.PdfViewer;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10820 : Page
    {
        PdfBookmarkBase bookmarks = null;
        public UWP_10820()
        {
            this.InitializeComponent();
            pdfViewer.AnnotationAdded += PdfViewer_AnnotationAdded;
        }

        IAnnotation addedStampAnnot = null;
        private void PdfViewer_AnnotationAdded(object sender, Syncfusion.Windows.PdfViewer.AnnotationAddedEventArgs e)
        {
            addedStampAnnot = e.Annotation;
        }

        private async void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(UWP_10820).GetTypeInfo().Assembly;
            var docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.GIS Succinctly.pdf");
            pdfViewer.LoadDocument(docStream);

        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.SinglePageViewCommand.Execute(true);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AddStamp(object sender, RoutedEventArgs e)
        {
            Grid grid = new Grid();
            grid.Width = 100;
            grid.Height = 100;
            grid.Background = new SolidColorBrush(Colors.Red);
            pdfViewer.AddStamp(grid, 1);
        }

        private void searchBtn_Click_1(object sender, RoutedEventArgs e)
        {
            pdfViewer.RemoveAnnotation(addedStampAnnot);
        }
    }
}
