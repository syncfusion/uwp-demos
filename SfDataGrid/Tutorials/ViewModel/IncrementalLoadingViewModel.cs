using Syncfusion.SampleBrowser;
using Syncfusion.SampleBrowser.UWP.SfDataGrid;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;

namespace DataGrid
{
    /// <summary>
    /// This class represents the IncrementalLoadingViewModel
    /// </summary>
    public class IncrementalLoadingViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Members

        NorthwindEntities northwindEntity;

        #endregion

        #region Ctor

        public IncrementalLoadingViewModel()
        {
            string uri = "http://services.odata.org/Northwind/Northwind.svc/";
            if (IsConnectedToInternet())
            {
                gridSource = new IncrementalList<Order>(LoadMoreItems) { MaxItemCount = 1000 };
                northwindEntity = new NorthwindEntities(new Uri(uri));
            }
            else
            {
                NoNetwork = true;
                IsBusy = false;
            }
        }

        #endregion

        #region Properties

        private IncrementalList<Order> gridSource;

        /// <summary>
        /// Gets or sets the GridSource.
        /// </summary>
        /// <value>The GridSource.</value>
        public IncrementalList<Order> GridSource
        {
            get { return gridSource; }
            set { gridSource = value; RaisePropertyChanged("GridSource"); }
        }

        private bool isBusy;

        /// <summary>
        /// Gets or sets the IsBusy.
        /// </summary>
        /// <value>The IsBusy.</value>
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; RaisePropertyChanged("IsBusy"); }
        }

        private bool noNetwork;

        /// <summary>
        /// Gets or sets the NoNetwork.
        /// </summary>
        /// <value>The NoNetwork.</value>
        public bool NoNetwork
        {
            get { return noNetwork; }
            set { noNetwork = value; RaisePropertyChanged("NoNetwork"); }
        }

        #endregion

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Methods

        async Task<IList<Order>> LoadMoreItems(CancellationToken c, uint count, int baseIndex)
        {
            IList<Order> list = null;
            IsBusy = true;

            await Task.Run(new Action(() =>
            {
                DataServiceQuery<Order> query = northwindEntity.Orders.Expand("Customer");
                query = query.Skip<Order>(baseIndex).Take<Order>(50) as DataServiceQuery<Order>;
                try
                {
                    IAsyncResult ar = query.BeginExecute(null, null);
                    var items = query.EndExecute(ar);
                    list = items.ToList();
                }
                catch(Exception)
                {

                }
            }));

                       IsBusy = false;
            return list;
        }

        public static bool IsConnectedToInternet()
        {
            ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (GridSource != null)
                GridSource.Clear();
        }

    }
}
