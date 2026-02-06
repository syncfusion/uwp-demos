#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.IO;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    public sealed partial class UWP_10445_10458 : ContentDialog
    {
        public UWP_10445_10458()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.FreeText.pdf");
            pdfViewer.LoadDocument(docStream);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Stream saveStream = pdfViewer.Save();
            pdfViewer.LoadDocument(saveStream);
        }

    }
}
