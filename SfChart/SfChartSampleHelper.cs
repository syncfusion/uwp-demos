#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BasicCharts).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                ProductIcons = "Icons/chart.PNG",
                Header = "Basic Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(FinancialCharts).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Financial Charts",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Palettes).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart",
                Header = "Palettes",
                SampleCategory = "Charts",
                Tag = Tags.None,
                HasOptions = true
            });
        }
    }
}
