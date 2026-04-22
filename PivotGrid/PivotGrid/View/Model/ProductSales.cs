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
        /// Gets or sets the class values from item source.
        /// </summary>
        public int Class { get; set; }
        /// <summary>
        /// Gets or sets the pid values from item source.
        /// </summary>
        public int PID { get; set; }
        /// <summary>
        /// Gets or sets the color values from item source.
        /// </summary>
        public int Color { get; set; }
        /// <summary>
        /// Gets or sets the color desc values from item source.
        /// </summary>
        public string ColorDesc { get; set; }
        /// <summary>
        /// Gets or sets the size values from item source.
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Gets or sets the units values from item source.
        /// </summary>
        public int Units { get; set; }
        /// <summary>
        /// Gets or sets the retail values from item source.
        /// </summary>
        public double Retail { get; set; }
        /// <summary>
        /// Gets or sets the cost values from item source.
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// Gets or sets the test str values from item source.
        /// </summary>
        public string TestStr { get; set; }
        /// <summary>
        /// Gets or sets the test int values from item source.
        /// </summary>
        public int TestInt { get; set; }
        /// <summary>
        /// Gets or sets the test double values from item source.
        /// </summary>
        public double TestDouble { get; set; }
        /// <summary>
        /// Gets or sets the location values from item source.
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// Returns the collection of objects from item source.
        /// </summary>
        /// <returns></returns>
        public static ProductSalesCollection GetSalesData()
        {
            /// Geography
            string[] countries = new string[] { "Australia", "Canada", "France", "Germany", "United Kingdom", "United States" };
            string[] ausStates = new string[] { "New South Wales", "Queensland", "South Australia", "Tasmania", "Victoria" };
            string[] canadaStates = new string[] { "Alberta", "British Columbia", "Brunswick", "Manitoba", "Ontario", "Quebec" };
            string[] franceStates = new string[] { "Charente-Maritime", "Essonne", "Garonne (Haute)", "Gers", };
            string[] germanyStates = new string[] { "Bayern", "Brandenburg", "Hamburg", "Hessen", "Nordrhein-Westfalen", "Saarland" };
            string[] ukStates = new string[] { "England" };
            string[] ussStates = new string[] { "New York", "North Carolina", "Alabama", "California", "Colorado", "New Mexico", "South Carolina" };

            /// Time
            string[] dates = new string[] { "FY 2005", "FY 2006", "FY 2007", "FY 2008", "FY 2009" };

            /// Products
            string[] products = new string[] { "Bike", "Car" };
            Random r = new Random(123345345);

            int numberOfRecords = 2000;
            ProductSalesCollection listOfProductSales = new ProductSalesCollection();
            for (int i = 0; i < numberOfRecords; i++)
            {
                ProductSales sales = new ProductSales();
                sales.Country = countries[r.Next(1, countries.GetLength(0))];
                sales.Quantity = r.Next(1, 12);
                /// 1 percent discount for 1 quantity
                double discount = (30000 * sales.Quantity) * (double.Parse(sales.Quantity.ToString()) / 100);
                sales.Amount = (30000 * sales.Quantity) - discount;
                sales.Date = dates[r.Next(r.Next(dates.GetLength(0) + 1))];
                sales.Product = products[r.Next(r.Next(products.GetLength(0) + 1))];
                sales.Units = (30000 * sales.Quantity);
                sales.Retail = (30000 * sales.Quantity) * discount;
                sales.Cost = (30000 * sales.Quantity) / 2;
                sales.TestStr = products[r.Next(r.Next(products.GetLength(0) + 1))];

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
    /// <summary>
    /// This class that defines collection of objects which is used as an item source.
    /// It derives from <see cref="BusinessObject">BusinessObject.</see>/>
    /// </summary>
    internal class BusinessObjectCollection : List<BusinessObject>
    {
        /// <summary>
        /// Returns the collection of objects from item source.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static BusinessObjectCollection GetList(int count)
        {
            BusinessObjectCollection list = new BusinessObjectCollection();

            List<string> Fruits = new List<string>(new string[] { "cherry", "mango", "orange", "grape", "Persimmon", "plum", "fig", "apple", "lime", "gooseberry", "strawberry", "rasberry" });
            List<string> Colors = new List<string>(new string[] { "red", "green", "blue", "yellow", "orange", "pink", "crimson", "almond", "white", "black", "aqua", "beige" });

            Random r = new Random(123123);

            for (int i = 0; i < count; ++i)
            {
                BusinessObject bo = new BusinessObject()
                {
                    Fruit = Fruits[r.Next(Fruits.Count)],
                    Color = Colors[r.Next(Colors.Count)],
                    Weight = ((int)(r.NextDouble() * 1000)) / 10d,
                    Even = r.Next(2) == 0,
                    Count = r.Next(4) + 1,
                    Section = r.Next(6)
                };
                list.Add(bo);
            }
            return list;
        }
    }
    /// <summary>
    /// This class that defines collection of objects which is used as an item source.
    /// </summary>
    internal class BusinessObject
    {
        /// <summary>
        /// Gets or sets the fruit values from item source.
        /// </summary>
        public string Fruit { get; set; }
        /// <summary>
        /// Gets or sets the color values from item source.
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Gets or sets the even values from item source.
        /// </summary>
        public bool Even { get; set; }
        /// <summary>
        /// Gets or sets the section values from item source.
        /// </summary>
        public int Section { get; set; }
        /// <summary>
        /// Gets or sets the weight values from item source.
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the count values from item source.
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Returns the string value of object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Fruit=[{0}] Color=[{1}] Even=[{2}] Section=[{3}] Weight=[{4:0.0}] Count=[{5}]", this.Fruit, this.Color, this.Even, this.Section, this.Weight, this.Count);
        }

    }
}
