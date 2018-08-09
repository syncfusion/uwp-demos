using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the CustomerInfoViewModel
    /// </summary>
    class CustomerInfoViewModel : INotifyPropertyChanged, IDisposable
    {

        public CustomerInfoViewModel()
        {
            CustomerDetails = new CustomersInfoRepository().GetCustomerDetails(50);
            CustomerDetails1 = new CustomersInfoRepository().GetCustomerDetails(50);
        }

        private ObservableCollection<Customers> customerDetails;

        /// <summary>
        /// Gets or sets the CustomerDetails.
        /// </summary>
        /// <value>The CustomerDetails.</value>
        public ObservableCollection<Customers> CustomerDetails
        {
            get
            {
                return customerDetails;
            }
            set
            {
                customerDetails = value;
                OnPropertyChanged("CustomerDetails");
            }
        }

        private ObservableCollection<Customers> customerDetails1;
        /// <summary>
        /// Gets or sets the CustomerDetails.
        /// </summary>
        /// <value>The CustomerDetails.</value>
        public ObservableCollection<Customers> CustomerDetails1
        {
            get
            {
                return customerDetails1;
            }
            set
            {
                customerDetails1 = value;
                OnPropertyChanged("CustomerDetails1");
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
            CustomerDetails.Clear();
            CustomerDetails1.Clear();
        }
    }
}
