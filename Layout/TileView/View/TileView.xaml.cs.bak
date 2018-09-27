
using Syncfusion.UI.Xaml.Controls.Layout;
using System;
using System.Collections.Generic;
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
using Common;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Layout.TileView
{
    public sealed partial class TileView : SampleLayout
    {
        public TileView()
        {
            ResourceDictionary dic = new ResourceDictionary();
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                dic.Source = new Uri("ms-appx:///TileView/View/Template_Winrt.xaml", UriKind.RelativeOrAbsolute);
            else
                dic.Source = new Uri("ms-appx:///TileView/View/Template_WP.xaml", UriKind.RelativeOrAbsolute);

            Application.Current.Resources.MergedDictionaries.Add(dic);
            this.InitializeComponent();

            tile.Visibility = Visibility.Visible;
            tile.Loaded += Tile_Loaded;
        }

        private void Tile_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void orientation_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).ItemsSource = Enum.GetValues(typeof(TileOrientation));
            ((ComboBox)sender).SelectedItem = TileOrientation.Horizontal;
        }

        void Restore(object sender, TappedRoutedEventArgs e)
        {
            tile.SelectedIndex = -1;
        }

        private void orientation_Loaded_2(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).ItemsSource = Enum.GetValues(typeof(MinimizedItemsOrientation));
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                ((ComboBox)sender).SelectedIndex = 2;
            else
                ((ComboBox)sender).SelectedIndex = 1;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
  
        }

        public override void Dispose()
        {
            Loaded -= Tile_Loaded;
            orientation.Loaded -= orientation_Loaded_1;
            minorientation.Loaded -= orientation_Loaded_2;
            if (tile != null)
            {
                tile.SelectionChanged -= tile_SelectionChanged;
                tile.Dispose();
                if (tileviewproperties != null)
                {
                    tileviewproperties.Dispose();
                    tileviewproperties = null;
                }
                if (tile.Items.Count > 0)
                {
                    tile.Items.Clear();
                }

                tile.ItemsSource = null;
                tile.ItemTemplate = null;
                tile.ItemsPanel = null;
                tile.MaximizedItemTemplate = null;
                tile = null;
              
            }
            GC.Collect();

        }

        private void tile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (minorientationtxt != null && minorientation != null && orientationtxt != null && orientation != null)
            {
                if (sender is SfTileView && (sender as SfTileView).SelectedIndex == -1)
                {
                    orientationtxt.Visibility = Visibility.Visible;
                    orientation.Visibility = Visibility.Visible;
                    minorientationtxt.Visibility = Visibility.Collapsed;
                    minorientation.Visibility = Visibility.Collapsed;
                }
                else
                {
                    orientationtxt.Visibility = Visibility.Collapsed;
                    orientation.Visibility = Visibility.Collapsed;
                    minorientationtxt.Visibility = Visibility.Visible;
                    minorientation.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
