using DataGrid;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MasterDetailsViewDemo
{
    /// <summary>
    /// This class represents OrderDetails
    /// </summary>
    public class OrderDetails:INotifyPropertyChanged
    {
        private System.Nullable<int> _OrderID;
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        /// <value>The order ID.</value>
        public System.Nullable<int> OrderID
        {
            get 
            {
                return this._OrderID; 
            }
            set
            {
                this._OrderID = value;
                RaisePropertyChanged("OrderID");
            }
        }

        private int _ProductID;

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        /// <value>The product ID.</value>
        public int ProductID
        {
            get
            {
                return this._ProductID; 
            }
            set
            {
                this._ProductID = value;
                RaisePropertyChanged("ProductID");
            }
        }

        private double _UnitPrice;

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>The unit price.</value>
        public double UnitPrice
        {
            get
            {
                return this._UnitPrice; 
            }
            set
            {
                this._UnitPrice = value;
                RaisePropertyChanged("UnitPrice");
            }
        }
        private Int16 _Quantity;

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public Int16 Quantity
        {
            get 
            {
                return this._Quantity; 
            }
            set
            {
                this._Quantity = value;
                RaisePropertyChanged("Quantity");
            }
        }
        private double _Discount;

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>The discount.</value>
        public double Discount
        {
            get
            {
                return this._Discount; 
            }
            set
            {
                this._Discount = value;
                RaisePropertyChanged("Discount");
            }
        }

        private string _customerID;

        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        /// <value>The customer ID.</value>
        public string CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                _customerID = value;
                RaisePropertyChanged("CustomerID");
            }
        }

        private DateTime _orderDate;

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>The order date.</value>
        public DateTime OrderDate
        {
            get
            {
                return _orderDate;
            }
            set
            {
                _orderDate = value;
                RaisePropertyChanged("OrderDate");
            }
        }


        /// <summary>
        /// gets or set the customer city
        /// </summary>
        private string customerCity;

        public string CustomerCity
        {
            get
            {
                return customerCity;
            }
            set
            {
                customerCity = value;
                RaisePropertyChanged("CustomerCity");
            }
        }

        /// <summary>
        /// gets or set the citydescription
        /// </summary>

        private string citydescription;

        public string CityDescription
        {
            get
            {
                return citydescription;
            }
            set
            {
                citydescription = value;
                RaisePropertyChanged("CityDescription");
            }
        }

        private string productsName;

        /// <summary>
        /// Gets or sets the Product Name.
        /// </summary>
        /// <value>The Product Name.</value>
        public string ProductName
        {
            get
            {
                return productsName;
            }
            set
            {
                productsName = value;
                RaisePropertyChanged("ProductName");
            }
        }

        private string imageLink;

        /// <summary>
        /// Gets or sets the Image Link.
        /// </summary>
        /// <value>The ImageLink.</value>
        public string ImageLink
        {
            get
            {
                return imageLink;
            }
            set
            {
                imageLink = value;
                RaisePropertyChanged("ImageLink");
            }
        }

        private bool isclosed;

        /// <summary>
        /// Gets or sets the IsClosed.
        /// </summary>
        /// <value>The IsClosed.</value>
        public bool IsClosed
        {
            get
            {
                return isclosed;
            }
            set
            {
                isclosed = value;
                RaisePropertyChanged("IsClosed");
            }
        }

        private ObservableCollection<ProductInfoModel> productDetails = new ObservableCollection<ProductInfoModel>();

        /// <summary>
        /// Get or set the ProductDetails
        /// </summary>
        public ObservableCollection<ProductInfoModel> ProductDetails
        {
            get
            {
                GetProductDetails();
                return productDetails;
            }
            set
            {
                productDetails = value;
                RaisePropertyChanged("ProductDetails");
            }
        }

        Random r = new Random();

        /// <summary>
        /// Get the ProductDetails
        /// </summary>
        private void GetProductDetails()
        {           
            for(int i = 0; i < 40 ; i++)
            {
                ProductInfoModel p = new ProductInfoModel();
                p.ProductName = ProductNames[r.Next(ProductNames.Count() - 1)];
                p.Year2008 = r.Next(100, 300);
                p.Year2009 = r.Next(400, 600);
                this.productDetails.Add(p);
            }            
        }

        string[] ProductNames = new string[]
        {
            "Laptop",            
            "Watch",         
            "Footware"
        };

        public OrderDetails()
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetails"/> class.
        /// </summary>
        /// <param name="ord">The ord.</param>
        /// <param name="prod">The prod.</param>
        /// <param name="unit">The unit.</param>
        /// <param name="quan">The quan.</param>
        /// <param name="disc">The disc.</param>
        public OrderDetails(int ord, int prod, double unit, Int16 quan, double disc,string cusid,DateTime ordDt, string city)
        {
            var shipcountry = imagcount < 15 ? ShipCountry[imagcount] : ShipCountry[0];
            this._Discount = disc;
            this._OrderID = ord;
            this._ProductID = prod;
            this._Quantity = quan;
            this._UnitPrice = unit;
            this._customerID = cusid;
            this._orderDate = ordDt;
            this.customerCity = shipcountry;
            this.citydescription = shipcountry;
            this.ProductName = pname + prod % 2 == 0 ? ProductNames[0] : ProductNames[1];           
            if (pname > 2)
                pname = 0;
            this.isclosed = imagcount % 2 == 0 ? true : false;
            if (imagcount % 2 == 0)
                this.ImageLink = "Brazil";
            else if (imagcount % 3 == 0)
                this.imageLink  = "Canada";
            else if (imagcount % 5 == 0)
                this.imageLink = "Germany";
            else
                this.imageLink = "Italy";
            imagcount++;
            if (imagcount > 15)
                imagcount = 1;
        } 

        static int imagcount = 0;
        static int pname = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        string[] ShipCountry = new string[]
        {
            
            "Argentina",
            "Austria",
            "Belgium",
            "Brazil",            
            "Canada",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "Ireland",
            "Italy",
            "Mexico",
            "Norway",
            "Poland",
            "Portugal",
            "Spain",
            "Sweden",
            "Switzerland",
            "UK",
            "USA",
            "Venezuela"
        };
    }
}
