#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
   public class Picture:INotifyPropertyChanged
    {
        private ImageSource photo;
        public ImageSource Photo
        {
            get
            {
                return photo;
            }
            set
            { 
                photo = value;
                OnPropertyChanged("Photo");
            }
        }
        public Picture()
        {
        }

        public Picture(ImageSource photo)
        {
            this.Photo = photo;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

