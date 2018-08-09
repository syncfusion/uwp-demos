using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SampleBrowser.Editors.Controls.AutoComplete;

namespace Editors
{
    public class ComboBoxProperties : NotificationObject , IDisposable
    {
        private ComboBoxModes comboBoxMode = ComboBoxModes.Editable;

        public ComboBoxModes ComboBoxMode
        {
            get { return comboBoxMode; }
            set { comboBoxMode = value; RaisePropertyChanged("ComboBoxMode"); }
        }

        private string watermarkText = "Type here or select from dropdown.";

        public string WatermarkText
        {
            get { return watermarkText; }
            set { watermarkText = value; RaisePropertyChanged("WatermarkText"); }
        }

        private  ObservableCollection<Person> personDetails;

        public  ObservableCollection<Person> PersonDetails
        {
            get { return personDetails; }
            set { personDetails = value; RaisePropertyChanged("PersonDetails");}
        }

        private ObservableCollection<ProductList> products;

        public ObservableCollection<ProductList> Products
        {
            get { return products; }
            set { products = value; RaisePropertyChanged("Products"); }
        }
        

        private void updatedata()
        {
            products = new ObservableCollection<ProductList>();
            ObservableCollection<string> tools = new ObservableCollection<string>()
            {
                "FontListBox","Gallary","FontComboBox","AutoComplete","CardView",
                "CheckListBox","ColorEdit","ColorPicker","ComboBoxAdv","DockingManager",
                "GroupBar","HierarchyNavigator","Ribbon","TreeviewAdv","TaskBar",
                "WizardControl","BusyIndicator","ButtonAdv","DropDownButton","MaskTextBox",
                "IntegerTextBox","PercentageTextBox","ToolBarAdv","DoubleTextBox"
            };
            ObservableCollection<string> grid = new ObservableCollection<string>()
            {
                "GridControl","GridDataControl","GridTreeControl"
            };
            ObservableCollection<string> chart = new ObservableCollection<string>()
            {
                "AreaChart","BarChart","BubbleChart","CandleChart","ColumnChart",
                "DoughnutChart","FastBarChart","FastHiLoOpenCloseChart","FunnelChart",
                "GanttChart","HiLoAreaChart","HistogramChart","PieChart","Line","PolarChart",
                "RenkoChart","SplineChart","SurfaceChart"
            };
            ObservableCollection<string> gauge = new ObservableCollection<string>()
            {
                "CircularGauge","DigitalGauge","LinearGauge","RollingGauge"
            };
            ObservableCollection<string> Olap = new ObservableCollection<string>()
            {
                "OlapGrid","OlapGauge","OlapClient"
            };
            ObservableCollection<string> pivot = new ObservableCollection<string>()
            {
                "PivotGridControl","PivotSchemaDesigner",
            };
            ObservableCollection<string> spreadsheet = new ObservableCollection<string>()
            {
                "SpreadSheetControl","SpreadSheetRibbon"
            };
            Products.Add(new ProductList() {ProductName = "Tools", Components = tools });
            products.Add(new ProductList() { ProductName = "Grid", Components = grid });
            products.Add(new ProductList() { ProductName = "Chart", Components = chart });
            products.Add(new ProductList() { ProductName = "Gauge", Components = gauge });
            products.Add(new ProductList() { ProductName = "Olap", Components = Olap });
            products.Add(new ProductList() { ProductName = "Pivot", Components = pivot });
            products.Add(new ProductList() { ProductName = "SpreadSheet", Components = spreadsheet });
        }

