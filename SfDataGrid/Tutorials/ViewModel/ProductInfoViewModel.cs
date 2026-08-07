using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the ProductInfoViewModel
    /// </summary>
    public class ProductInfoViewModel : NotificationObject, IDisposable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        public ProductInfoViewModel()
        {
            this.PopulateData();
        }

        #region Private Memebers

        private double _Average;
        private int _Count;
        private int _NumCount;
        private double _Min;
        private double _Max;
        private double _Sum;
        private ObservableCollection<ProductInfoModel> productList = new ObservableCollection<ProductInfoModel>();

        #endregion

        #region Public Members

        /// <summary>
        /// Gets or sets the Average value of the cells.
        /// </summary>
        /// <value>The Average Value.</value>
        public double Average
        {
            get { return _Average; }
            set { _Average = value; RaisePropertyChanged("Average"); }
        }

        /// <summary>
        /// Gets or sets the Count of the cells.
        /// </summary>
        /// <value>The Count.</value>
        public int Count
        {
            get { return _Count; }
            set { _Count = value; RaisePropertyChanged("Count"); }
        }

        /// <summary>
        /// Gets or sets the Numerical Count of the cells.
        /// </summary>
        /// <value>The Numerical Count.</value>
        public int NumCount
        {
            get { return _NumCount; }
            set { _NumCount = value; RaisePropertyChanged("NumCount"); }
        }

        /// <summary>
        /// Gets or sets the Minimum value of the cells.
        /// </summary>
        /// <value>The Minimum Value.</value>
        public double Min
        {
            get { return _Min; }
            set { _Min = value; RaisePropertyChanged("Min"); }
        }

        /// <summary>
        /// Gets or sets the Maximum value of the cells.
        /// </summary>
        /// <value>The Maximum Value.</value>
        public double Max
        {
            get { return _Max; }
            set { _Max = value; RaisePropertyChanged("Max"); }
        }

        /// <summary>
        /// Gets or sets the Sum of the cells.
        /// </summary>
        /// <value>The Sum.</value>
        public double Sum
        {
            get { return _Sum; }
            set { _Sum = value; RaisePropertyChanged("Sum"); }
        }

        /// <summary>
        /// Gets or sets the product list.
        /// </summary>
        /// <value>The product list.</value>
        public ObservableCollection<ProductInfoModel> ProductList
        {
            get { return productList; }
            set { productList = value; RaisePropertyChanged("ProductList"); }
        }

        #endregion

        /// <summary>
        /// Collection of ProductNames
        /// </summary>
        string[] productName = new string[]
        {
            "Alice Mutton",	
            "NuNuCa Nuß-Nougat-Creme",	
            "Boston Crab Meat",	
            "Raclette Courdavault",	
            "Wimmers gute Semmelknödel",
            "Gorgonzola Telino",	
            "Chartreuse verte",	
            "Fløtemysost",	
            "Carnarvon Tigers",	
            "Thüringer Rostbratwurst",	
            "Vegie-spread",	
            "Tarte au sucre",	
            "Konbu",	
            "Valkoinen suklaa",	
            "Queso Manchego La Pastora",	
            "Perth Pasties",	
            "Vegie-spread",	
            "Tofu",	
            "Sir Rodney's Scone 7",	
            "Manjimup Dried Apples"
        };

        /// <summary>
        /// Populates the data.
        /// </summary>
        private void PopulateData()
        {
            Random r = new Random();
            for (int i = 0; i < 70; i++)
            {
                ProductInfoModel productInfo = new ProductInfoModel();
                productInfo.ProductName = productName[r.Next(20)];
                productInfo.Year2008 = Math.Round(r.Next(2000) + r.NextDouble(), 2);
                productInfo.Year2009 = Math.Round(r.Next(250) + r.NextDouble(), 2);
                productInfo.Year2010 = Math.Round(r.Next(300) + r.NextDouble(), 2);
                productInfo.Year2011 = Math.Round(r.Next(550) + r.NextDouble(), 2);
                productInfo.Year2012 = Math.Round(r.Next(700) + r.NextDouble(), 2);
                productInfo.Year2013 = Math.Round(r.Next(1000) + r.NextDouble(), 2);
                productInfo.TotalSales = productInfo.Year2008 + productInfo.Year2009 + productInfo.Year2010 + productInfo.Year2011 + productInfo.Year2012 + productInfo.Year2013;
                this.productList.Add(productInfo);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (ProductList != null)
            {
                this.ProductList.Clear();
            }
         
        }
    }
}
