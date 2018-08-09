using Common;
using Syncfusion.SampleBrowser.UWP.Diagram;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Diagram
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RadialTree : SampleLayout
    {
        //Initialize Employee Class
        ObservableCollection<Employee1> employee;
        string imagepath;
        public RadialTree()
        {
            this.InitializeComponent();

            //To Disable ContextMenu
            diagramControl.Menu = null;

            //Initialize Nodes and Connectors
            diagramControl.Nodes = new ObservableCollection<Node>();
            diagramControl.Connectors = new ObservableCollection<Connector>();

            //Item Added Event
            (diagramControl.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;

            employee = new ObservableCollection<Employee1>();

            //To Represent DataSourceSettings Properties
            DataSourceSettings settings = new DataSourceSettings();
            settings.ParentId = "ParentId";
            settings.Id = "EmpId";
            settings.Root = "1";
            settings.DataSource = employee;
            diagramControl.DataSourceSettings = settings;
            imagepath = "ms-appx:///Assets/";
            //Initialize method
            Data();

            diagramControl.Tool = Tool.ZoomPan;
            diagramControl.Constraints = diagramControl.Constraints & ~GraphConstraints.PanRails;

            //Constraints used to enable/disable the Selection

            //To Represent LayoutManager Properties
            diagramControl.LayoutManager = new LayoutManager()
            {
                Layout = new RadialTreeLayout()
            };
            (diagramControl.LayoutManager.Layout as RadialTreeLayout).HorizontalSpacing = 10;
            (diagramControl.LayoutManager.Layout as RadialTreeLayout).VerticalSpacing = 35;

            //Initialize PageSettings and Constraints
            InitializeDiagram();

            //Unload Diagram
            this.Unloaded += diagramControl_Unloaded;
        }

        private void InitializeDiagram()
        {
            diagramControl.Constraints = diagramControl.Constraints &
                                           ~(GraphConstraints.Draggable | GraphConstraints.Selectable);
            diagramControl.Constraints |= GraphConstraints.AllowPan;
            //PageSettings used to enable the Appearance of Diagram Page.
            diagramControl.ScrollSettings.ScrollLimit = ScrollLimit.Diagram;
            diagramControl.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            diagramControl.PageSettings.PageBackground = new SolidColorBrush(Colors.White);
            diagramControl.DefaultConnectorType = ConnectorType.Line;
            diagramControl.PointerReleased += diagramControl_PointerReleased;
        }

        //Apply Node and Connector Style
        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is INode)
            {
                (args.Item as INode).UnitWidth = 20;
                (args.Item as INode).UnitHeight = 20;
                //(args.Item as INode).ContentTemplate = this.Resources["radialTemplate"] as DataTemplate;
                (args.Item as INode).ContentTemplate = XamlReader.Load(R_TreeTemplate.vTemplate) as DataTemplate;
            }
            else if (args.Item is IConnector)
            {
                SolidColorBrush solid = new SolidColorBrush();
                solid.Color = Color.FromArgb(255, 186, 186, 186);
                (args.Item as IConnector).TargetDecoratorStyle = GetStyle("#ffffff");
            }

        }
        private Style GetStyle(string fill)
        {
            string hex = fill;
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
            Style style = new Style();
            style.TargetType = typeof(Shape);
            style.Setters.Add(new Setter() { Property = Shape.FillProperty, Value = myBrush });
            style.Setters.Add(new Setter() { Property = Shape.StrokeThicknessProperty, Value = (double)1.0 });
            style.Setters.Add(new Setter() { Property = Shape.StretchProperty, Value = Stretch.Fill });
            return style;
        }
        //Get Employee Details
        private void Data()
        {
            employee.Add(new Employee1() { EmpId = 1, ParentId = "", Imageurl = imagepath + "Thomas.PNG" });
            employee.Add(new Employee1() { EmpId = 2, ParentId = "1", Imageurl = imagepath + "Clayton.png" });
            employee.Add(new Employee1() { EmpId = 3, ParentId = "1", Imageurl = imagepath + "eric.png" });
            employee.Add(new Employee1() { EmpId = 4, ParentId = "1", Imageurl = imagepath + "John.png" });
            employee.Add(new Employee1() { EmpId = 5, ParentId = "1", Imageurl = imagepath + "image12.png" });
            employee.Add(new Employee1() { EmpId = 6, ParentId = "1", Imageurl = imagepath + "image2.png" });
            employee.Add(new Employee1() { EmpId = 7, ParentId = "1", Imageurl = imagepath + "image3.png" });
            employee.Add(new Employee1() { EmpId = 8, ParentId = "1", Imageurl = imagepath + "image50.png" });
            employee.Add(new Employee1() { EmpId = 9, ParentId = "2", Imageurl = imagepath + "image51.png" });
            employee.Add(new Employee1() { EmpId = 10, ParentId = "2", Imageurl = imagepath + "image53.png" });
            employee.Add(new Employee1() { EmpId = 11, ParentId = "3", Imageurl = imagepath + "image54.png" });
            employee.Add(new Employee1() { EmpId = 12, ParentId = "3", Imageurl = imagepath + "image55.png" });
            employee.Add(new Employee1() { EmpId = 13, ParentId = "4", Imageurl = imagepath + "image56.png" });
            employee.Add(new Employee1() { EmpId = 14, ParentId = "4", Imageurl = imagepath + "image57.png" });
            employee.Add(new Employee1() { EmpId = 15, ParentId = "5", Imageurl = imagepath + "images7.png" });
            employee.Add(new Employee1() { EmpId = 16, ParentId = "5", Imageurl = imagepath + "images9.png" });
            employee.Add(new Employee1() { EmpId = 17, ParentId = "6", Imageurl = imagepath + "Jenny.png" });
            employee.Add(new Employee1() { EmpId = 18, ParentId = "6", Imageurl = imagepath + "John.png" });
            employee.Add(new Employee1() { EmpId = 19, ParentId = "7", Imageurl = imagepath + "eric.png" });
            employee.Add(new Employee1() { EmpId = 20, ParentId = "7", Imageurl = imagepath + "Maria.png" });
            employee.Add(new Employee1() { EmpId = 21, ParentId = "8", Imageurl = imagepath + "image12.png" });
            employee.Add(new Employee1() { EmpId = 22, ParentId = "8", Imageurl = imagepath + "Paul.png" });
            employee.Add(new Employee1() { EmpId = 23, ParentId = "9", Imageurl = imagepath + "Robin.png" });
            employee.Add(new Employee1() { EmpId = 24, ParentId = "9", Imageurl = imagepath + "smith.png" });
            employee.Add(new Employee1() { EmpId = 25, ParentId = "10", Imageurl = imagepath + "Thomas.png" });
            employee.Add(new Employee1() { EmpId = 26, ParentId = "10", Imageurl = imagepath + "Clayton.png" });
            employee.Add(new Employee1() { EmpId = 27, ParentId = "11", Imageurl = imagepath + "eric.png" });
            employee.Add(new Employee1() { EmpId = 28, ParentId = "11", Imageurl = imagepath + "images7.png" });
            employee.Add(new Employee1() { EmpId = 29, ParentId = "12", Imageurl = imagepath + "image12.png" });
            employee.Add(new Employee1() { EmpId = 30, ParentId = "12", Imageurl = imagepath + "image2.png" });
            employee.Add(new Employee1() { EmpId = 31, ParentId = "13", Imageurl = imagepath + "image3.png" });
            employee.Add(new Employee1() { EmpId = 32, ParentId = "13", Imageurl = imagepath + "image50.png" });
            employee.Add(new Employee1() { EmpId = 33, ParentId = "14", Imageurl = imagepath + "image51.png" });
            employee.Add(new Employee1() { EmpId = 34, ParentId = "14", Imageurl = imagepath + "image53.png" });
            employee.Add(new Employee1() { EmpId = 35, ParentId = "15", Imageurl = imagepath + "image54.png" });
            employee.Add(new Employee1() { EmpId = 36, ParentId = "15", Imageurl = imagepath + "image55.png" });
            employee.Add(new Employee1() { EmpId = 37, ParentId = "16", Imageurl = imagepath + "image56.png" });
            employee.Add(new Employee1() { EmpId = 38, ParentId = "16", Imageurl = imagepath + "image57.png" });
            employee.Add(new Employee1() { EmpId = 39, ParentId = "17", Imageurl = imagepath + "images7.png" });
            employee.Add(new Employee1() { EmpId = 40, ParentId = "17", Imageurl = imagepath + "images9.png" });
            employee.Add(new Employee1() { EmpId = 41, ParentId = "18", Imageurl = imagepath + "Jenny.png" });
            employee.Add(new Employee1() { EmpId = 42, ParentId = "18", Imageurl = imagepath + "John.png" });
            employee.Add(new Employee1() { EmpId = 43, ParentId = "19", Imageurl = imagepath + "Clayton.png" });
            employee.Add(new Employee1() { EmpId = 44, ParentId = "19", Imageurl = imagepath + "Maria.png" });
            employee.Add(new Employee1() { EmpId = 45, ParentId = "20", Imageurl = imagepath + "image55.png" });
            employee.Add(new Employee1() { EmpId = 46, ParentId = "20", Imageurl = imagepath + "Paul.png" });
            employee.Add(new Employee1() { EmpId = 47, ParentId = "21", Imageurl = imagepath + "Robin.png" });
            employee.Add(new Employee1() { EmpId = 48, ParentId = "21", Imageurl = imagepath + "smith.png" });
            employee.Add(new Employee1() { EmpId = 49, ParentId = "22", Imageurl = imagepath + "Thomas.png" });
            employee.Add(new Employee1() { EmpId = 50, ParentId = "22", Imageurl = imagepath + "John.png" });
        }

        void diagramControl_Unloaded(object sender, RoutedEventArgs e)
        {
            diagramControl.PointerReleased -= diagramControl_PointerReleased;
            this.Unloaded -= diagramControl_Unloaded;
            //Item Added Event
            (diagramControl.Info as IGraphInfo).ItemAdded -= MainWindow_ItemAdded;
            this.Hspace.TextChanged -= Hspace_TextChanged;
        }

        void diagramControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
        #region Event Handlers

        //To change the Spacing properties
        /// <summary>
        /// HorizontalSpacing and VerticalSpacing  
        /// /// Description:
        /// Provide the spaces between the connectors of the adjacent nodes (Siblings)[HorizontalSpacing].
        /// Provide  spaces between the nodes that lie at the next levels of the layout[VerticalSpacing].        
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
                        (diagramControl.LayoutManager.Layout as RadialTreeLayout).HorizontalSpacing = double.Parse(box.Text);
                        diagramControl.LayoutManager.Layout.UpdateLayout();
                        break;
                    case "Vspace":
                        (diagramControl.LayoutManager.Layout as RadialTreeLayout).VerticalSpacing = double.Parse(box.Text);
                        diagramControl.LayoutManager.Layout.UpdateLayout();
                        break;
                }
            }
        }
        #endregion
    }

    //Initialize Class variables
    public class Employee1
    {
        public int EmpId { get; set; }
        public string Imageurl { get; set; }
        public string ParentId { get; set; }
    }
    public static class R_TreeTemplate
    {
               
      public const string vTemplate = "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
                                       "<Grid>" +
                                       "<Ellipse Height =\"20\" Width=\"20\">" +
                                       "<Ellipse.Fill>" +
                                       "<ImageBrush" +
                                         " ImageSource = \"{Binding Path=Imageurl}\"" +
                                         " Stretch=\"Uniform\" />" +
                                       "</Ellipse.Fill>" +
                                       "</Ellipse>" +
                                       "</Grid>" +
                                       "</DataTemplate>";

    }
}
