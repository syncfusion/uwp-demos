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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SfRibbon.Ribbon
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomBackStagePage : Page
    {
        public CustomBackStagePage()
        {
            this.InitializeComponent();

            description = @" Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula. Lorem tortor neque, purus taciti quis id. Elementum integer orci accumsan minim phasellus vel.";
            description += description;
            description += description;
            this.RichTextContent.Document.SetText(Windows.UI.Text.TextSetOptions.None, description);

            Images = new ObservableCollection<People>();
            Images.Add(new People("Carl", "ms-appx:///Ribbon/Assets/4.jpg"));
            Images.Add(new People("James", "ms-appx:///Ribbon/Assets/1.jpg"));
            Images.Add(new People("Peaches", "ms-appx:///Ribbon/Assets/2.jpg"));
            Images.Add(new People("Linda", "ms-appx:///Ribbon/Assets/3.jpg"));
            Images.Add(new People("Niko", "ms-appx:///Ribbon/Assets/5.jpg"));
            this.DataContext = this;
            SelectedItem = Images[2].Image;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                mainribbon.LayoutUpdated += Mainribbon_LayoutUpdated ;
                mainribbon.BackStageOpened += Mainribbon_BackStageOpened;
            }

        }

       

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(string), typeof(CustomBackStagePage), new PropertyMetadata(0));


        private ObservableCollection<People> images;

        public ObservableCollection<People> Images
        {
            get { return images; }
            set { images = value; }
        }


        string description;

        private void Mainribbon_LayoutUpdated(object sender, object e)
        {
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                GetBackStageButton(mainribbon as DependencyObject);
                this.LayoutUpdated -= Mainribbon_LayoutUpdated;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.flyoutcontent.Height = this.ActualHeight -5;
            
            this.flyoutbutton.Flyout.ShowAt(this.flyoutbutton);
        }

        private void setImage(int i)
        {
            this.contenttitle.Text = Images[i].Name;
            this.SelectedItem = Images[i].Image;
            this.contentpanel.Opacity = 0;
            this.sbAnimateImage.Begin();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
              setImage(0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            setImage(1);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            setImage(2);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            setImage(3);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            setImage(4);
        }
        private void GetBackStageButton(DependencyObject parent)
        {
            bool SearchChild = true;

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                object v = (object)VisualTreeHelper.GetChild(parent, i);

                if (v is Button && (v as Button).Name.ToString() == "PART_BackStage")
                {
                    (v as Button).Click += CustomBackStagePage_Click; 
                }
                if (v is Grid && SearchChild)
                {
                    if ((v as Grid).Name.ToString() == "PART_TitleBar")
                        SearchChild = false;
                    GetBackStageButton(v as DependencyObject);
                }
            }

        }

        private void CustomBackStagePage_Click(object sender, RoutedEventArgs e)
        {
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                if (sender is Button)
                {
                    (sender as Button).Flyout = this.flyoutbutton.Flyout;
                    (sender as Button).Flyout.ShowAt(this.flyoutbutton);
                }
            }
        }
        private void Mainribbon_BackStageOpened(object sender, EventArgs e)
        {
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                mainribbon.CloseBackStage();
        }
    }

    public class People
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public People(string name, string image)
        {
            Name = name;
            Image = image;
        }
    }
}
