using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Controls.Notification;
using Syncfusion.UI.Xaml.Controls.Input;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Notification.HubTile
{
    public sealed partial class HubTileView : SampleLayout
    {
        public HubTileView()
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

        private HubTileTransitionCollection collection = new HubTileTransitionCollection();

        private SlideTransition slidetransition = new SlideTransition();

        private RotateTransition rotatetransition = new RotateTransition();

        private FadeTransition fadetransition = new FadeTransition();

        private void ToggleSwitch_Toggled_1(object sender, RoutedEventArgs e)
        {
            ToggleSwitch _slide = sender as ToggleSwitch;
            if (_slide.IsOn)
            {
                collection.Add(slidetransition);
            }
            else
            {
                collection.Remove(slidetransition);
            }

        }

        private void rotate_Toggled_1(object sender, RoutedEventArgs e)
        {
            ToggleSwitch _rotate = sender as ToggleSwitch;
            if (_rotate.IsOn)
            {
                collection.Add(rotatetransition);
            }
            else
            {
                collection.Remove(rotatetransition);
            }

        }

        private void fade_Toggled_1(object sender, RoutedEventArgs e)
        {
            ToggleSwitch _fade = sender as ToggleSwitch;
            if (_fade.IsOn)
            {
                collection.Add(fadetransition);
            }
            else
            {
                collection.Remove(fadetransition);
            }

        }

        private void NumericUpDown_ValueChanged_1(object sender, Syncfusion.UI.Xaml.Controls.Input.ValueChangedEventArgs e)
        {
            if (hubtile != null)
            {
                SfNumericUpDown _interval = sender as SfNumericUpDown;
                hubtile.Interval = TimeSpan.FromSeconds(Double.Parse(_interval.Value.ToString()));
            }
        }

        private void hubtile_Loaded_1(object sender, RoutedEventArgs e)
        {
            hubtile.Interval = TimeSpan.FromSeconds(Double.Parse(interval.Value.ToString()));
            hubtile.HubTileTransitions = collection;
        }

        public override void Dispose()
        {
            hubtile.HubTileTransitions.Clear();
            hubtile.Loaded -= hubtile_Loaded_1;

            if (hubtile != null)
            {
                hubtile.Dispose();
                hubtile.Title = null;
                hubtile.SecondaryContent = null;
                hubtile = null;
            }

            fade.Toggled -= fade_Toggled_1;
            rotate.Toggled -= rotate_Toggled_1;
            slide.Toggled -= ToggleSwitch_Toggled_1;
            interval.ValueChanged -=NumericUpDown_ValueChanged_1;
            rotatetransition = null;
            fadetransition = null;
            slidetransition = null;
            collection.Clear();
            collection = null;
        }
    }
}
