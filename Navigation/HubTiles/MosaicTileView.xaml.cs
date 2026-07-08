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
using Syncfusion.UI.Xaml.Controls.Input;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Notification.HubTile
{
    public sealed partial class MosaicTileView : SampleLayout
    {
        public MosaicTileView()
        {
            this.InitializeComponent();
        }

        private void interval_ValueChanged(object sender, Syncfusion.UI.Xaml.Controls.Input.ValueChangedEventArgs e)
        {
            if (mosaictile != null)
            {
               // SfNumericUpDown _interval = sender as SfNumericUpDown;
              //  mosaictile.Interval = TimeSpan.FromSeconds(Double.Parse(_interval.Value.ToString()));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            //if(this.controlView.Visibility == Visibility.Visible)
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

        public override void Dispose()
        {
            //if(interval != null)
            //interval.ValueChanged -= interval_ValueChanged;
            if (mosaictile != null)
            {
                mosaictile.Dispose();
                if (mosaictile.ImageList != null)
                    mosaictile.ImageList.Clear();
                mosaictile = null;
            }
        }
    }
}
