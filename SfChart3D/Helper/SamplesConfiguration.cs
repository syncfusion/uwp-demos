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
                SampleView = typeof(Area3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                ProductIcons = "Icons/Chart3D.png",
                Header = "Area3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Bar3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "Bar3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Column3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "Column3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Doughnut3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "Doughnut3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Line3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "Line3D",
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
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(Scatter3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "Scatter3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SemiDoughnut3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "SemiDoughnut3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(SemiPie3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "SemiPie3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(StackingBar1003D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "StackingBar1003D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(StackingBar3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "StackingBar3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(StackingColumn1003D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "StackingColumn1003D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(StackingColumn3D).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "StackingColumn3D",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = false
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(DepthAxis).AssemblyQualifiedName,
                Category = Categories.DataVisualization,
                Product = "Chart3D",
                Header = "DepthAxis",
                SampleCategory = "Chart3D",
                Tag = Tags.None,
                HasOptions = true,
                Description = "This sample illustrates the various 3D Chart types with X, Y and Z Axis.",
                DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfChart3D/Assets/3D.png",
                MobileImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfChart3D/Assets/3D.png"
            });

            #region Product Tags

            SampleHelper.SetTagsForProduct("Chart3D", Tags.None);

            #endregion
        }
    }
}
