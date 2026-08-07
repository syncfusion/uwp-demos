using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Input;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Notification.HubTile
{
    public sealed partial class SplitMosaicTileView : Common.SampleLayout
    {
        public SplitMosaicTileView()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (this.controlView.Visibility == Visibility.Visible)
            //{
            //    this.controlView.Visibility = Visibility.Collapsed;
            //    this.setting.Visibility = Visibility.Visible;
            //    (sender as Button).Content = "Done";
            //}
            //else
            //{
            //    this.controlView.Visibility = Visibility.Visible;
            //    this.setting.Visibility = Visibility.Collapsed;
            //    (sender as Button).Content = "Options";
            //}
        }

        private void interval_ValueChanged(object sender, Syncfusion.UI.Xaml.Controls.Input.ValueChangedEventArgs e)
        {
            if (hubtile != null)
            {
                SfNumericUpDown _interval = sender as SfNumericUpDown;
                hubtile.Interval = TimeSpan.FromSeconds(Double.Parse(_interval.Value.ToString()));
            }
        }

        public override void Dispose()
        {
            interval.ValueChanged -= interval_ValueChanged;
            if (hubtile != null)
            {
                hubtile.Dispose();
                if (hubtile.ImageList != null)
                {
                    hubtile.ImageList.Clear();
                    hubtile.ImageList = null;
                }

                hubtile = null;
            }
        }
    }
}
