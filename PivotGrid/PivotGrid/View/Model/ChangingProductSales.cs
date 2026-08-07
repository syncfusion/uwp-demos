#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace BI.PivotGrid.Tutorials.PivotGridSamples.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    /// <summary>
    /// This class that defines collection of objects which is used as an item source.
    /// It derives from <see cref="INotifyPropertyChanged">INotifyPropertyChanged</see>/>
    /// It derives from <see cref="INotifyPropertyChanging">INotifyPropertyChanging</see>/>
    /// </summary>
    public class ChangingProductSales : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Fields
        /// <summary>
        /// Initializes the product value.
        /// </summary>
        private string product = "";
        /// <summary>
        /// Initializes the date value.
        /// </summary>
        private string date = "";
        /// <summary>
        /// Initializes the country value.
        /// </summary>
        private string country = "";
        /// <summary>
        /// Initializes the state value.
        /// </summary>
        private string state = "";
        /// <summary>
        /// Initializes the quantity value.
        /// </summary>
        private int quantity = 0;
        /// <summary>
        /// Initializes the amount value.
        /// </summary>
        private double amount = 0;
        /// <summary>
        /// Initializes the extra value.
        /// </summary>
        private int extra = 0;
        /// <summary>
        /// Gets or sets the event raising when the property was changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets or sets the event raising when the property is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion


        /// <summary>
        /// Gets or sets the product values from item source.
        /// </summary>
        public string Product
        {
            get
            {
                return this.product;
            }
            set
            {
                if (this.product != value)
                {
                    this.OnPropertyChanging("Product");
                    this.product = value;
                    this.OnPropertyChanged("Product");
                }
            }
        }
        /// <summary>
        /// Gets or sets the date values from item source.
        /// </summary>
        public string Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if (this.date != value)
                {
                    this.OnPropertyChanging("Date");
                    this.date = value;
                    this.OnPropertyChanged("Date");
                }
            }
        }
        /// <summary>
        /// Gets or sets the country values from item source.
        /// </summary>
        public string Country
        {
            get
            {
                return this.country;
            }
            set
            {
                if (this.country != value)
                {
                    this.OnPropertyChanging("Country");
                    this.country = value;
                    this.OnPropertyChanged("Country");
                }
            }
        }
        /// <summary>
        /// Gets or sets the state values from item source.
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                if (this.state != value)
                {
                    this.OnPropertyChanging("State");
                    this.state = value;
                    this.OnPropertyChanged("State");
                }
            }
        }
        /// <summary>
        /// Gets or sets the quantity values from item source.
        /// </summary>
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                if (this.quantity != value)
                {
                    this.OnPropertyChanging("Quantity");
                    this.quantity = value;
                    this.OnPropertyChanged("Quantity");
                }
            }
        }
        /// <summary>
        /// Gets or sets the quantity values from item source.
        /// </summary>
        public double Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                if (this.amount != value)
                {
                    this.OnPropertyChanging("Amount");
                    this.amount = value;
                    this.OnPropertyChanged("Amount");
                }
            }
        }
        /// <summary>
        /// Gets or sets the extra values from item source.
        /// </summary>
        public int Extra
        {
            get
            {
                return this.extra;
            }
            set
            {
                if (this.extra != value)
                {
                    this.OnPropertyChanging("Extra");
                    this.extra = value;
                    this.OnPropertyChanged("Extra");
                }
            }
        }
        /// <summary>
        /// Returns the collection of objects from item source.
        /// </summary>
        /// <returns></returns>
        public static ProductSalesCollection GetSalesData()
        {
            /// Geography
            string[] countries = new string[] { "Australia", "Canada", "France", "Germany", "United Kingdom", "United States" };
            string[] ausStates = new string[] { "New South Wales", "Queensland", "South Australia", "Tasmania", "Victoria" };
            string[] canadaStates = new string[] { "Alberta", "British Columbia", "Brunswick" };
            string[] franceStates = new string[] { "Charente-Maritime", "Essonne", "Garonne (Haute)"};
            string[] germanyStates = new string[] { "Bayern", "Brandenburg", "Hamburg",  "Saarland" };
            string[] ukStates = new string[] { "England" };
            string[] ussStates = new string[] { "New York",  "Alabama", "California", "Colorado", "New Mexico", "South Carolina" };

            /// Time
            string[] dates = new string[] { "FY 2005", "FY 2006", "FY 2008", };

            /// Products
            string[] products = new string[] { "Bike", "Van" };
            Random r = new Random(123345345);

            int numberOfRecords = 2000;
            ProductSalesCollection listOfProductSales = new ProductSalesCollection();
            for (int i = 0; i < numberOfRecords; i++)
            {
                ChangingProductSales sales = new ChangingProductSales();
                sales.Country = countries[r.Next(1, countries.GetLength(0))];
                sales.Quantity = r.Next(1, 12);
                /// 1 percent discount for 1 quantity
                double discount = (30000 * sales.Quantity) * (double.Parse(sales.Quantity.ToString()) / 100);
                sales.Amount = (30000 * sales.Quantity) - discount;
                sales.Date = dates[r.Next(r.Next(dates.GetLength(0) + 1))];
                sales.Product = products[r.Next(r.Next(products.GetLength(0) + 1))];
                switch (sales.Product)
                {
                    case "Car":
                        {
                            sales.Date = "FY2005";
                            break;
                        }
                }
                switch (sales.Country)
                {
                    case "Australia":
                        {
                            sales.State = ausStates[r.Next(ausStates.GetLength(0))];
                            break;
                        }
                    case "Canada":
                        {
                            sales.State = canadaStates[r.Next(canadaStates.GetLength(0))];
                            break;
                        }
                    case "France":
                        {
                            sales.State = franceStates[r.Next(franceStates.GetLength(0))];
                            break;
                        }
                    case "Germany":
                        {
                            sales.State = germanyStates[r.Next(germanyStates.GetLength(0))];
                            break;
                        }
                    case "United Kingdom":
                        {
                            sales.State = ukStates[r.Next(ukStates.GetLength(0))];
                            break;
                        }
                    case "United States":
                        {
                            sales.State = ussStates[r.Next(ussStates.GetLength(0))];
                            break;
                        }
                }
                listOfProductSales.Add(sales);
            }

            return listOfProductSales;
        }
        /// <summary>
        /// Returns the string value of object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", this.Country, this.State, this.Product);
        }

       
        #region INotifyPropertyChanged Members

        /// <summary>
        /// The property changed call back method is invoked when the item property was changed.
        /// </summary>
        /// <param name="name">The property name.</param>
        protected virtual void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region INotifyPropertyChanging Members
        /// <summary>
        /// The property changed call back method is invoked when the item property is changing.
        /// </summary>
        /// <param name="name">The property name.</param>
        protected virtual void OnPropertyChanging(string name)
        {
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        }
        #endregion
        /// <summary>
        /// This class that defines collection of objects which is used as an item source.
        /// It derives from <see cref="ChangingProductSales">ChangingProductSales.</see>/>
        /// </summary>
        public class ProductSalesCollection : ObservableCollection<ChangingProductSales>
        {
            /// <summary>
            /// This event handler method is invoked when object collection was changed.
            /// </summary>
            /// <param name="e"></param>
            protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                //Console.WriteLine(e.Action);
                base.OnCollectionChanged(e);
            }
        }
    }
}
