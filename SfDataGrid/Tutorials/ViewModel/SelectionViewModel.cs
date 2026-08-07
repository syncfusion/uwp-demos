using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the SelectionViewModel
    /// </summary>
    public class SelectionViewModel : NotificationObject,IDisposable
    {
        public SelectionViewModel()
        {
            ProductInfoRepository products = new ProductInfoRepository();
            ProductDetails = products.GetProductDetails(50);
        }
        private List<ProductInfo> _ProductDetails;

        /// <summary>
        /// Gets or sets the orders details.
        /// </summary>
        /// <value>The orders details.</value>
        public List<ProductInfo> ProductDetails
        {
            get { return _ProductDetails; }
            set { _ProductDetails = value; RaisePropertyChanged("ProductDetails"); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (ProductDetails != null)
            {
                ProductDetails.Clear();
            }
        }
    }
}
