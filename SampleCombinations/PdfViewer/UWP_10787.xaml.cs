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

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UWP_10787 : Page
    {
        public UWP_10787()
        {
            this.InitializeComponent();
        }
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.FX3S.FX3G.FX3GC.FX3U.FX3UC.pdf");

            PdfLoadedDocument pdfloadeddoc = new PdfLoadedDocument(docStream);
            await pdfViewer.LoadDocumentAsync(pdfloadeddoc);
        }

        private void AddRectAnnot_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.RectangleAnnotationCommand.Execute(true);
        }

        private void AddEllipse_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.EllipseAnnotationCommand.Execute(true);
        }

        private void FreeText_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.FreeTextAnnotationCommand.Execute(true);
        }

        private async void Save_Reload_Click(object sender, RoutedEventArgs e)
        {
            Stream strm = pdfViewer.Save();
            strm.Position = 0;
            await pdfViewer.LoadDocumentAsync(strm);
        }
    }
}
