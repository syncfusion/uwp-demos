using Common;
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

namespace Media.ColorPickers
{
    public sealed partial class ColorPaletteView : SampleLayout
    {
        public ColorPaletteView()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                this.Loaded += ColorPaletteDemo_Loaded;
            if (DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                ColorPalette.Height = 350;
                ColorPalette.Width = 250;

            }
           
        }

      
        void ColorPaletteDemo_Loaded(object sender, RoutedEventArgs e)
        {
            ColorPalette.HorizontalAlignment = HorizontalAlignment.Center;
            ColorPalette.VerticalAlignment = VerticalAlignment.Center;
        }

        public override void Dispose()
        {
            if (ColorPalette != null)
                ColorPalette.Dispose();
            Loaded -= ColorPaletteDemo_Loaded;
        }
    }
}
