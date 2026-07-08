#region Copyright Syncfusion Inc. 2001-2016.
// Copyright Syncfusion Inc. 2001-2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.PivotChart.Model
{
    using System;
    using System.Collections.Generic;

    #region ProductSales

    /// <summary>
    /// This class that defines collection of objects which is used as an item source.
    /// </summary>
    public class ProductSales
    {
        #region Properties

        /// <summary>
        /// Gets or sets the product values from item source.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Gets or sets the date values from item source.
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Gets or sets the country values from item source.
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the state values from item source.
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the quantity values from item source.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the amount values from item source.
        /// </summary>
        public double Amount { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the collection of objects from item source.
        /// </summary>
        /// <returns>Collection of sales data.</returns>
        public static ProductSalesCollection GetSalesData()
        {
            // Geography
            string[] countries = new string[] { "Australia", "Canada", "France", "Germany", "United Kingdom", "United States" };
            string[] ausStates = new string[] { "New South Wales", "Queensland", "South Australia", "Tasmania", "Victoria" };
            string[] canadaStates = new string[] { "Alberta", "British Columbia", "Brunswick", "Manitoba", "Ontario", "Quebec" };
            string[] franceStates = new string[] { "Charente-Maritime", "Essonne", "Garonne (Haute)", "Gers", };
            string[] germanyStates = new string[] { "Bayern", "Brandenburg", "Hamburg", "Hessen", "Nordrhein-Westfalen", "Saarland" };
            string[] ukStates = new string[] { "England" };
            string[] ussStates = new string[] { "New York", "North Carolina", "Alabama", "California", "Colorado", "New Mexico", "South Carolina" };

            // Time
            string[] dates = new string[] { "FY 2005", "FY 2006", "FY 2007", "FY 2008", "FY 2009" };

            // Products
            string[] products = new string[] { "Bike", "Car" };
            Random r = new Random(123345345);

            int numberOfRecords = 2000;
            ProductSalesCollection listOfProductSales = new ProductSalesCollection();
            for (int i = 0; i < numberOfRecords; i++)
            {
                ProductSales sales = new ProductSales();
                sales.Country = countries[r.Next(1, countries.GetLength(0))];
                sales.Quantity = r.Next(1, 12);
                // 1 percent discount for 1 quantity
                double discount = (sales.Quantity) * (double.Parse(sales.Quantity.ToString()) / 100);
                sales.Amount = (sales.Quantity) - discount;
                sales.Date = dates[r.Next(r.Next(dates.GetLength(0) + 1))];
                sales.Product = products[r.Next(r.Next(products.GetLength(0) + 1))];

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

        #endregion        
    }

    #endregion

    #region ProductSalesCollection

    /// <summary>
    /// This class that defines collection of objects which is used as an item source.
    /// It derives from <see cref="ProductSales">ProductSales.</see>/>
    /// </summary>
    public class ProductSalesCollection : List<ProductSales>
    {
    }

    #endregion
}