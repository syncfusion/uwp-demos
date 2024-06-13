#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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

namespace Syncfusion.SampleBrowser.UWP.SfGantt
{

    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(
                new SampleInfo()
                    {
                        ProductIcons = "Images/gantt.png",
                        SampleView = typeof(Getting_Started).AssemblyQualifiedName,
                        Category = Categories.DataVisualization,
                        Product = "Gantt",
                        Header = "Getting Started",
                        SampleCategory = "SfGantt",
                        SearchKeys = new string[] { "Gantt", "Getting","Started" },
                        Description = "This sample demonstrates about the basic features available in the Gantt control.",
                        Tag = Tags.None
                    });
            SampleHelper.SampleViews.Add(
                new SampleInfo()
                    {
                        SampleView = typeof(Timescale).AssemblyQualifiedName,
                        Category = Categories.DataVisualization,
                        Product = "Gantt",
                        Header = "Timescale",
                        SampleCategory = "SfGantt",
                        SearchKeys = new string[] { "Gantt", "Scale", "Timescale" },
                        Description = "This sample demonstrates about how the Timescale can be used in the Gantt control.",
                        Tag = Tags.None
                    });

            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                SampleHelper.SampleViews.Add(
                    new SampleInfo()
                        {
                            SampleView = typeof(Editing).AssemblyQualifiedName,
                            Category = Categories.DataVisualization,
                            Product = "Gantt",
                            Header = "Editing",
                            SampleCategory = "SfGantt",
                            SearchKeys = new string[] { "Gantt", "Edit"},
                            Description = "This sample demonstrates about how to edit the task in the Gantt control",
                            Tag = Tags.None
                        });
            }

            SampleHelper.SampleViews.Add(
                new SampleInfo()
                {
                    SampleView = typeof(Stripline).AssemblyQualifiedName,
                    Category = Categories.DataVisualization,
                    Product = "Gantt",
                    Header = "Stripline",
                    SampleCategory = "SfGantt",
                    DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfGantt/Assets/Stripline.jpg",
                    MobileImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfGantt/Assets/Stripline.jpg",
                    SearchKeys = new string[] { "Gantt", "Stripline", "Strip" },
                    Description = "This sample demonstrates about how the Stripline can be displayed in the Gantt control.",
                    Tag = Tags.None
                });

            SampleHelper.SampleViews.Add(
               new SampleInfo()
               {
                   SampleView = typeof(Holidays).AssemblyQualifiedName,
                   Category = Categories.DataVisualization,
                   Product = "Gantt",
                   Header = "Holidays",
                   SampleCategory = "SfGantt",
                   SearchKeys = new string[] { "Gantt", "Non-Working", "holidays" },
                   Description = "This sample demonstrates about how the non working days can be highlighted in the Gantt control.",
                   Tag = Tags.None
               });

            SampleHelper.SampleViews.Add(
             new SampleInfo()
             {
                 SampleView = typeof(AutoValidation).AssemblyQualifiedName,
                 Category = Categories.DataVisualization,
                 Product = "Gantt",
                 Header = "Auto Validation",
                 SampleCategory = "SfGantt",
                 SearchKeys = new string[] { "Gantt", "validation", "auto","mamual" },
                 Description = "This sample demonstrates about how to validate the task automatically.",
                 Tag = Tags.None
             });

            SampleHelper.SampleViews.Add(
                new SampleInfo()
                    {
                        SampleView = typeof(CriticalPath).AssemblyQualifiedName,
                        Category = Categories.DataVisualization,
                        Product = "Gantt",
                        Header = "Critical Path",
                        SampleCategory = "SfGantt",
                        SearchKeys = new string[] { "Gantt", "critical", "highlight" },
                        Description = "This sample visualizes the critical path in the project.",
                        Tag = Tags.None
                    });

            SampleHelper.SampleViews.Add(
                        new SampleInfo()
                        {
                            SampleView = typeof(TemplateSupport).AssemblyQualifiedName,
                            Category = Categories.DataVisualization,
                            Product = "Gantt",
                            Header = "Template Support",
                            SampleCategory = "SfGantt",
                            SearchKeys = new string[] { "Gantt", "template", "customize" },
                            Description = "This sample demonstrates how to custoimze the default view of items in the chart.",
                            Tag = Tags.None
                        });

            SampleHelper.SampleViews.Add(
                new SampleInfo()
                    {
                        SampleView = typeof(Project_Tracker).AssemblyQualifiedName,
                        Category = Categories.DataVisualization,
                        Product = "Project Tracker",
                        Header = "Project Tracker",
                        SearchKeys = new string[] { "Gantt", "Project", "Track" },
                        SampleType = SampleType.Showcase,
                        DesktopImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfGantt/Assets/ProjectTracker.jpg",
                        MobileImage = "ms-appx:///Syncfusion.SampleBrowser.UWP.SfGantt/Assets/ProjectTracker.jpg",
                        Description = "Visualize and edit project schedule, and track project progress.",
                        Tag = Tags.None
                    });
            SampleHelper.SetTagsForProduct("Gantt", Tags.None);
        }
    }
}