        public ComboBoxProperties()
        {
            updatedata();
            PersonDetails = new ObservableCollection<Person>();
            PersonDetails.Add(new Person("Eric Joplin", "ms-appx:///TileView/Assets/Emp_02.png", 0.0, "Chairman", "Management", "27/09/1973", "Boston", "+800 9899 9929", "ericjoplin@syncfusion.com", "#FFA400", "#E78E00"));
            PersonDetails.Add(new Person("Paul Vent", "ms-appx:///TileView/Assets/Emp_04.png", 0.0, "Chief Executive Officer", "Management", "27/09/1975", "New York", "+800 9899 9930", "paulvent@syncfusion.com", "#6DA4A3", "#4E7F7D"));
            PersonDetails.Add(new Person("Clara Venus", "ms-appx:///TileView/Assets/Emp_06.png", 0.0, "Chief Executive Assistant", "Management", "27/09/1978", "California", "+800 9899 9931", "claravenus@syncfusion.com", "#A45378", "#883F64"));
            PersonDetails.Add(new Person("Maria Even", "ms-appx:///TileView/Assets/Emp_11.png", 0.0, "Executive Manager", "Operational Unit", "27/09/1970", "New York", "+800 9899 9932", "mariaeven@syncfusion.com", "#DA9545", "#BB7731"));
            PersonDetails.Add(new Person("Mark Zuen", "ms-appx:///TileView/Assets/Emp_13.png", 0.0, "Senior Executive", "Operational Unit", "27/09/1983", "Boston", "+800 9899 9933", "markzuen@syncfusion.com", "#AC3832", "#8B2826"));
            PersonDetails.Add(new Person("Robin Rane", "ms-appx:///TileView/Assets/Emp_16.png", 0.0, "Manager", "Customer Service", "27/09/1985", "New Jersey", "+800 9899 9934", "robinrane@syncfusion.com", "#31A1FF", "#2394E1"));
            PersonDetails.Add(new Person("Chris Marker", "ms-appx:///TileView/Assets/Emp_21.png", 0.0, "Team Manager", "Customer Service", "27/09/1963", "California", "+800 9899 9935", "chrismarker@syncfusion.com", "#5B5BA9", "#484892"));
            PersonDetails.Add(new Person("Seria Sum", "ms-appx:///TileView/Assets/Emp_23.png", 0.0, "Coordinator", "Customer Service", "27/09/1961", "New York", "+800 9899 9936", "seriasum@syncfusion.com", "#597C2A", "#46601D"));
            PersonDetails.Add(new Person("Mathew Fleming", "ms-appx:///TileView/Assets/Emp_25.png", 0.0, "Recruitment Manager", "Human Resource", "27/09/1986", "Boston", "+800 9899 9937", "mathewdfleming@syncfusion.com", "#BCCBD3", "#8BA0A9"));
            PersonDetails.Add(new Person("Steven Joplin", "ms-appx:///TileView/Assets/Emp_02.png", 0.0, "Chairman", "Management", "27/09/1973", "Boston", "+800 9899 9929", "ericjosplin@syncfusion.com", "#FFA400", "#E78E00"));
            PersonDetails.Add(new Person("Carl Vent", "ms-appx:///TileView/Assets/Emp_04.png", 0.0, "Chief Executive Officer", "Management", "27/09/1975", "New York", "+800 9899 9930", "paulavent@syncfusion.com", "#6DA4A3", "#4E7F7D"));
            PersonDetails.Add(new Person("James Venus", "ms-appx:///TileView/Assets/Emp_06.png", 0.0, "Chief Executive Assistant", "Management", "27/09/1978", "California", "+800 9899 9931", "clarahvenus@syncfusion.com", "#A45378", "#883F64"));
            PersonDetails.Add(new Person("Maria Strauss", "ms-appx:///TileView/Assets/Emp_11.png", 0.0, "Executive Manager", "Operational Unit", "27/09/1970", "New York", "+800 9899 9932", "mariaveven@syncfusion.com", "#DA9545", "#BB7731"));
            PersonDetails.Add(new Person("Kate Zuen", "ms-appx:///TileView/Assets/Emp_13.png", 0.0, "Senior Executive", "Operational Unit", "27/09/1983", "Boston", "+800 9899 9933", "markrzuen@syncfusion.com", "#AC3832", "#8B2826"));
            PersonDetails.Add(new Person("Niko Rane", "ms-appx:///TileView/Assets/Emp_16.png", 0.0, "Manager", "Customer Service", "27/09/1985", "New Jersey", "+800 9899 9934", "robinxrane@syncfusion.com", "#31A1FF", "#2394E1"));
            PersonDetails.Add(new Person("Chris gayle", "ms-appx:///TileView/Assets/Emp_21.png", 0.0, "Team Manager", "Customer Service", "27/09/1963", "California", "+800 9899 9935", "chriswmarker@syncfusion.com", "#5B5BA9", "#484892"));
            PersonDetails.Add(new Person("Sloth Sum", "ms-appx:///TileView/Assets/Emp_23.png", 0.0, "Coordinator", "Customer Service", "27/09/1961", "New York", "+800 9899 9936", "seriaqsum@syncfusion.com", "#597C2A", "#46601D"));
            PersonDetails.Add(new Person("Thomas Fleming", "ms-appx:///TileView/Assets/Emp_25.png", 0.0, "Recruitment Manager", "Human Resource", "27/09/1986", "Boston", "+800 9899 9937", "mathewsfleming@syncfusion.com", "#BCCBD3", "#8BA0A9"));
        }
        public void Dispose()
        {
            if (PersonDetails != null)
            {
                PersonDetails.Clear();
            }
            if (Products != null)
            {
                Products.Clear();
            }
        }

    }

    public class ProductList
    {
        public string ProductName { get; set; }
        public ObservableCollection<string> Components { get; set; }
    }
}
