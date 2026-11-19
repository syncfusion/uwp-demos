

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
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Layout.Carousel
{
    public sealed partial class CarouselView : SampleLayout
    {
        public CarouselView()
        {
            this.InitializeComponent();

            
        }
        void CarouselDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                carousel.Height = 400;
                offset.FontSize = scaleoffset.FontSize = zoffset.FontSize = selectedoffset.FontSize = rotationangle.FontSize = 18;
                offset.BorderBrush = scaleoffset.BorderBrush = zoffset.BorderBrush = selectedoffset.BorderBrush = rotationangle.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                offset.Background = scaleoffset.Background = zoffset.Background = selectedoffset.Background = rotationangle.Background = new SolidColorBrush(Colors.White);
            }
#if (!WINDOWS_STORE)
           carousel.SelectedIndex =9;
#else
            carousel.SelectedIndex = 4;
#endif
        }

        private void previous_Click_1(object sender, RoutedEventArgs e)
        {
            carousel.MovePrevious();
        }

        private void next_Click_1(object sender, RoutedEventArgs e)
        {
            carousel.MoveNext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public override void Dispose()
        {
            next.Click -= next_Click_1;
            previous.Click -= previous_Click_1;
            if (carousel != null)
            {
                carousel.Loaded -= CarouselDemo_Loaded;
                carousel.Dispose();
                
                if (DataContext is CarouselProperties)
                {
                    if((DataContext as CarouselProperties).Images != null)
                       (DataContext as CarouselProperties).Images.Clear();
                    (DataContext as CarouselProperties).Images = null;
                    DataContext = null;
                }

                if (carousel.Items != null && carousel.Items.Count > 0)
                {
                    carousel.Items.Clear();
                }

            }
            
        }
    }
}
