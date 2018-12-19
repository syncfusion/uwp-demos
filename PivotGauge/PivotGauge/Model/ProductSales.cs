#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion


namespace Syncfusion.SampleBrowser.UWP.PivotGauge.PivotGauge.Model
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// This class that defines collection of objects which is used as an item source.
    /// </summary>
    internal class ProductSales
    {
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
        /// <summary>
        /// Gets or sets the unit price values from item source.
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// Gets or sets the total price values from item source.
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// Returns the collection of objects from item source.
        /// </summary>
        /// <returns></returns>


        public static ProductSalesCollection GetSalesData()
        {
            // Geography
            string[] countries = new[] { "Australia", "Canada", "France", "Germany", "United Kingdom", "United States" };
            string[] ausStates = new[] { "New South Wales", "Queensland", "South Australia", "Tasmania", "Victoria" };
            string[] canadaStates = new[] { "Alberta", "British Columbia", "Brunswick", "Manitoba", "Ontario", "Quebec" };
            string[] franceStates = new[] { "Charente-Maritime", "Essonne", "Garonne (Haute)", "Gers", };
            string[] germanyStates = new[] { "Bayern", "Brandenburg", "Hamburg", "Hessen", "Nordrhein-Westfalen", "Saarland" };
            string[] ukStates = new[] { "England" };
            string[] ussStates = new[] { "New York", "North Carolina", "Alabama", "California", "Colorado", "New Mexico", "South Carolina" };

            // Time
            string[] dates = new[] { "FY 2005", "FY 2006", "FY 2007", "FY 2008", "FY 2009" };

            // Products
            string[] products = new[] { "Bike", "Van", "Car" };
            Random r = new Random(123345345);

            int numberOfRecords = 2000;
            ProductSalesCollection listOfProductSales = new ProductSalesCollection();
            for (int i = 0; i < numberOfRecords; i++)
            {
                ProductSales sales = new ProductSales();
                sales.Country = countries[r.Next(1, countries.GetLength(0))];
                sales.Quantity = r.Next(1, 12);
                // 1 percent discount for 1 quantity
                double discount = 30000 * sales.Quantity * (double.Parse(sales.Quantity.ToString()) / 100);
                sales.Amount = 30000 * sales.Quantity - discount;
                sales.UnitPrice = sales.Amount / sales.Quantity;
                sales.TotalPrice = sales.Amount + sales.Quantity;
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
        /// <summary>
        /// This class that defines collection of objects which is used as an item source.
        /// It derives from <see cref="ProductSales">ProductSales.</see>/>
        /// </summary>
        public class ProductSalesCollection : List<ProductSales>
        {
        }
    }
}
