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
    public sealed partial class PulsingTileView : Common.SampleLayout
    {
        public PulsingTileView()
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

        private void pulseduration_Changed(object sender, Syncfusion.UI.Xaml.Controls.Input.ValueChangedEventArgs e)
        {
            if (hubtile != null)
            {
                SfNumericUpDown updown = sender as SfNumericUpDown;
                hubtile.PulseDuration = TimeSpan.FromSeconds(Double.Parse(pulseduration.Value.ToString()));
            }
        }

        public override void Dispose()
        {
            pulseduration.ValueChanged -= pulseduration_Changed;
            if (hubtile != null)
            {
                hubtile.ImageSource = null;
                hubtile = null;
            }
        }
    }
}
