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

namespace Common
{
    public interface ITileItem
    {
        string Description
        {
            get;
            set;
        }

        string Tags
        {
            get;
            set;
        }

        string Type
        {
            get;
            set;
        }

        string GroupHeader
        {
            get;
            set;
        }

        string Image
        {
            get;
            set;
        }

        string Header
        {
            get;
            set;
        }

        string Assembly
        {
            get;
            set;
        }

        int ColumnSpan
        {
            get;

            set;
        }

        int RowSpan
        {
            get;

            set;
        }

        bool EnablePreview
        {
            get; 
            
            set;
        }
    }
    public class TagHelper
    {
        public bool IsPreview { get; set; }
        public bool HasOptions { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsNew { get; set; }
        public TagHelper()
        {

        }
    }
}
