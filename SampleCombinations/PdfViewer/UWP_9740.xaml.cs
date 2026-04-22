#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
extern alias syncfusion;

using syncfusion::Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    public sealed partial class UWP_9740 : Page
    {
        public UWP_9740()
        {
            this.InitializeComponent();
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream("Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.GIS Succinctly Annot.pdf");
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            PdfLoadedDocument ldoc = new PdfLoadedDocument(buffer);
            pdfViewer.LoadDocument(fileStream);
        }
        PdfDocument pdfDocument;
        PdfLoadedDocument ldoc;
        bool bFirst, bisEdited;
        PdfPageRotateAngle rotation;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bFirst)
            {
                ldoc = new PdfLoadedDocument((pdfViewer.Save() as MemoryStream).ToArray());
                bFirst = false;
                if (this.pdfViewer.IsDocumentEdited)
                {
                    bisEdited = true;
                }
            }
            else
            {
                if (this.pdfViewer.IsDocumentEdited)
                {
                    bisEdited = true;
                    ldoc = new PdfLoadedDocument((pdfViewer.Save() as MemoryStream).ToArray());
                }
            }
            if (ldoc != null)
            {
                pdfDocument = new PdfDocument();
                //Set Rotate property in page settings
                pdfDocument.PageSettings.Margins.All = 0;
                rotation = PdfPageRotateAngle.RotateAngle0;
                pdfDocument.PageSettings.Rotate = rotation;
                if (pdfDocument.PageSettings.Rotate == PdfPageRotateAngle.RotateAngle0)
                {
                    pdfDocument.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle90;
                }
                else if (pdfDocument.PageSettings.Rotate == PdfPageRotateAngle.RotateAngle90)
                {
                    pdfDocument.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle180;
                }
                else if (pdfDocument.PageSettings.Rotate == PdfPageRotateAngle.RotateAngle180)
                {
                    pdfDocument.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle270;
                }
                else if (pdfDocument.PageSettings.Rotate == PdfPageRotateAngle.RotateAngle270)
                {
                    pdfDocument.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle0;
                }
                rotation = pdfDocument.PageSettings.Rotate;

                for (int i = 0; i < ldoc.Pages.Count; i++)
                {
                    float width = ldoc.Pages[i].Size.Width;
                    float height = ldoc.Pages[i].Size.Height;

                    if (rotation == PdfPageRotateAngle.RotateAngle90 || rotation == PdfPageRotateAngle.RotateAngle270)
                    {
                        width = ldoc.Pages[i].Size.Height;
                        height = ldoc.Pages[i].Size.Width;
                    }

                    if (width > height)
                    {
                        pdfDocument.PageSettings.Orientation = PdfPageOrientation.Portrait;
                    }
                    else
                    {
                        pdfDocument.PageSettings.Orientation = PdfPageOrientation.Landscape;
                    }

                    pdfDocument.PageSettings.Size = new SizeF(width, height);
                    pdfDocument.ImportPage(ldoc, i);
                }
                //Save and load the document in viewer
                MemoryStream memoryStream = new MemoryStream();
                pdfDocument.Save(memoryStream);
                memoryStream.Position = 0;
                PdfLoadedDocument ldocument = new PdfLoadedDocument(memoryStream);
                pdfViewer.LoadDocument(ldocument);
            }
        }
    }
}
