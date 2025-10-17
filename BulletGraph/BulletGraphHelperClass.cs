#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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

namespace Syncfusion.SampleBrowser.UWP.BulletGraph
{
   public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            CollectSampleView();
            CollectShowcaseViews();
        }
        public void CollectSampleView()
        {
#if STORE_SAMPLE
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(BulletGraphUWP_Samples.BulletGraph).AssemblyQualifiedName,SampleType = SampleType.Featured, Product = "BulletGraph",ProductIcons = "Icons/BulletGraph.png", Header = "Getting Started", Tag = Tags.None, Category = Categories.DataVisualization , HasOptions = false  });
#else
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(BulletGraphUWP_Samples.BulletGraph).AssemblyQualifiedName,SampleType = SampleType.Featured, Product = "BulletGraph",ProductIcons = "Icons/BulletGraph.png", Header = "Getting Started", Tag = Tags.None, Category =Categories.DataVisualization , HasOptions = false  });
            SampleHelper.SampleViews.Add(new SampleInfo() { SampleView = typeof(BulletGraphUWP_Samples.BulletGraphMeasuresAndRanges).AssemblyQualifiedName, Product = "BulletGraph", SampleType = SampleType.Featured, ProductIcons = "Icons/BulletGraph.png", Header = "BulletGraphMeasuresAndRanges", Tag = Tags.None, Category = Categories.DataVisualization, HasOptions = false });
#endif
        }

        public void CollectShowcaseViews()
        {
           
        }
    }
   
}
