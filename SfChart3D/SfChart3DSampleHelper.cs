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

namespace Syncfusion.SampleBrowser.UWP.SfChart3D
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Column3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                ProductIcons = "Icons/Chart3D",
                Header = "Column3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Pie3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "Pie3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
        }
    }
}
