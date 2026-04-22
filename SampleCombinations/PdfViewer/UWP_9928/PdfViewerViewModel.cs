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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.PdfViewer.PdfViewer.UWP_9928
{
    public class PdfViewerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Stream _pdfDocument1, _pdfDocument2, _pdfDocument3;
        public Stream PdfDocument1
        {
            get
            {
                if(_pdfDocument1==null)
                {
                    _pdfDocument1 = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("ReplicationSample.Assets.F# Succinctly.pdf");
                }
                return _pdfDocument1;
            }
            set
            {
                _pdfDocument1 = value;
                NotifyPropertyChanged("PdfDocument1");
            }
        }

        public Stream PdfDocument2
        {
            get
            {
                if (_pdfDocument2 == null)
                {
                    _pdfDocument2 = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("ReplicationSample.Assets.GIS Succinctly.pdf");
                }
                return _pdfDocument2;
            }
            set
            {
                _pdfDocument2 = value;
                NotifyPropertyChanged("PdfDocument2");
            }
        }

        public Stream PdfDocument3
        {
            get
            {
                if (_pdfDocument3 == null)
                {
                    _pdfDocument3 = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("ReplicationSample.Assets.HTTP Succinctly.pdf");
                }
                return _pdfDocument3;
            }
            set
            {
                _pdfDocument3 = value;
                NotifyPropertyChanged("PdfDocument3");
            }
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PdfViewerViewModel()
        {
           
        }
    }
}
