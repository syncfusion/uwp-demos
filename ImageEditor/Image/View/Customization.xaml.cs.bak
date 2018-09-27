using Syncfusion.UI.Xaml.ImageEditor;
using Syncfusion.UI.Xaml.ImageEditor.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Customization : Page
    {
        bool isOpen;
        bool isShape;
        bool isPath;
        bool isText;
        string location = null;
        Stream imagestream;
        public Customization()
        {         
            this.InitializeComponent();
            editor.ItemSelected += ImageEditor_ItemSelected;
        }
        private object Settings;

        private void ImageEditor_ItemSelected(object sender, ItemSelectedEventArgs args)
        {
            Settings = args.Settings;
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            Share();
        }
        private void Share()
        {
            editor.Save();
            editor.ImageSaving += (sender, args) =>
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

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            editor.Undo();
        }

        private void Rect_Click(object sender, RoutedEventArgs e)
        {
            isPath = false;
            isShape = true;
            isText = false;
            colorPalette.Visibility = Visibility.Visible;
            editor.AddShape(ShapeType.Rectangle, new PenSettings()
            {
                StrokeWidth = 3
            });
        }

        private void Text_Click(object sender, RoutedEventArgs e)
        {
            isPath = false;
            isShape = false;
            isText = true;

            colorPalette.Visibility = Visibility.Visible;
            editor.AddText("Text", new TextSettings() { Color = new SolidColorBrush(Colors.Red) });
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            isPath = true;
            isShape = false;
            isText = false;
            colorPalette.Visibility = Visibility.Visible;
            editor.AddShape(ShapeType.Path, new PenSettings()
            {
                StrokeWidth = 1
            });
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            colorPalette.Visibility = Visibility.Collapsed;
            imageEditornew.Visibility = Visibility.Visible;
            Top.Visibility = Visibility.Collapsed;
            editor.Reset();
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var color = button.Background;
            if (isPath)
            {
                editor.AddShape(ShapeType.Path, new PenSettings() { Color = color });
            }
            else
            {
                if (Settings is PenSettings)
                {
                    (Settings as PenSettings).Color = color;
                }
                else
                {
                    (Settings as TextSettings).Color = color;
                }
            }
        }

        private void imageEditornew_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            imageEditor.Visibility = Visibility.Visible;
            Top.Visibility = Visibility.Visible;
            imageEditornew.Visibility = Visibility.Collapsed;
        }
    }
}
