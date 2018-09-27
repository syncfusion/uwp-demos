using System;
using System.Collections.Generic;

namespace MasterDetailsViewDemo
{
    /// <summary>
    /// This class represents the DetailsViewViewModel
    /// </summary>
    public class DetailsViewViewModel : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        public DetailsViewViewModel()
        {
            OrderInfoRepository order = new OrderInfoRepository();
            OrdersDetails = order.GetOrdersDetails(100);
        }

        public List<OrderInfo> _ordersDetails;

        /// <summary>
        /// Gets or sets the orders details.
        /// </summary>
        /// <value>The orders details.</value>
        public List<OrderInfo> OrdersDetails
        {
            get{ return _ordersDetails; }
            set{ _ordersDetails = value;}
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (OrdersDetails != null)
                OrdersDetails.Clear();
        }
    }
}
