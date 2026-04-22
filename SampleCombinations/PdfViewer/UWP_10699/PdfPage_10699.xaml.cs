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

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfPage_10699 : Page
    {
        public PdfPage_10699()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UWP_10699));
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(PdfPage_10699).GetTypeInfo().Assembly;
            Stream docStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.40_file_2020.pdf");
            pdfViewer.LoadDocument(docStream);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            pdfViewer.Print();
        }

        protected override void OnDisconnectVisualChildren()
        {
            pdfViewer.Unload(true);
            base.OnDisconnectVisualChildren();
        }
    }
}
