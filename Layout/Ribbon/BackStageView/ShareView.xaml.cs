using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SfRibbon.Ribbon
{
    public sealed partial class ShareView : UserControl
    {
        public ShareView()
        {
            this.InitializeComponent();
            Images = new ObservableCollection<Person>();
            Images.Add(new Person("Carl", "ms-appx:///Ribbon/Assets/4.jpg"));
            Images.Add(new Person("James", "ms-appx:///Ribbon/Assets/1.jpg"));
            Images.Add(new Person("Peaches", "ms-appx:///Ribbon/Assets/2.jpg"));
            Images.Add(new Person("Linda", "ms-appx:///Ribbon/Assets/3.jpg"));
            Images.Add(new Person("Niko", "ms-appx:///Ribbon/Assets/5.jpg"));
            this.DataContext = this;
        }

        private ObservableCollection<Person> images;

        public ObservableCollection<Person> Images
        {
            get { return images; }
            set { images = value; }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("File shared successfully.");
            await dialog.ShowAsync();
        }
    }
}
