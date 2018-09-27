using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;
using Common;
using Syncfusion.UI.Xaml.Controls.Layout;
using Windows.UI.Xaml.Data;

namespace Layout.Carousel
{
    public class CarouselProperties:NotificationObject
    {
        private double offset = 60;
        private double selecteditemoffset = 120;       
        private double zoffset = 0;
        private double scaleoffset = 0.7;
        private double rotationangle = 45;
        private VisualMode _visualmode=VisualMode.Default;
        public CarouselProperties()
        {
            Images = new ObservableCollection<Person>();

#if (!WINDOWS_STORE)
            for (int iCount = 1; iCount <= 24; iCount++)
            {
                if (iCount == 10)
                    continue;
                images.Add(new Person() { Image = "ms-appx:///Carousel/Assets/" + iCount + ".jpg" });
            }
#else
            for (int iCount = 1; iCount < 9; iCount++)
            {
                images.Add(new Person() { Image = "ms-appx:///Carousel/Assets/" + iCount + ".jpg" });
            }
#endif

        }


        private ObservableCollection<Person> images;

        public ObservableCollection<Person> Images
        {
            get { return images; }
            set { images = value; }
        }

        

        public VisualMode VisualMode
        {
            get { return _visualmode; }
            set { _visualmode = value; RaisePropertyChanged("VisualMode"); }
        }

        public double Offset
        {
            get { return offset; }
            set { offset = value; RaisePropertyChanged("Offset"); }
        }

        public double SelectedItemOffset
        {
            get { return selecteditemoffset; }
            set { selecteditemoffset = value; RaisePropertyChanged("SelectedItemOffset"); }
        }

        public double RotationAngle
        {
            get { return rotationangle; }
            set { rotationangle = value; RaisePropertyChanged("RotationAngle"); }
        }

        public double ZOffset
        {
            get { return zoffset; }
            set { zoffset = value; RaisePropertyChanged("ZOffset"); }
        }

        public double ScaleOffset
        {
            get { return scaleoffset; }
            set { scaleoffset = value; RaisePropertyChanged("ScaleOffset"); }
        }     

    }


    public class Person
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public Person()
        { }

        public Person(string name, string image)
        {
            Name = name;
            Image = image;
        }
    }

    public class StringToVisulModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            VisualMode mode = (VisualMode)value;
            if (mode == VisualMode.LinearMode)
            {
                return "LinearMode";
            }
            else
                return "Default";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value.ToString() == "LinearMode")
            {
                return VisualMode.LinearMode;
            }
            else
                return VisualMode.Default;
        }
    }
}
