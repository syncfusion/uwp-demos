using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using System.IO;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    class SerializationViewModel
    {
        public ObservableCollection<SerializationModel> ModelList
        {
            get;
            set;
        }

        public SerializationViewModel()
        {
            ModelList = new ObservableCollection<SerializationModel>
            {
                new SerializationModel { Image=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/CreateNew.png")),EditedStream=null,ImageName="Create New", BackGround=new SolidColorBrush(Colors.Blue),ImageHeight=120,ImageWidth=140,SelectedItemThickness=new Thickness(0,0,0,0)},
                new SerializationModel { Image=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/Chart.jpg")),EditedStream=null,Imagestream="ms-appx:///Image/View/Assets/Chart.txt",ImageName="Chart",BackGround=new SolidColorBrush(Colors.Transparent),ImageHeight=120,ImageWidth=140,SelectionImage=new BitmapImage (new Uri("ms-appx:/Image/View/Assets/NotSelected.png")),SelectedImageVisibility=Visibility.Collapsed,SelectedItemThickness=new Thickness(0,0,0,0)},
                new SerializationModel { Image=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/Venn.jpg")),EditedStream=null,Imagestream="ms-appx:///Image/View/Assets/Venn.txt",ImageName="VennDiagram",BackGround=new SolidColorBrush(Colors.Transparent),ImageHeight=120,ImageWidth=140,SelectionImage=new BitmapImage (new Uri("ms-appx:/Image/View/Assets/NotSelected.png")),SelectedImageVisibility=Visibility.Collapsed,SelectedItemThickness=new Thickness(0,0,0,0)},
                new SerializationModel { Image=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/Freehand.jpg")),EditedStream=null,Imagestream="ms-appx:///Image/View/Assets/FreeHand.txt",ImageName="Freehand",BackGround=new SolidColorBrush(Colors.Transparent),ImageHeight=120,ImageWidth=140,SelectionImage=new BitmapImage (new Uri("ms-appx:/Image/View/Assets/NotSelected.png")),SelectedImageVisibility=Visibility.Collapsed,SelectedItemThickness=new Thickness(0,0,0,0)},
            };
        }
    }

    public class SerializationModel : INotifyPropertyChanged
    {
        private BitmapImage image;
        public BitmapImage Image
        {
            get { return image; }
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }

        private string _imagestream;
        public string Imagestream
        {
            get { return _imagestream; }
            set
            {
                _imagestream = value;
                RaisePropertyChanged("Imagestream");
            }
        }

        private Stream editedstream = null;
        public Stream EditedStream
        {
            get { return editedstream; }
            set
            {
                editedstream = value;
                RaisePropertyChanged("EditedStream");
            }
        }

        private string imagename;
        public string ImageName
        {
            get { return imagename; }
            set
            {
                imagename = value;
                RaisePropertyChanged("ImageName");
            }
        }

        private SolidColorBrush _backGround;
        public SolidColorBrush BackGround
        {
            get { return _backGround; }
            set
            {
                _backGround = value;
                RaisePropertyChanged("BackGround");
            }
        }

        private int _imageheight;
        public int ImageHeight
        {
            get { return _imageheight; }
            set
            {
                _imageheight = value;
                RaisePropertyChanged("ImageHeight");
            }
        }

        private int _imagewidth;
        public int ImageWidth
        {
            get { return _imagewidth; }
            set
            {
                _imagewidth = value;
                RaisePropertyChanged("ImageWidth");
            }
        }

        private ImageSource _selectionImage;
        public ImageSource SelectionImage
        {
            get { return _selectionImage; }
            set
            {
                _selectionImage = value;
                RaisePropertyChanged("SelectionImage");
            }
        }

        private Visibility _selectedImageVisibility;
        public Visibility SelectedImageVisibility
        {
            get { return _selectedImageVisibility; }
            set
            {
                _selectedImageVisibility = value;
                RaisePropertyChanged("SelectedImageVisibility");
            }
        }

        private Thickness _selectedItemThickness;
        public Thickness SelectedItemThickness
        {
            get { return _selectedItemThickness; }
            set
            {
                _selectedItemThickness = value;
                RaisePropertyChanged("SelectedItemThickness");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}