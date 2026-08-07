using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the OrderInfo
    /// </summary>
    public partial class OrderInfo : NotificationObject
    {

        private int _OrderID;

        private DateTime _orderDate;

        private string _CustomerID;

        private string _EmployeeID;

        private string _ShipCity;

        private string _ShipCountry;

        private double _unitPrice;

        private double _Freight;

        private bool _isClosed;

        private string _ShipPostalCode;

        private DateTime _Shipping = DateTime.Now;

        private double _Discount;

        private int _Quantity;

        private double _Expense;

        private string _image;

        private bool _isvisible;


        /// <summary>
        /// Initializes a new instance of the <see cref="OrderInfo"/> class.
        /// </summary>
        public OrderInfo()
        {

        }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        /// <value>The order ID.</value>\
        [Display(Name = "Order ID",Order =1 )]
        public int OrderID
        {
            get
            {
                return this._OrderID;
            }
            set
            {
                this._OrderID = value;
                this.RaisePropertyChanged("OrderID");
            }
        }

        /// <summary>
        /// Gets or Sets the OrderDate
        /// </summary>
        public DateTime OrderDate
        {
            get
            {
                return _orderDate;
            }
            set
            {
                _orderDate = value;
            }
        }
        /// <summary>
        /// Gets or sets the shipping.
        /// </summary>
        /// <value>
        /// The shipping.
        /// </value>
        [Display(Name = "Shipping Date", AutoGenerateField = false)]        
        public DateTime Shipping
        {
            get
            {
                return _Shipping;
            }
            set
            {
                _Shipping = value;
                RaisePropertyChanged("Shipping");
            }
        }

        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        /// <value>The customer ID.</value>
        [Display(Name = "Customer ID",Order = 2)]
        public string CustomerID
        {
            get
            {
                return this._CustomerID;
            }
            set
            {
                this._CustomerID = value;
                this.RaisePropertyChanged("CustomerID");
            }
        }

        /// <summary>
        /// Gets or sets quantity
        /// </summary>
        /// <value>the quantity</value>     
        [Display(Name="Quantity", Order = 3 )]
        public int Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                _Quantity = value;
                RaisePropertyChanged("Quantity");
            }
        }

        /// <summary>
        /// Gets or sets Discount
        /// </summary>
        /// <value>the Discount</value> 
        [Display(Name = "Discount",  AutoGenerateField = false)]        
        public double Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
                RaisePropertyChanged("Discount");
            }
        }

        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        /// <value>The employee ID.</value>
        [Display(Name = "EmployeeID", AutoGenerateField = false)]                
        public string EmployeeID
        {
            get
            {
                return this._EmployeeID;
            }
            set
            {
                this._EmployeeID = value;
                this.RaisePropertyChanged("EmployeeID");
            }
        }

        /// <summary>
        /// Gets or sets the ship city.
        /// </summary>
        /// <value>The ship city.</value>
        [Display(Name = "Ship City", Order = 4)]
        public string ShipCity
        {
            get
            {
                return this._ShipCity;
            }
            set
            {
                this._ShipCity = value;
                this.RaisePropertyChanged("ShipCity");
            }
        }

        /// <summary>
        /// Gets or sets the ship country.
        /// </summary>
        /// <value>The ship country.</value>
        [Display(Name = "Ship Country", Order =5)]        
        public string ShipCountry
        {
            get
            {
                return this._ShipCountry;
            }
            set
            {
                this._ShipCountry = value;
                this.RaisePropertyChanged("ShipCountry");
            }
        }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>The unit price.</value>
        [Display(Name = "Unit Price", AutoGenerateField = false)]        
        public double UnitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                _unitPrice = value;
                RaisePropertyChanged("UnitPrice");
            }
        }

        /// <summary>
        /// Gets or sets the freight.
        /// </summary>
        /// <value>The freight.</value>
        [Display(Name = "Freight",AutoGenerateField = false)]        
        public double Freight
        {
            get
            {
                return this._Freight;
            }
            set
            {
                this._Freight = value;
                this.RaisePropertyChanged("Freight");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        [Display(Name = "Is Closed",Order = 6)]        
        public bool IsClosed
        {
            get
            {
                return this._isClosed;
            }

            set
            {
                this._isClosed = value;
                this.RaisePropertyChanged("IsClosed");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is IsVisible.
        /// </summary>
        /// <value><c>true</c> if this instance is IsVisible; otherwise, <c>false</c>.</value>
        public bool IsVisible
        {
            get
            {
                return this._isvisible;
            }

            set
            {
                this._isvisible = value;
                this.RaisePropertyChanged("IsVisible");
            }
        }
        /// <summary>
        /// gets or set ship postal code
        /// </summary>
        [Display(Name = "Ship PostalCode",AutoGenerateField = false)]        
        public string ShipPostalCode
        {
            get
            {
                return this._ShipPostalCode;
            }
            set
            {
                this._ShipPostalCode = value;
                this.RaisePropertyChanged("ShipPostalCode");
            }
        }

        /// <summary>
        /// gets or sets the expense
        /// </summary>
        [Display(Name = "Expense", AutoGenerateField = false)]        
        public double Expense
        {
            get
            {
                return this._Expense;
            }
            set
            {
                this._Expense = value;
                this.RaisePropertyChanged("Expense");
            }
        }

        /// <summary>
        /// gets or set the image
        /// </summary>
        [Display(Name = "Image", AutoGenerateField = false)]                
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
            }
        }        
    }

    /// <summary>
    /// This class represents the ProductInfo
    /// </summary>
    public class ProductInfo : NotificationObject
    {
        private int _ProductID;

        private string _ProductModel;

        private int _UserRating;

        private string _Product;

        private bool _Availability;

        private double _Price;

        private int _shippingDays;

        private string _ProductType;

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        /// <value>The ProductID.</value>
        public int ProductID
        {
            get
            {
                return this._ProductID;
            }
            set
            {
                this._ProductID = value;
                this.RaisePropertyChanged("ProductID");
            }
        }

        /// <summary>
        /// Gets or sets the ProductModel.
        /// </summary>
        /// <value>The productModel.</value>
        public string ProductModel
        {
            get
            {
                return this._ProductModel;
            }
            set
            {
                this._ProductModel = value;
                this.RaisePropertyChanged("ProductModel");
            }
        }

        /// <summary>
        /// Gets or sets the UserRating.
        /// </summary>
        /// <value>The UserRating.</value>
        public int UserRating
        {
            get
            {
                return this._UserRating;
            }
            set
            {
                this._UserRating = value;
                this.RaisePropertyChanged("UserRating");
            }
        }

        /// <summary>
        /// Gets or sets the Availability.
        /// </summary>
        /// <value>The Availability.</value>
        public bool Availability
        {
            get
            {
                return this._Availability;
            }
            set
            {
                this._Availability = value;
                this.RaisePropertyChanged("Availability");
            }
        }

        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        /// <value>The Price.</value>
        public double Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
                this.RaisePropertyChanged("Price");
            }
        }

        /// <summary>
        /// Gets or sets the ShippingDays.
        /// </summary>
        /// <value>The ShippingDays.</value>
        public int ShippingDays
        {
            get
            {
                return this._shippingDays;
            }
            set
            {
                this._shippingDays = value;
                this.RaisePropertyChanged("ShippingDays");
            }
        }

        /// <summary>
        /// Gets or sets the ProductType.
        /// </summary>
        /// <value>The ProductType.</value>
        public string ProductType
        {
            get
            {
                return this._ProductType;
            }
            set
            {
                this._ProductType = value;
                this.RaisePropertyChanged("ProductType");
            }
        }

        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        /// <value>The Product.</value>
        public string Product
        {
            get
            {
                return this._Product;
            }
            set
            {
                this._Product = value;
                this.RaisePropertyChanged("Product");
            }
        }

        
        

    }
}
