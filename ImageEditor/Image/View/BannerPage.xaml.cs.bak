using Syncfusion.UI.Xaml.ImageEditor;
using System;
using System.Collections.Generic;
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
    public sealed partial class BannerPage : Page
    {
        CustomHeader item1;
        Stream imagestream;
        FooterToolbarItem item;
        FooterToolbarItem banner;
        public BannerPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.SizeChanged += (ss, ee) =>
            {
                cancelToolBar.Visibility = Visibility.Collapsed;
            };
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var image = (BitmapImage)((sender as Button).Content as Image).Source;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                NewImageEditor((stream));
            }
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var image = (BitmapImage)((sender as Button).Content as Image).Source;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(image.UriSource);
            if (file != null)
            {
                var stream = await file.OpenAsync(FileAccessMode.Read);
                NewImageEditor((stream));
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            imgeditor.ImageSource = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/twitter.jpeg"));
            NewImageEditor((null));
        }

        private void NewImageEditor(IRandomAccessStream stream)
        {
            imgeditor.Image = stream as FileRandomAccessStream;
            frontPage.Visibility = Visibility.Collapsed;
            imageEditorPage.Visibility = Visibility.Visible;
            imgeditor.SetToolbarItemVisibility("text, save,redo,undo,reset,path,shape,transform", false);

            banner = new FooterToolbarItem()
            {
                Text = "Banner Types",
                SubItems = new System.Collections.ObjectModel.ObservableCollection<Syncfusion.UI.Xaml.ImageEditor.ToolbarItem>()
                {
                    new Syncfusion.UI.Xaml.ImageEditor.ToolbarItem(){Text="Facebook Post"},
                    new Syncfusion.UI.Xaml.ImageEditor.ToolbarItem(){Text="Facebook Cover" },
                    new Syncfusion.UI.Xaml.ImageEditor.ToolbarItem(){Text="Twitter Cover" },
                    new Syncfusion.UI.Xaml.ImageEditor.ToolbarItem(){Text="Twitter Post" },
                    new Syncfusion.UI.Xaml.ImageEditor.ToolbarItem(){Text="YouTubeChannel Cover"},
                },
            };

            item1 = new CustomHeader();
            item = new FooterToolbarItem();
            imgeditor.ToolbarSettings.ToolbarItems.Add(banner);
            item1.HeaderName = "Share";
            BitmapImage bitmap = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/share.png"));

            item1.Icon =  bitmap;
            item1.IconHeight = 15;
            imgeditor.ToolbarSettings.ToolbarItems.Add(item);
            imgeditor.ToolbarSettings.ToolbarItems.Add(item1);
            imgeditor.ToolbarSettings.ToolbarItemSelected += (sender, e) =>
            {
                cancelToolBar.Visibility = Visibility.Collapsed;
                if (e.ToolbarItem is CustomHeader)
                    if ((e.ToolbarItem as CustomHeader).HeaderName == "Share")
                    {
                        Share();
                    }
                if (e.ToolbarItem is Syncfusion.UI.Xaml.ImageEditor.ToolbarItem)
                {
                    var toolitem = e.ToolbarItem as UI.Xaml.ImageEditor.ToolbarItem;
                    if (cancelToolBar.Visibility == Visibility.Visible && toolitem.Text == "Banner Types") { imgeditor.ToggleCropping(); }
                    cancelToolBar.Visibility = Visibility.Collapsed;
                    if (toolitem is HeaderToolbarItem)
                    {
                        var item2 = toolitem as HeaderToolbarItem;
                        if (item2.Text == "Banner Types" && item1.Text == "Share")
                        {
                            cancelToolBar.Visibility = Visibility.Collapsed;
                        }
                    }
                    else if (toolitem.Text != "Banner Types")
                        cancelToolBar.Visibility = Visibility.Visible;
                    if (toolitem.Text == "Facebook Post")
                        imgeditor.ToggleCropping(1200, 900);
                    else if (toolitem.Text == "Facebook Cover")
                        imgeditor.ToggleCropping(851, 315);
                    else if (toolitem.Text == "Twitter Cover")
                        imgeditor.ToggleCropping(1500, 500);
                    else if (toolitem.Text == "Twitter Post")
                        imgeditor.ToggleCropping(1024, 512);
                    else if (toolitem.Text == "YouTubeChannel Cover")
                        imgeditor.ToggleCropping(2560, 1440);
                }
            };
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            frontPage.Visibility = Visibility.Visible;
            imageEditorPage.Visibility = Visibility.Collapsed;
            imgeditor.ToolbarSettings.ToolbarItems.Clear();
        }

        public class CustomHeader : HeaderToolbarItem
        {
            public string HeaderName { get; set; }
        }

        private void Share()
        {
            imgeditor.Save();
            imgeditor.ImageSaving += (sender, args) =>
            {
                args.Cancel = true;
                imagestream = args.Stream;
                Sharing();
            };
        }

        private void DelayAction(int v, Action sharing)
        {
            sharing();
        }

        void Sharing()
        {
            Share share = new Share();
            share.Show("Title", "Message",imagestream);
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            imgeditor.Crop(new Rect());
            cancelToolBar.Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            imgeditor.ToggleCropping();
            cancelToolBar.Visibility = Visibility.Collapsed;
        }
    }
}
