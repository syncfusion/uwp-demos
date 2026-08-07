using Common;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editors
{
    public class DomainUpDownProperties : NotificationObject , IDisposable
    {
        private bool autoreverse = true;
        private bool enableanimation = true;
        private SpinButtonsAlignment buttonalignment;

        private ObservableCollection<Technology> technology = new ObservableCollection<Technology>();
        private ObservableCollection<Product> product = new ObservableCollection<Product>();

        public bool AutoReverse
        {
            get { return autoreverse; }
            set { autoreverse = value; RaisePropertyChanged("AutoReverse"); }
        }

        public bool EnableAnimation
        {
            get { return enableanimation; }
            set { enableanimation = value; RaisePropertyChanged("EnableAnimation"); }
        }

        public SpinButtonsAlignment ButtonAlignment
        {
            get { return buttonalignment; }
            set { buttonalignment = value; RaisePropertyChanged("ButtonAlignment"); }
        }

        public ObservableCollection<Technology> Technology  
        {
            get { return technology; }
            set { technology = value; RaisePropertyChanged("Technology"); }
        }

        public ObservableCollection<Product> Product  
        {
            get { return product; }
            set { product = value; RaisePropertyChanged("Product"); }
        }

        public string WindowsLogoPath { get; set; }

        public DomainUpDownProperties()
        {
            Technology tech1 = new Technology() { Name = "Windows Forms" };
            Technology tech2 = new Technology() { Name = "WPF" };
            Technology tech3 = new Technology() { Name = "Silverlight" };
            Technology tech4 = new Technology() { Name = "ASP .NET" };
            Technology tech5 = new Technology() { Name = "ASP .NET MVC" };

            // Use ResourcePathHelper for image paths
            string basePath = "Editors/Controls/DomainUpDown/Images/";
            Product product1 = new Product() { ProductName = "Essential Tools", ProductIcon = ResourcePathHelper.GetResourcePath(basePath + "Tools.png") };
            Product product2 = new Product() { ProductName = "Essential Chart", ProductIcon = ResourcePathHelper.GetResourcePath(basePath + "Chart.png") };
            Product product3 = new Product() { ProductName = "Essential Grid", ProductIcon = ResourcePathHelper.GetResourcePath(basePath + "Grid.png") };
            Product product4 = new Product() { ProductName = "Essential Report", ProductIcon = ResourcePathHelper.GetResourcePath(basePath + "Report.png") };
            Product product5 = new Product() { ProductName = "Essential Diagram", ProductIcon = ResourcePathHelper.GetResourcePath(basePath + "Diagram.png") };

            technology.Add(tech1);
            technology.Add(tech2);
            technology.Add(tech3);
            technology.Add(tech4);
            technology.Add(tech5);

            product.Add(product1);
            product.Add(product2);
            product.Add(product3);
            product.Add(product4);
            product.Add(product5);

            // Set WindowsLogoPath for XAML binding
            WindowsLogoPath = ResourcePathHelper.GetResourcePath(basePath + "WindowsLogo.png");
        }
        public void Dispose()
        {
            if (Technology != null)
            {
                Technology.Clear();
            }
            if (Product != null)
            {
                Product.Clear();
            }
        }
    }

    public class Technology
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Product
    {
        private string productname;
        private string producticon;

        public string ProductName
        {
            get { return productname; }
            set { productname = value; }
        }
        public string ProductIcon
        {
            get { return producticon; }
            set { producticon = value; }
        }
    }
}
