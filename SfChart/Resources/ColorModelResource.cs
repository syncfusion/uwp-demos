#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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

                if (assemblyName.Equals("SampleBrowser"))
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