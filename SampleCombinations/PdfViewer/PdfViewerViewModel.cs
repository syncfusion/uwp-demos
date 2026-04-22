#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer
{
    class PdfViewerViewModel:INotifyPropertyChanged
    {
        private Stream pdfDocumentStream;

        private string m_documentName;

        string filePath = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DocumentName
        {
            get
            {
                return m_documentName;
            }
            set
            {
                m_documentName = value;
                
            }
        }

        public PdfViewerViewModel()
        {
            filePath = "Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.Assets.Pdf.";
            pdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream(filePath + "Windows Store Apps Succinctly.pdf");
        }
        public Stream PdfDocumentStream
        {
            get
            {
                return pdfDocumentStream;
            }
            set
            {
                pdfDocumentStream = value;
                NotifyPropertyChanged("PdfDocumentStream");

            }
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
