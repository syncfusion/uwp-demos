#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
extern alias syncfusion;

using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10857 : Page
    {
        public UWP_10857()
        {
            this.InitializeComponent();
            Assembly assembly = typeof(UWP_10820).GetTypeInfo().Assembly;
            var docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.GIS Succinctly.pdf");
            pdfViewer.LoadDocument(docStream);
        }

        private void AddStamp_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = new Grid();
            grid.Width = 100;
            grid.Height = 100;
            grid.Background = new SolidColorBrush(Colors.Red);
            pdfViewer.AddStamp(grid, 1);
        }

        private async void SaveReload_Click(object sender, RoutedEventArgs e)
        {
            var stream = await pdfViewer.SaveAsync();
            pdfViewer.LoadDocument(stream);
        }
    }
}
