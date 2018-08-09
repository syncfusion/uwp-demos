#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.PdfViewer
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(PdfViewerDemo.MainPage).AssemblyQualifiedName,
                Header = "Getting Started",
                Category = Categories.FileFormat,
                Product = "PDFViewer",
                ProductIcons= "Icons/PdfViewer.png",
                Tag = Tags.None,
                SearchKeys = new string[] { "PDFViewer", "Viewer", "Pdf" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(PdfViewerDemo.FormFillings).AssemblyQualifiedName,
                Header = "Form Fillings",
                Category = Categories.FileFormat,
                Product = "PDFViewer",
                ProductIcons = "Icons/PdfViewer.png",
                Tag = Tags.New,
                SearchKeys = new string[] { "PDFViewer", "Viewer", "Pdf", "Forms","FormFillings", "Form" }
            });
			SampleHelper.SetTagsForProduct("PDFViewer", Tags.Updated);
        }
    }
}
