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

namespace Syncfusion.SampleBrowser.UWP.SfRangeNavigator
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(RangeNavigatorDemo).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "RangeNavigator",
                ProductIcons = "Icons/range_selection.png",
                Header = "Getting Started",
                SampleCategory = "RangeNavigator",
                SearchKeys = new string[] { "Range", "range", "Navi", "navi", "Navigator", "navigator" },
                Tag = Tags.None,
                HasOptions = false
            });
        }
    }
}
