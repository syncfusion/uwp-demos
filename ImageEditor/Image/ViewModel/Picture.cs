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

