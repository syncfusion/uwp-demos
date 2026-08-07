using Common;
using Syncfusion.UI.Xaml.TreeMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TreeMapWinRTSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TreemapLegends : UserControl, IDisposable
    {
        public TreemapLegends()
        {
            InitializeComponent();
        }

        private void rb_ItemsLayoutChecked(object sender, RoutedEventArgs e)
        {
            if (TreeMap != null)
            {
                switch ((sender as RadioButton).Name)
                {
                    case "Squarified":
                        TreeMap.ItemsLayoutMode = TreeMapLayoutMode.Squarified;
                        break;
                    case "SliceAndDiceAuto":
                        TreeMap.ItemsLayoutMode = TreeMapLayoutMode.SliceAndDiceAuto;
                        break;
                    case "SliceAndDiceHorizontal":
                        TreeMap.ItemsLayoutMode = TreeMapLayoutMode.SliceAndDiceHorizontal;
                        break;
                    case "SliceAndDiceVertical":
                        TreeMap.ItemsLayoutMode = TreeMapLayoutMode.SliceAndDiceVertical;
                        break;
                }
            }
        }
        public void Dispose()
        {
            TreeMap.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (settingsGrid.Visibility == Visibility.Collapsed)
            {
                settingsGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
