using Common;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HierarchicalTree : SampleLayout
    {
        //Initialize Employee class
        ObservableCollection<HierarchicalEmployee> employee { get; set; }

        //Initialize DataSourceSettings
        DataSourceSettings settings = new DataSourceSettings();

        public HierarchicalTree()
        {
            this.InitializeComponent();

            //Initialize Nodes and Connectors
            diagramControl.Nodes = new ObservableCollection<Node>();
            diagramControl.Connectors = new ObservableCollection<Connector>();

            employee = new ObservableCollection<HierarchicalEmployee>();
            (diagramControl.Info as IGraphInfo).ItemAdded += HierarchicalTree_ItemAdded;
            //To Disable ContextMenu
            diagramControl.Menu = null;

            //To Represent DataSourceSettings Properties
            settings.ParentId = "ParentId";
            settings.Id = "EmpId";
            settings.Root = "1";            
            //Initialize Method
            Data();
            settings.DataSource = employee;
            diagramControl.DataSourceSettings = settings;

            //Initialize PageSettings and Constraints
            InitializeDiagram();

            //To Represent LayoutManager Properties
            diagramControl.LayoutManager = new Syncfusion.UI.Xaml.Diagram.Layout.LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    HorizontalSpacing=30,
                    VerticalSpacing=70
                }
            };
           
            //To Disable ContextMenu
            diagramControl.Menu = null;
            //To Enable Zooming and Panning
           diagramControl.Tool = Tool.ZoomPan;
          diagramControl.Constraints = diagramControl.Constraints & ~GraphConstraints.PanRails;
            diagramControl.PointerReleased += diagramControl_PointerReleased;

            //Unload Diagram
            this.Unloaded += diagramControl_Unloaded;

        }

        private void HierarchicalTree_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is INode)
            {
                (args.Item as INode).UnitWidth = 120;
                (args.Item as INode).UnitHeight = 40;
                (args.Item as INode).ContentTemplate = XamlReader.Load(H_TreeTemplate.vTemplate) as DataTemplate;
            }
        }

        private void InitializeDiagram()
        {
            //Constraints used to enable/disable the Selectable.
            diagramControl.Constraints = diagramControl.Constraints &
                                         ~(GraphConstraints.Draggable | GraphConstraints.Selectable);
            diagramControl.Constraints |= GraphConstraints.AllowPan;
            //PageSettings used to enable the Appearance of Diagram Page.
            diagramControl.ScrollSettings.ScrollLimit = ScrollLimit.Diagram;
            diagramControl.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            diagramControl.PageSettings.PageBackground = new SolidColorBrush(Colors.White);
            diagramControl.PageSettings.MultiplePage = true;
            diagramControl.PageSettings.MultiplePage = true;
        }

        //Get Employee Details like EmpId,ParentId,Name and _Color.
        private void Data()
        {
            employee.Add(new HierarchicalEmployee() { EmpId = 1, ParentId = "", Name = "Plant Manager", _Color = "#034d6d" });
            employee.Add(new HierarchicalEmployee() { EmpId = 2, ParentId = "1", Name = "Production Manager", _Color = "#1b80c6" });
            employee.Add(new HierarchicalEmployee() { EmpId = 3, ParentId = "1", Name = "Administrative Officer", _Color = "#1b80c6" });
            employee.Add(new HierarchicalEmployee() { EmpId = 4, ParentId = "1", Name = "Maintenance Manager", _Color = "#1b80c6" });
            employee.Add(new HierarchicalEmployee() { EmpId = 5, ParentId = "2", Name = "Control Room", _Color = "#3dbfc9" });
            employee.Add(new HierarchicalEmployee() { EmpId = 6, ParentId = "2", Name = "Plant Operator", _Color = "#3dbfc9" });
            employee.Add(new HierarchicalEmployee() { EmpId = 7, ParentId = "4", Name = "Electrical Supervisor", _Color = "#3dbfc9" });
            employee.Add(new HierarchicalEmployee() { EmpId = 8, ParentId = "4", Name = "Mechanical Supervisor", _Color = "#3dbfc9" });
            employee.Add(new HierarchicalEmployee() { EmpId = 9, ParentId = "5", Name = "Foreman", _Color = "#2bb28e" });
            employee.Add(new HierarchicalEmployee() { EmpId = 10, ParentId = "6", Name = "Foreman", _Color = "#2bb28e" });
            employee.Add(new HierarchicalEmployee() { EmpId = 11, ParentId = "7", Name = "Craft Personnel", _Color = "#2bb28e" });
            employee.Add(new HierarchicalEmployee() { EmpId = 12, ParentId = "7", Name = "Craft Personnel", _Color = "#2bb28e" });
            employee.Add(new HierarchicalEmployee() { EmpId = 13, ParentId = "8", Name = "Craft Personnel", _Color = "#2bb28e" });
            employee.Add(new HierarchicalEmployee() { EmpId = 14, ParentId = "8", Name = "Craft Personnel", _Color = "#2bb28e" });
            employee.Add(new HierarchicalEmployee() { EmpId = 15, ParentId = "9", Name = "Craft Personnel", _Color = "#76d13b" });
            employee.Add(new HierarchicalEmployee() { EmpId = 16, ParentId = "9", Name = "Craft Personnel", _Color = "#76d13b" });
            employee.Add(new HierarchicalEmployee() { EmpId = 17, ParentId = "10", Name = "Craft Personnel", _Color = "#76d13b" });
        }

        void diagramControl_Unloaded(object sender, RoutedEventArgs e)
        {
            diagramControl.PointerReleased -= diagramControl_PointerReleased;
            this.Unloaded -= diagramControl_Unloaded;
            this.Hspace.TextChanged -= Hspace_TextChanged;
        }

        void diagramControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #region Event Handlers

        //Orientation changed
        ///<summary>
        ///TreeOrientation
        ///Description:
        ///The Layout Manager lets you orient the tree in many directions and create sophisticated arrangements. 
        ///The Orientation property of the Diagram model can be used to specify the tree orientation.
        /// </summary>
        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if ((sender as ComboBox).SelectedItem != null)
        //    {
        //        ComboBoxItem cbitem = (sender as ComboBox).SelectedItem as ComboBoxItem;
        //        if (cbitem.Content.ToString() == "TopBottom")
        //        {
        //            if (diagramModel != null)
        //            {
        //                refreshLayout(TreeOrientation.TopBottom);
        //            }
        //        }
        //        else if (cbitem.Content.ToString() == "BottomTop")
        //        {

        //            refreshLayout(TreeOrientation.BottomTop);
        //        }
        //        else if (cbitem.Content.ToString() == "LeftRight")
        //        {
        //            refreshLayout(TreeOrientation.LeftRight);
        //        }
        //        else
        //        {
        //            refreshLayout(TreeOrientation.RightLeft);
        //        }
        //    }
        //}

        //To change the Spacing properties
        /// <summary>
        /// HorizontalSpacing ,VerticalSpacing and SpaceBetweenSubTrees
        /// Description:
        /// Provide the spaces between the connectors of the adjacent nodes (Siblings)[HorizontalSpacing].
        /// Provide  spaces between the nodes that lie at the next levels of the layout[VerticalSpacing].
        /// Provide the sthe spaces between adjacent Subtrees[SpaceBetweenSubTrees].
        /// Property of DiagramModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hspace_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (sender as TextBox);
              if (box.Text != string.Empty)
                {
                    switch (box.Name)
                    {
                        case "Hspace":
                            (diagramControl.LayoutManager.Layout as DirectedTreeLayout).HorizontalSpacing = double.Parse(box.Text);
                            diagramControl.LayoutManager.Layout.UpdateLayout();
                            break;
                        case "Vspace":
                            (diagramControl.LayoutManager.Layout as DirectedTreeLayout).VerticalSpacing = double.Parse(box.Text);
                            diagramControl.LayoutManager.Layout.UpdateLayout();
                            break;
                    }
                }
        }

        #endregion
    }

    //Initailize class variables
    public class HierarchicalEmployee
    {
        public int EmpId { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string _Color { get; set; }
    }
    public static class H_TreeTemplate
    {
        public const string vTemplate = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                         "<Grid>" +
                                         "<Border Background = \"{Binding Path = _Color}\" >" +
                                         "<TextBlock TextWrapping=\"Wrap\"  Text=\"{Binding Path = Name}\" FontSize=\"14px\" VerticalAlignment=\"Center\" HorizontalAlignment=\"Center\"" +
                                           " Foreground=\"White\"/>" +
                                         "</Border>" +
                                         "</Grid>" +
                                         "</DataTemplate>";

    }
}
