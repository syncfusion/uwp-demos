using Syncfusion.UI.Xaml.ImageEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomView : Page
    {
        ObservableCollection<ToolbarItem> CustomViewItems;
        bool isReplaced;
        CustomViewSettings customViewSettings;

        public CustomView()
        {
            this.InitializeComponent();
            editor.SetToolbarItemVisibility("text, transform, shape, path", false);

            CustomViewItems = new ObservableCollection<ToolbarItem>()
            {
                new CustomToolbarItem(){Icon=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/ITypogy1.png")),CustomName = "ITypogy1",IconHeight=70 },
                new CustomToolbarItem(){Icon=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/ITypogy2.png")),CustomName = "ITypogy2",IconHeight=70 },
                new CustomToolbarItem(){Icon=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/ITypogy3.png")),CustomName = "ITypogy3",IconHeight=70 },
                new CustomToolbarItem(){Icon=new BitmapImage(new Uri("ms-appx:/Image/View/Assets/ITypogy4.png")),CustomName = "ITypogy4",IconHeight=70 },
            };

            var item = new FooterToolbarItem()
            {
                Text = "Add",
                Icon = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/AddIcon.png")),
                SubItems = CustomViewItems,
                TextHeight = 20,
            };
            editor.ToolbarSettings.ToolbarItems.Add(item);

            item = new FooterToolbarItem()
            {
                Text = "Replace",
                Icon = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/ReplaceIcon.png")),
                SubItems = CustomViewItems,
                TextHeight = 20
            };
            editor.ToolbarSettings.ToolbarItems.Add(item);

            item = new FooterToolbarItem()
            {
                Icon = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/BringFrontIcon.png")),
                Text = "Front",
                TextHeight = 20
            };
            editor.ToolbarSettings.ToolbarItems.Add(item);

            item = new FooterToolbarItem()
            {
                Icon = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/SendBack.png")),
                Text = "Back",
                TextHeight = 20
            };
            editor.ToolbarSettings.ToolbarItems.Add(item);
            editor.ToolbarSettings.SubItemToolbarHeight = 70;
            editor.ToolbarSettings.ToolbarItemSelected += OnToolbarItemSelected;
            editor.ItemSelected += ImageEditor_ItemSelected;
        }

        private void ImageEditor_ItemSelected(object sender, ItemSelectedEventArgs args)
        {
            customViewSettings = args.Settings as CustomViewSettings;
        }

        private void OnToolbarItemSelected(object sender, ToolbarItemSelectedEventArgs e)
        {
            string text = string.Empty;
            if (e.ToolbarItem is FooterToolbarItem)
            {
                text = e.ToolbarItem.Text;
                e.MoveSubItemsToFooterToolbar = true;
            }
            else if (e.ToolbarItem is CustomToolbarItem)
                text = (e.ToolbarItem as CustomToolbarItem).CustomName;

            if (text == "Replace")
            {
                isReplaced = true;
            }
            else if (text == "back")
            {
                isReplaced = false;
            }
            else if (text == "Add")
            {
                isReplaced = false;
            }
            if (isReplaced && editor.IsSelected && (text == "ITypogy1" ||
                text == "ITypogy2" || text == "ITypogy3" || text == "ITypogy4"))
            {
                editor.Delete();
                AddImage(text);
            }
            if (!isReplaced)
                AddImage(text);
            if (text == "Front")
                editor.BringToFront();
            else if (text == "Back")
                editor.SendToBack();
        }

        private void AddImage(string text)
        {
            if (text == "ITypogy1")
                SetSVGImage("ITypogy1");

            else if (text == "ITypogy2")
                SetSVGImage("ITypogy2");

            else if (text == "ITypogy3")
                SetSVGImage("ITypogy3");

            else if (text == "ITypogy4")
                SetSVGImage("ITypogy4");
        }

        private void SetSVGImage(string imageName)
        {
            ContentControl contentControl = new ContentControl();
            contentControl.ContentTemplate = this.Resources[imageName] as DataTemplate;
            Viewbox viewbox = new Viewbox() { Height = 200, Width = 200 };
            viewbox.Child = contentControl;
            Object vie = viewbox;
            if (isReplaced)
                editor.AddCustomView(vie as FrameworkElement, new CustomViewSettings() { Bounds = customViewSettings.Bounds });
            else
                editor.AddCustomView(vie as FrameworkElement, new CustomViewSettings());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = (BitmapImage)((sender as Button).Content as Image).Source;

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);
            }

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = (BitmapImage)((sender as Button).Content as Image).Source;

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image = (BitmapImage)((sender as Button).Content as Image).Source;

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                OpenImageEditor(stream);

            }
        }

        private void OpenImageEditor(IRandomAccessStream stream)
        {
            editor.Image = stream as FileRandomAccessStream;
            CustomViewFirstPage.Visibility = Visibility.Collapsed;
            CustomViewSecondPage.Visibility = Visibility.Visible;
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            CustomViewFirstPage.Visibility = Visibility.Visible;
            CustomViewSecondPage.Visibility = Visibility.Collapsed;

        }

        public class CustomToolbarItem : ToolbarItem
        {
            public string CustomName { get; set; }
            public CustomToolbarItem()
            {

            }
        }
    }
}
