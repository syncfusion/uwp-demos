using Common;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.RadialMenu
{
    public sealed partial class RadialSliderView : SampleLayout
    {
        public RadialSliderView()
        {
            this.InitializeComponent();
            this.Loaded += RadialSlider_Loaded;
        }

        void RadialSlider_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                radialSlider.Foreground = new SolidColorBrush(Colors.Black);
                PointerFactor.FontSize = 16;
                radialSlider.InnerRimFill = new SolidColorBrush(Colors.LightGray);
                radialSlider.InnerRimStroke = new SolidColorBrush(Colors.Black);
                PointerFactor.Background = new SolidColorBrush(Colors.White);
                PointerFactor.BorderBrush = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.controlView.Visibility == Visibility.Visible)
            {
                this.controlView.Visibility = Visibility.Collapsed;
                this.setting.Visibility = Visibility.Visible;
                (sender as Button).Content = "Done";
            }
            else
            {
                this.controlView.Visibility = Visibility.Visible;
                this.setting.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Options";
            }
        }

        public override void Dispose()
        {
            Loaded -= RadialSlider_Loaded;
            if(Tickfrequency != null)
            {
                Tickfrequency.ValueChanged -= Tickfrequency_ValueChanged;
            }

            if (radialSlider != null)
            {
                radialSlider.Dispose();
                radialSlider = null;
            }

            GC.Collect();
        }

        private void Tickfrequency_ValueChanged(object sender, Syncfusion.UI.Xaml.Controls.Input.ValueChangedEventArgs e)
        {
            if(radialSlider!= null && ShowMaximumButton != null)
            {
                radialSlider.TickFrequency = Convert.ToInt32((sender as Syncfusion.UI.Xaml.Controls.Input.SfNumericUpDown).Value);
                
                if((radialSlider.Maximum - radialSlider.Minimum) % radialSlider.TickFrequency != 0)
                {
                    ShowMaximumButton.IsEnabled = true;                   
                }
                else
                {
                    ShowMaximumButton.IsEnabled = false;
                }
            }
        }
    }
}
