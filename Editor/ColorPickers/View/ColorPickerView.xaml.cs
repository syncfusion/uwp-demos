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
    public sealed partial class ColorPickerView : SampleLayout
    {
        public ColorPickerView()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                this.Loaded += ColorPickerDemo_Loaded;
            if (IsMobileFamily())
            {
                viewbox.Width = 250;

            }
           
        }

        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        void ColorPickerDemo_Loaded(object sender, RoutedEventArgs e)
        {
            ColorPicker.VerticalAlignment = VerticalAlignment.Center;
        }

        public override void Dispose()
        {
            if (ColorPicker != null)
                ColorPicker.Dispose();
            Loaded -= ColorPickerDemo_Loaded;
        }
    }
}
