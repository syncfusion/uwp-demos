#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Common
{
    public class SampleViewItem
    {
        public string Type
        {
            get;
            set;
        }

        public string Header
        {
            get;
            set;
        }
        public bool IsNew
        {
            get;
            set;
        }

        public bool IsUpdated
        {
            get;
            set;
        }

        public bool IsPreview
        {
            get;
            set;
        }

        public bool IsVS2013
        {
            get;
            set;
        }
#if WINDOWS_UAP
        public string content { get; set; }
#else
        public Control content { get; set; }
#endif
        public bool enableOptions = true;
        public bool EnableOptions
        {
            get { return enableOptions; }
            set { enableOptions = value; }
        }

        public string Tag { get; set; }
            
    }
}
