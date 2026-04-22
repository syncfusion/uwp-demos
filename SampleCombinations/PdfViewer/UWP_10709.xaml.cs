#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class UWP_10709 : Page
    {
        public UWP_10709()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pdfViewer.PageView == Syncfusion.Windows.PdfViewer.PdfViewerPageView.SinglePageView)
                pdfViewer.PageView = Syncfusion.Windows.PdfViewer.PdfViewerPageView.ContinuousPageView;

        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            pdfViewer.LastPageCommand.Execute(true);
        }



        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(UWP_10709).GetTypeInfo().Assembly;
            Stream docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.Windows Store Apps Succinctly.pdf");
            pdfViewer.LoadDocument(docStream);
            pdfViewer.MinimumZoomPercentage = 20;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (pdfViewer.PageView == Syncfusion.Windows.PdfViewer.PdfViewerPageView.ContinuousPageView)
                pdfViewer.PageView = Syncfusion.Windows.PdfViewer.PdfViewerPageView.SinglePageView;
        }
    }
}
