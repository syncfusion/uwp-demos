#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input.DateTimePickers
{
    public class TimePickerProperties : NotificationObject
    {
        private String formatString = "t";

        public String FormatString
        {
            get { return formatString; }
            set { formatString = value; RaisePropertyChanged("FormatString"); }
        }
    }
}
