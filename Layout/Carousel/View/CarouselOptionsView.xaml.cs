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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Layout.Carousel
{
    public sealed partial class CarouselOptionsView : UserControl
    {
        CarouselProperties prop;
        public CarouselOptionsView()
        {
            this.InitializeComponent();
            prop = new CarouselProperties();
            this.DataContext = new CarouselView().DataContext;
        }
        public CarouselOptionsView(object datacontext)
        {
            DataContext = datacontext;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (this.DataContext is CarouselProperties)
            //{
            //    (this.DataContext as CarouselProperties).SampleViewVisibility = Visibility.Collapsed;
            //    (this.DataContext as CarouselProperties).OptionsViewVisibility = Visibility.Collapsed;
            //}
        }
    }
}
