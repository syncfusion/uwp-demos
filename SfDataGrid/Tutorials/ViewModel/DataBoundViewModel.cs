using Syncfusion.UI.Xaml.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace DataGrid
{
    /// <summary>
    /// This class represents the DataBoundViewModel
    /// </summary>
    public class DataBoundViewModel : INotifyPropertyChanged, IDisposable
    {

        public DataBoundViewModel()
        {

        }

        private List<OrderInfo> _ordersListDetails = null;

        /// <summary>
        /// Gets or sets the OrderListDetails.
        /// </summary>
        /// <value>The OrderListDetails.</value>
        public List<OrderInfo> OrdersListDetails
        {
            get
            {
                if (_ordersListDetails == null)
                    return new OrderInfoViewModel().OrdersListDetails;
                else
                    return _ordersListDetails;
            }
        }

        private ObservableCollection<Employees> employeeDetails = null;

        /// <summary>
        /// Gets or sets the EmployeeDetails.
        /// </summary>
        /// <value>The EmployeeDetails.</value>
        public ObservableCollection<Employees> EmployeeDetails
        {
            get
            {
                if (employeeDetails == null)
                    return new EmployeeInfoViewModel().EmployeeDetails;
                else
                    return employeeDetails;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (OrdersListDetails != null)
            {
                OrdersListDetails.Clear();
            }
            if (EmployeeDetails != null)
            {
                EmployeeDetails.Clear();
            }
        }
    }
}
