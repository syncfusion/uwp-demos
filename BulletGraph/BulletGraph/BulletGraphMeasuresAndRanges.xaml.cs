#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace BulletGraphUWP_Samples
{
    public sealed partial class BulletGraphMeasuresAndRanges : SampleLayout,IDisposable
    {
        Dictionary<string, Windows.UI.Color> colorsList = new Dictionary<string, Windows.UI.Color>();

        public BulletGraphMeasuresAndRanges()
        {
            this.InitializeComponent();

            Type colorType = typeof(Windows.UI.Colors);
            IEnumerable<PropertyInfo> propInfos = colorType.GetRuntimeProperties();
            colorsList.Add("Limeade", Windows.UI.Color.FromArgb(255, 97, 163, 1));
            colorsList.Add("Candlelight", Windows.UI.Color.FromArgb(255, 252, 218, 33));
            colorsList.Add("Cherry", Windows.UI.Color.FromArgb(255, 214, 30, 64));
            foreach (PropertyInfo propInfo in propInfos)
            {
                colorsList.Add(propInfo.Name, (Windows.UI.Color)propInfo.GetValue(colorType));
            }

            cmb_Range1Stroke.ItemsSource = cmb_Range2Stroke.ItemsSource = cmb_Range3Stroke.ItemsSource = colorsList.Keys;

            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                this.SfBulletGraph.QuantitativeScaleLength = 450;
            }
        }


        private void cmb_Range1Stroke_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SfBulletGraph.QualitativeRanges[0].RangeStroke = new SolidColorBrush(colorsList[cmb_Range1Stroke.SelectedItem.ToString()]);
        }

        private void cmb_Range2Stroke_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SfBulletGraph.QualitativeRanges[1].RangeStroke = new SolidColorBrush(colorsList[cmb_Range2Stroke.SelectedItem.ToString()]);
        }

        private void cmb_Range3Stroke_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SfBulletGraph.QualitativeRanges[2].RangeStroke = new SolidColorBrush(colorsList[cmb_Range3Stroke.SelectedItem.ToString()]);
        }

        public override void Dispose()
        {
            this.SfBulletGraph.Dispose();
            this.SfBulletGraph = null;
        }
       
    }
}
