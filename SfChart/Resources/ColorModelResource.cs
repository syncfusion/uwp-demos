using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Syncfusion.SampleBrowser.UWP.SfChart.Resources
{
    public static class ColorModelResource
    {
        public static ResourceDictionary Resource
        {
            get
            {
                string assemblyName = Assembly.GetEntryAssembly().GetName().Name;

                if (assemblyName.Equals("Syncfusion.SampleBrowser.UWP"))
                {
                    return new ResourceDictionary { Source = new Uri("ms-appx:///Syncfusion.SampleBrowser.UWP.SfChart/Resources/ColorModel.xaml", UriKind.RelativeOrAbsolute) };
                }
                else
                {
                    return new ResourceDictionary { Source = new Uri("ms-appx:///Resources/ColorModel.xaml", UriKind.RelativeOrAbsolute) };
                }
            }
        }
    }
}