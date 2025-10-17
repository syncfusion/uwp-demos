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
using Windows.UI.Xaml.Media;

namespace Common
{
    public class Tile : NotificationObject, ITileItem
    {
        private string sampleName;
        private bool enablepreview;

        public bool EnablePreview
        {
            get { return enablepreview; }
            set { enablepreview = value; }
        }

        private bool enableupdated;

        public bool EnableUpdated
        {
            get { return enableupdated; }
            set { enableupdated = value; }
        }

        private bool enablenew;

        public bool EnableNew
        {
            get { return enablenew; }
            set { enablenew = value; }
        }

        private bool enableVS2013;

        public bool EnableVS2013
        {
            get { return enableVS2013; }
            set { enableVS2013 = value; }
        }
        private string tags;

        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        private string description = "Description goes here...";

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string groupheader;

        public string GroupHeader
        {
            get { return groupheader; }
            set { groupheader = value; }
        }

        private string header;

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        private string color ="#FF2673EC";

        public string Color
        {
            get { return color; }
            set { color = value; }
        }


        private string assembly;

        public string Assembly
        {
            get { return assembly; }
            set { assembly = value; }
        }

        private int columnSpan = 1;

        public int ColumnSpan
        {
            get { return columnSpan; }
            set { columnSpan = value; }
        }

        private int rowSpan = 1;

        public int RowSpan
        {
            get { return rowSpan; }
            set { rowSpan = value; }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        private string liveImage;

        public string LiveImage
        {
            get { return liveImage; }
            set { liveImage = value; }
        }

        private string transition;

        public string Transition
        {
            get { return transition; }
            set { transition = value; }
        }

        private TimeSpan interval;

        public TimeSpan Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        private bool needAnimation;

        public bool NeedAnimation
        {
            get { return needAnimation; }
            set { needAnimation = value; }
        }

        private string categoryname;
        public string CategoryName
        {
            get
            {
                return categoryname;
            }

            set
            {
                categoryname = value;
            }
        }

        private bool isselected;
        public bool IsSelected
        {
            get
            {
                return isselected;
            }

            set
            {
               isselected=value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public string SampleName
        {
            get
            {
                return sampleName;
            }

            set
            {
                sampleName = value;
            }
        }
    }
}
