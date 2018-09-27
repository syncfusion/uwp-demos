using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.ImageEditor;
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
using NativeWindows = Windows;
using Windows.UI;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Serialization : Page
    {
        SerializationViewModel model;
        SerializationModel SelectedItem;
        Stream editedStream;
        Stream newEditedStream;
        string textStream;
        BitmapImage image;
        string itemName = "";
        bool isPressed = false;
        CustomHeader item1;
        Stream imagestream;

        public Serialization()
        {
            this.InitializeComponent();
            model = new SerializationViewModel();
            SelectedItem = new SerializationModel();
            deleteImage.Source = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/Delete1.png"));
            listView1.ItemsSource = model.ModelList;
            listView1.SelectionChanged += ListView1_SelectionChanged1;
            listView1.SelectionChanged -= ListView1_SelectionChanged1;
            imgedit.SetToolbarItemVisibility("save", false);
            item1 = new CustomHeader();
            item1.Text = "Save Edits";
            item1.HeaderName = "Save Edits";
            imgedit.ToolbarSettings.ToolbarItems.Add(item1);
            imgedit.ToolbarSettings.ToolbarItemSelected += ToolbarSettings_ToolbarItemSelected;
        }

        private void ToolbarSettings_ToolbarItemSelected(object sender, ToolbarItemSelectedEventArgs e)
        {
            if (e.ToolbarItem is CustomHeader)
            {
                var text = (e.ToolbarItem as CustomHeader).HeaderName;
                if (text == "Save Edits")
                {
                    if (SelectedItem.ImageName == "Create New")
                    {
                        popup.Visibility = Visibility.Visible;
                        grid.Visibility = Visibility.Visible;
                    }
                    else
                        imgedit.Save();
                }
            }
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            itemName = entry.Text;
            imgedit.Save();
            popup.Visibility = Visibility.Collapsed;
            grid.Visibility = Visibility.Collapsed;
            entry.Text = "";
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            popup.Visibility = Visibility.Collapsed;
            grid.Visibility = Visibility.Collapsed;
            entry.Text = "";
        }

        private async void ImageEditor_ImageSaving(object sender, ImageSavingEventArgs args)
        {
            args.Cancel = true;
            imagestream = args.Stream;
            if (SelectedItem.ImageName == "Create New")
            {
                model.ModelList.Add(new SerializationModel
                {
                    EditedStream = imgedit.SaveEdits(),
                    SelectionImage = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/NotSelected.png")),
                    ImageName = itemName != "" ? itemName : ValidateName(),
                    ImageHeight = 120,
                    ImageWidth = 140,
                    BackGround = new SolidColorBrush(Colors.Transparent),
                    SelectedImageVisibility = Visibility.Collapsed,
                    SelectedItemThickness = new Thickness(0, 0, 0, 0),
                    Imagestream = "",
                    Image = await ImageFromBytes(ReadFully(imagestream))
                });
            }
            else
            {
                for (int i = 0; i < model.ModelList.Count; i++)
                {
                    if (SelectedItem.ImageName == model.ModelList[i].ImageName)
                    {
                        model.ModelList[i].EditedStream = imgedit.SaveEdits();
                        LoadingImage(i, imagestream);
                    }
                }
            }
        }

        async void LoadingImage(int i, Stream stream)
        {
            model.ModelList[i].Image = await ImageFromBytes(ReadFully(stream));
            model.ModelList[0].Image = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/CreateNew.png"));
        }

        private string ValidateName()
        {
            String Name = "NewItem";
            int j = 1;
            for (int i = 0; i < model.ModelList.Count; i++)
            {
                if (model.ModelList[i].ImageName == Name)
                {
                    Name = "NewItem " + j;
                    j++;
                }
            }
            return Name;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public async static Task<BitmapImage> ImageFromBytes(Byte[] bytes)
        {
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }
            return image;
        }

        public async Task DelayActionAsync(int delay, Action action)
        {
            await Task.Delay(delay);
            action();
        }

        void Action()
        {
            imgedit.LoadEdits(editedStream);
        }

        private void listView1_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            isPressed = true;
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems.Clear();
            }
            for (int i = 0; i < model.ModelList.Count; i++)
            {
                model.ModelList[i].SelectedImageVisibility = Visibility.Visible;
                model.ModelList[i].SelectionImage = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/NotSelected.png"));
                if (model.ModelList[i].ImageName == "Create New")
                {
                    model.ModelList[i].SelectedImageVisibility = Visibility.Collapsed;
                }
            }
            deleteImage.Visibility = Visibility.Visible;
            listView1.IsItemClickEnabled = false;
            listView1.SelectionChanged += ListView1_SelectionChanged1;
        }

        private void ListView1_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            deleteImage.Visibility = Visibility.Visible;
            for (int i = 0; i < e.AddedItems.Count; i++)
            {
                var item = e.AddedItems[i];
                if ((item as SerializationModel).ImageName == "Create New")
                {
                    listView1.SelectedItems.Remove(item);
                }
                else
                {
                    (item as SerializationModel).SelectionImage = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/Selected.png"));
                    (item as SerializationModel).SelectedItemThickness = new Thickness(15, 15, 15, 15);
                }
            }
            for (int i = 0; i < e.RemovedItems.Count; i++)
            {
                var item = e.RemovedItems[i];
                (item as SerializationModel).SelectionImage = new BitmapImage(new Uri("ms-appx:/Image/View/Assets/NotSelected.png"));
                (item as SerializationModel).SelectedItemThickness = new Thickness(0, 0, 0, 0);
            }
        }

        private void deleteImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var items = listView1.SelectedItems.ToList();
            foreach (SerializationModel item in items)
            {
                (item as SerializationModel).SelectedImageVisibility = Visibility.Collapsed;
                if (model.ModelList.Contains(item))
                    model.ModelList.Remove(item);
            }
            ClearItems();
            isPressed = false;
            listView1.SelectionChanged -= ListView1_SelectionChanged1;
        }

        private void ClearItems()
        {
            for (int i = 1; i < model.ModelList.Count; i++)
            {
                model.ModelList[i].SelectedImageVisibility = Visibility.Collapsed;
                model.ModelList[i].SelectedItemThickness = new Thickness(0, 0, 0, 0);
            }
            listView1.SelectedItems.Clear();
            deleteImage.Visibility = Visibility.Collapsed;
        }

        private async void listView1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if ((e.OriginalSource is Image) || (e.OriginalSource is Grid))
            {
                if ((e.OriginalSource is Image))
                {
                    image = ((e.OriginalSource as Image).DataContext as SerializationModel).Image;
                    newEditedStream = ((e.OriginalSource as Image).DataContext as SerializationModel).EditedStream;
                    SelectedItem.ImageName = ((e.OriginalSource as Image).DataContext as SerializationModel).ImageName;
                    textStream = ((e.OriginalSource as Image).DataContext as SerializationModel).Imagestream;
                }
                else
                {
                    if (((e.OriginalSource as Grid).DataContext as SerializationModel) != null)
                    {
                        newEditedStream = ((e.OriginalSource as Grid).DataContext as SerializationModel).EditedStream;
                        SelectedItem.ImageName = ((e.OriginalSource as Grid).DataContext as SerializationModel).ImageName;
                        textStream = ((e.OriginalSource as Grid).DataContext as SerializationModel).Imagestream;
                    }
                }
                if (((isPressed == false) || (SelectedItem.ImageName == "Create New")) && ((textStream != null) || (SelectedItem.ImageName == "Create New")))
                {
                    if (SerializationSecondPage.Children.Count > 1)
                    {
                        SerializationSecondPage.Children.RemoveAt(1);
                    }
                    if ((newEditedStream == null) && (textStream != null))
                    {
                        StorageFile textFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(textStream));
                        var plainText = await FileIO.ReadTextAsync(textFile);
                        var content = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(plainText));
                        string fname = plainText.ToString();
                        editedStream = content;
                    }
                    else
                    {
                        editedStream = newEditedStream;
                    }
                    this.Loaded();
                    DelayActionAsync(100, Action);
                    SerializationSecondPage.Children.Add(imgedit);
                    Grid.SetRow(imgedit, 1);
                    imgedit.ImageSaving += ImageEditor_ImageSaving;
                    SerializationFirstPage.Visibility = Visibility.Collapsed;
                    SerializationSecondPage.Visibility = Visibility.Visible;
                }
            }
        }

        private new void Loaded()
        {
            imgedit = new UI.Xaml.ImageEditor.SfImageEditor();
            for (int i = 0; i < imgedit.ToolbarSettings.ToolbarItems.Count; i++)
            {
                if (imgedit.ToolbarSettings.ToolbarItems[i].Text == "Save")
                {
                    imgedit.ToolbarSettings.ToolbarItems.RemoveAt(i);
                    imgedit.ToolbarSettings.ToolbarItems.Add(item1);
                }
            }
            imgedit.ToolbarSettings.ToolbarItemSelected += ToolbarSettings_ToolbarItemSelected;
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            SerializationFirstPage.Visibility = Visibility.Visible;
            SerializationSecondPage.Visibility = Visibility.Collapsed;
            listView1.SelectedItems.Clear();
            for(int i=0;i<model.ModelList.Count;i++)
            {
                model.ModelList[i].SelectedImageVisibility = Visibility.Collapsed;
            }
            deleteImage.Visibility = Visibility.Collapsed;
            isPressed = false;
            SelectedItem.ImageName = null;
            textStream = null;
        }
    }

    public class CustomHeader : HeaderToolbarItem
    {
        public string HeaderName { get; set; }
    }
}
