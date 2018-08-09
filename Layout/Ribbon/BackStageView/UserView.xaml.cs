using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class UserView : UserControl
    {
        public UserView()
        {
            this.InitializeComponent();
            Images = new ObservableCollection<Person>();
            Images.Add(new Person("Carl", "ms-appx:///Ribbon/Assets/4.jpg"));
            Images.Add(new Person("James", "ms-appx:///Ribbon/Assets/1.jpg"));
            Images.Add(new Person("Peaches", "ms-appx:///Ribbon/Assets/2.jpg"));
            Images.Add(new Person("Linda", "ms-appx:///Ribbon/Assets/3.jpg"));
            Images.Add(new Person("Niko", "ms-appx:///Ribbon/Assets/5.jpg"));
            this.DataContext = this;
            SelectionImage = Images[0].Image;
        }

        public int SelectedItem
        {
            get { return (int)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(int), typeof(UserView), new PropertyMetadata(0));

        public string SelectionImage { get; set; }

        private ObservableCollection<Person> images;

        public ObservableCollection<Person> Images
        {
            get { return images; }
            set { images = value; }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionImage = Images[SelectedItem].Image;
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public Person(string name, string image)
        {
            Name = name;
            Image = image;
        }
    }
}
