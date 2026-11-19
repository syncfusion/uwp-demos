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
    /// This class represents the SalesInfoViewModel
    /// </summary>
    class SalesInfoViewModel : IDisposable
    {
        public SalesInfoViewModel()
        {
            _SalesDetails = new SalesInfoRepository().GetSalesDetailsByYear(5);
        }
        private ObservableCollection<SalesByYear> _SalesDetails = null;

        /// <summary>
        /// Gets or sets the DailySalesDetails.
        /// </summary>
        /// <value>The DailySalesDetails.</value>
        public ObservableCollection<SalesByYear> YearlySalesDetails
        {
            get
            {
                return _SalesDetails;
            }

        }

        private ObservableCollection<SalesByDate> _DailySalesDetails = null;

        /// <summary>
        /// Gets or sets the DailySalesDetails.
        /// </summary>
        /// <value>The DailySalesDetails.</value>
        public ObservableCollection<SalesByDate> DailySalesDetails
        {
            get
            {
                if (_DailySalesDetails == null)
                    return new SalesInfoRepository().GetSalesDetailsByDay(60);
                else
                    return _DailySalesDetails;
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (DailySalesDetails != null)
            {
                DailySalesDetails.Clear();
            }
            if (YearlySalesDetails != null)
            {
                YearlySalesDetails.Clear();
            }
        }
    }
}
