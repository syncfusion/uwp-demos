using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagram;

namespace Syncfusion.SampleBrowser.UWP.Diagram
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
#if !STORE_SB
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Diagram Builder",
                    Category = Categories.DataVisualization,
                    SampleType = SampleType.Showcase,
                    DesktopImage = "ms-appx:///ShowcaseImage/Diagrambuilder.jpg",
                    Description =
                        "This application is used to create new or edit existing diagrams like flowchart, circuit diagram. Stencil is available to add nodes into diagram.",
                    Product = "Diagram",
                    SampleView = typeof (DiagramBuilder.MainPage).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
            }

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Floor Planner",
                    Category = Categories.DataVisualization,
                    SampleType = SampleType.Showcase,
                    DesktopImage = "ms-appx:///ShowcaseImage/FloorPlanner.jpg",
                    Description = "Floor planner is an application to create detailed and precise design.",
                    Product = "Diagram",
                    SampleView = typeof (FloorPlannerDemo.MainPage).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Workflow Editor",
                    Category = Categories.DataVisualization,
                    SampleType = SampleType.Showcase,
                    DesktopImage = "ms-appx:///ShowcaseImage/WorkFlowEditor.jpg",
                    Description =
                        "Workflow editor is an application used to represent a process/workflow as block diagram and see the flow during execution.",
                    Product = "Diagram",
                    SampleView = typeof (WorkFlowEditor.MainPage).AssemblyQualifiedName,
                    Tag = Tags.None,
                });
            }

            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    Header = "Organizational Chart",
            //    Category = Categories.DataVisualization,
            //    SampleType = SampleType.Showcase,
            //    DesktopImage = "ms-appx:///ShowcaseImage/OrganizationalChart.jpg",
            //    Description =
            //           "Organization Chart is an application to show the structure of an organization and relationships.",
            //    Product = "Diagram",
            //    SampleView = typeof(OrganizationalChartDemo.MainPage).AssemblyQualifiedName,
            //    Tag = Tags.None,
            //});
            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    Header = "Wireframe Builder",
            //    Category = Categories.DataVisualization,
            //    SampleType = SampleType.Showcase,
            //    DesktopImage = "/ShowcaseImage/Mockup.jpg",
            //    Description = "This sample displays an organizational chart that can be expanded and collapsed as required.",
            //    Product = "Diagram",
            //    SampleView = typeof(Mockup.MainPage).AssemblyQualifiedName,
            //    Tag = Tags.None,
            //});

            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    Header = "Mind Map",
            //    Category = Categories.DataVisualization,
            //    SampleType = SampleType.Showcase,
            //    DesktopImage = "ms-appx:///ShowcaseImage/MindMap.jpg",
            //    Description = "Mind map is an application used to visually organize the idea or information.",
            //    Product = "Diagram",
            //    SampleView = typeof(MindMapDemo.MainPage).AssemblyQualifiedName,
            //    Tag = Tags.None,
            //});
            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    Header = "Workflow Editor",
            //    Category = Categories.DataVisualization,
            //    SampleType = SampleType.Showcase,
            //    DesktopImage = "ms-appx:///ShowcaseImage/WorkFlowEditor.jpg",
            //    Description = "Workflow editor is an application used to represent a process/workflow as block diagram and see the flow during execution.",
            //    Product = "Diagram",
            //    SampleView = typeof(WorkFlowEditor.MainPage).AssemblyQualifiedName,
            //    Tag = Tags.None,
            //});


            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    Header = "UML Diagramming",
            //    Category = Categories.DataVisualization,
            //    SampleType = SampleType.Showcase,
            //    DesktopImage = "ms-appx:///ShowcaseImage/uml.jpg",
            //    Description = "This sample lets uses create and save UML diagrams using the diagram controls touch interface.",
            //    Product = "Diagram",
            //    SampleView = typeof(UMLDiagramDesigner.MainPage).AssemblyQualifiedName,
            //    Tag = Tags.None,
            //});
#endif

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop || DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (FlowDiagram).AssemblyQualifiedName,
                    Header = "Flow Diagram",
                    Product = "Diagram",
                    ProductIcons = "ms-appx:///Assets/diagram.png",
                    SampleCategory = "Getting Started",
                    SearchKeys = new string[] {"Diagram", "FlowChart", "Usage"},
                    Category = Categories.DataVisualization,
                });


                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (NodeContent).AssemblyQualifiedName,
                    Header = "Node Content",
                    SampleCategory = "Getting Started",
                    Product = "Diagram",
                    SearchKeys = new string[] {"Diagram", "Content", "Usage"},
                    Category = Categories.DataVisualization,
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (HierarchicalTree).AssemblyQualifiedName,
                    Header = "Hierarchical Tree",
                    SampleCategory = "Automatic Layout",
                    Product = "Diagram",
                    SearchKeys = new string[] {"Diagram", "HierarchicalTree"},
                    Category = Categories.DataVisualization,
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (RadialTree).AssemblyQualifiedName,
                    Header = "Radial Tree",
                    SampleCategory = "Automatic Layout",
                    Product = "Diagram",
                    SearchKeys = new string[] {"Diagram", "RadialTree"},
                    Category = Categories.DataVisualization,
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (OrganizationChart).AssemblyQualifiedName,
                    Header = "Organization Chart",
                    SampleCategory = "Automatic Layout",
                    Product = "Diagram",
                    SearchKeys = new string[] {"Diagram", "OrganizationChart"},
                    Category = Categories.DataVisualization,
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (SequenceDiagram).AssemblyQualifiedName,
                    Header = "Sequence Diagram",
                    SampleCategory = "Getting Started",
                    Product = "Diagram",
                    SearchKeys = new string[] {"Diagram", "SequenceDiagram"},
                    Category = Categories.DataVisualization,
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof (IshikawaDiagram).AssemblyQualifiedName,
                    Header = "Ishikawa Diagram",
                    SampleCategory = "Getting Started",
                    Product = "Diagram",
                    SearchKeys = new string[] {"Diagram", "IshikawaDiagram"},
                    Category = Categories.DataVisualization,
                });
                //SampleHelper.SampleViews.Add(new SampleInfo()
                //{
                //    SampleView = typeof (Selector).AssemblyQualifiedName,
                //    Header = "Selector",
                //    SampleCategory = "Getting Started",
                //    Product = "Diagram",
                //    SearchKeys = new string[] {"Diagram", "Selector"},
                //    Category = Categories.DataVisualization,
                //});

#if !STORE_SB
                if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                {
                    SampleHelper.SampleViews.Add(new SampleInfo()
                    {
                        SampleView = typeof(RulersAndUnit).AssemblyQualifiedName,
                        Header = "Rulers And Units",
                        SampleCategory = "Getting Started",
                        Product = "Diagram",
                        SearchKeys = new string[] { "Diagram", "Ruler", "Units" },
                        Category = Categories.DataVisualization,
                    });

                }
                //SampleHelper.SampleViews.Add(new SampleInfo()
                //{
                //    SampleView = typeof (Overview).AssemblyQualifiedName,
                //    Header = "Overview",
                //    SampleCategory = "Getting Started",
                //    Product = "Diagram",
                //    SearchKeys = new string[] {"Diagram", "Overview"},
                //    Category = Categories.DataVisualization,
                //});
#endif
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    SampleView = typeof(CircuitDiagram).AssemblyQualifiedName,
                    Header = "Circuit Diagram",
                    SampleCategory = "Getting Started",
                    Product = "Diagram",
                    SearchKeys = new string[] { "Diagram", "Circuit" },
                    Category = Categories.DataVisualization,
                });

            }
        }
    }
}
