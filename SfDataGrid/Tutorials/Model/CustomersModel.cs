using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the Customers
    /// </summary>
    public class Customers : NotificationObject
    {
        private string _CustomerID;
        private string _CompanyName;
        private string _ContactName;
        private string _ContactTitle;
        private string _Address;
        private string _City;
        private string _PostalCode;
        private string _Country;
        private OrderInfo orders;
        private ProductInfo[] _Products;

        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        /// <value>The customer ID.</value>
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
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                this._CompanyName = value;
                this.RaisePropertyChanged("CompanyName");
            }
        }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        /// <value>The name of the contact.</value>
        public string ContactName
        {
            get
            {
                return this._ContactName;
            }
            set
            {
                this._ContactName = value;
                this.RaisePropertyChanged("ContactName");
            }
        }

        /// <summary>
        /// Gets or sets the contact title.
        /// </summary>
        /// <value>The contact title.</value>
        public string ContactTitle
        {
            get
            {
                return this._ContactTitle;
            }
            set
            {
                this._ContactTitle = value;
                this.RaisePropertyChanged("ContactTitle");
            }
        }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        /// <value>The Address.</value>
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this._Address = value;
                this.RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        /// <value>The City.</value>
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
                this.RaisePropertyChanged("City");
            }
        }

        /// <summary>
        /// Gets or sets the PostalCode.
        /// </summary>
        /// <value>The PostalCode.</value>
        public string PostalCode
        {
            get
            {
                return this._PostalCode;
            }
            set
            {
                this._PostalCode = value;
                this.RaisePropertyChanged("PostalCode");
            }
        }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>The Country.</value>
        public string Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                this._Country = value;
                this.RaisePropertyChanged("Country");
            }
        }

        /// <summary>
        /// Gets or sets the Orders.
        /// </summary>
        /// <value>The Orders.</value>
        public OrderInfo Orders
        {
            get
            {
                return orders;
            }
            set
            {
                this.orders = value;
                RaisePropertyChanged("Orders");
            }
        }

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        /// <value>The Products.</value>
        public ProductInfo[] Products
        {
            get
            {
                return _Products;
            }
            set
            {
                _Products = value;
                RaisePropertyChanged("Products");
            }
        }
    }   
}
