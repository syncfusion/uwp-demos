#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace Syncfusion.SampleBrowser.UWP.PivotGrid.PivotGrid.View.Model
{
    /// <summary>
    /// This class that defines collection of objects which is used as an item source.
    /// </summary>
    public class DataPoint
    {
        #region Constructor
        /// <summary>
        /// Initializes the new instance of <see cref="DataPoint">DataPoint.</see>/>
        /// </summary>
        /// <param name="category">The Category value.</param>
        /// <param name="country">The Country value.</param>
        /// <param name="currency">The Currency value.</param>
        /// <param name="id">The ID value.</param>
        /// <param name="item">The Item value.</param>
        /// <param name="state">The State value.</param>
        /// <param name="value1">The Shipped value.</param>
        /// <param name="value2">The Scrap value.</param>
        /// <param name="value3">The Received value</param>
        public DataPoint(int id, string category, string item, string country, string state, decimal currency,
            double value1, double value2, double value3)
        {
            this.ID = id;
            this.Category = category;
            this.Item = item;
            this.Country = country;
            this.State = state;
            this.Currency = currency;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the ID values from item source.
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Gets or sets the country values from item source.
        /// </summary>
        public string Country { get; private set; }
        /// <summary>
        /// Gets or sets the state values from item source.
        /// </summary>
        public string State { get; private set; }
        /// <summary>
        /// Gets or sets the category values from item source.
        /// </summary>
        public string Category { get; private set; }
        /// <summary>
        /// Gets or sets the item values from item source.
        /// </summary>
        public string Item { get; private set; }
        /// <summary>
        /// Gets or sets the currency values from item source.
        /// </summary>
        public decimal Currency { get; private set; }
        /// <summary>
        /// Gets or sets the shipped values from item source.
        /// </summary>
        public double Value1 { get; private set; }
        /// <summary>
        /// Gets or sets the scrap values from item source.
        /// </summary>
        public double Value2 { get; private set; }
        /// <summary>
        /// Gets or sets the item values from item source.
        /// </summary>
        public double Value3 { get; private set; }
        #endregion
    }
}
