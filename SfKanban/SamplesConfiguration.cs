using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.SfKanban
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
            SampleHelper.SampleViews.Add(
                new SampleInfo()
                    {
                        SampleView = typeof(Getting_Started).AssemblyQualifiedName,
                        Category = Categories.DataVisualization,
                        Product = "Kanban",
                        ProductIcons = "Icons/SfKanban.png",
                        Header = "Software Development Process",
                        SampleCategory = "SfKanban",
                        Description =
                            "This sample demonstrates how the Kanban control can be used to manage the software development workflow. ",
                        Tag = Tags.None,
                        SearchKeys = new string[] { "kanban", "Getting", "Started" }
                    });
            SampleHelper.SampleViews.Add(
                new SampleInfo()
                    {
                        SampleView = typeof(Pizza_Hut).AssemblyQualifiedName,
                        Category = Categories.DataVisualization,
                        Product = "Kanban",
                        Header = "Pizza Shop",
                        SampleCategory = "SfKanban",
                        Description =
                            "This sample demonstrates how the Kanban control can be used to manage a pizza ordering workflow. ",
                        Tag = Tags.None,
                        SearchKeys = new string[] { "kanban", "workflow", "drag", "Card" }
                    });
            SampleHelper.SampleViews.Add(
                new SampleInfo()
                {
                    SampleView = typeof(Swimlane).AssemblyQualifiedName,
                    Category = Categories.DataVisualization,
                    Product = "Kanban",
                    Header = "Swimlane",
                    SampleCategory = "SfKanban",
                    Description =
                            "This sample demonstrates how the Kanban can be organized by different projects, teams, users, or other user needs.",
                    Tag = Tags.None,
                    SearchKeys = new string[] { "swimlane" }
                });
            SampleHelper.SetTagsForProduct("Kanban", Tags.None);
        }
    }
}
