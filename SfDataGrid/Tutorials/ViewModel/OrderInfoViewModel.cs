using Common;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace DataGrid
{
    internal delegate void Toggled();
    /// <summary>
    /// This class represents the OrderInfoViewModel
    /// </summary>
    public class OrderInfoViewModel : NotificationObject, IDisposable
    {
        private bool canAllowSorting = true;

        public bool CanAllowSorting
        {
            get
            {
                return canAllowSorting;
            }
            set
            {
                canAllowSorting = value;
            }
        }

        private bool canAllowFiltering = true;

        public bool CanAllowFiltering
        {
            get
            {
                return canAllowFiltering;
            }
            set
            {
                canAllowFiltering = value;
            }
        }
        private bool canAllowBlankFilters = true;

        public bool CanAllowBlankFilters
        {
            get
            {
                return canAllowBlankFilters;
            }
            set
            {
                canAllowBlankFilters = value;
            }
        }
        private bool canImmediateUpdateColumnFilter;

        public bool CanImmediateUpdateColumnFilter
        {
            get
            {
                return canImmediateUpdateColumnFilter;
            }
            set
            {
                canImmediateUpdateColumnFilter = value;
            }
        }
        public Popup CustomChooser { get; set; }
        public OrderInfoViewModel()
        {   
            OrdersListDetails = new OrderInfoRepository().GetListOrdersDetails(2000);
            OrdersDetails = OrdersListDetails.Take(100).ToList<OrderInfo>();
#if !SFDATAGRID_STORE
            this.CustomChooser = new Popup();
            _comboBoxItemsSource = styleName.ToList();
            _pasteoption.Add(GridPasteOption.None);
            _pasteoption.Add(GridPasteOption.PasteData);
            _pasteoption.Add(GridPasteOption.ExcludeFirstLine);
            _copyoption.Add(GridCopyOption.None);
            _copyoption.Add(GridCopyOption.CopyData);
            _copyoption.Add(GridCopyOption.CutData);
            _copyoption.Add(GridCopyOption.IncludeHeaders);
            _copyoption.Add(GridCopyOption.IncludeFormat);
#endif
        }

 #if !SFDATAGRID_STORE
        private List<GridPasteOption> _pasteoption = new List<GridPasteOption>();
        private List<GridCopyOption> _copyoption = new List<GridCopyOption>();

        /// <summary>
        /// Gets or sets the CopyOption.
        /// </summary>
        /// <value>The CopyOption.</value>
        public List<GridCopyOption> CopyOption
        {
            get { return _copyoption; }
            set { _copyoption = value; RaisePropertyChanged("CopyOption"); }
        }

        /// <summary>
        /// Gets or sets the PasteOption.
        /// </summary>
        /// <value>The PasteOption.</value>
        public List<GridPasteOption> PasteOption
        {
            get { return _pasteoption; }
            set { _pasteoption = value; RaisePropertyChanged("PasteOption"); }
        }

        private bool showColumnChooser = true;

        internal Toggled toggled;

        private ICommand togglebuttonToggled;

        public ICommand TogglebuttonToggled
        {
            get { return new DelegateCommand(ToggledEvent, args => true); }
            set { togglebuttonToggled = value; RaisePropertyChanged("TogglebuttonToggled"); }
        }

        private void ToggledEvent(object pram)
        {
            if (pram != null)
            {
                this.ShowColumnChooser = (bool)pram;
                toggled();
            }
        }
        /// <summary>
        /// Gets or sets the ShowColumnChooser.
        /// </summary>
        /// <value>The ShowColumnChooser.</value>
        public bool ShowColumnChooser
        {
            get
            {
                return showColumnChooser;
            }
            set
            {
                showColumnChooser = value;
                RaisePropertyChanged("ShowColumnChooser");
            }
        }

        private bool useDefaultColumnChooser = true;

        /// <summary>
        /// Gets or sets the UseDefaultColumnChooser.
        /// </summary>
        /// <value>The UseDefaultColumnChooser.</value>
        public bool UseDefaultColumnChooser
        {
            get
            {
                return useDefaultColumnChooser;
            }
            set
            {
                useDefaultColumnChooser = value;
                RaisePropertyChanged("UseDefaultColumnChooser");
            }
        }

        private bool useCustomColumnChooser;

        /// <summary>
        /// Gets or sets the UseCustomColumnChooser.
        /// </summary>
        /// <value>The UseCustomColumnChooser.</value>
        public bool UseCustomColumnChooser
        {
            get
            {
                return useCustomColumnChooser;
            }
            set
            {
                useCustomColumnChooser = value;
                RaisePropertyChanged("UseCustomColumnChooser");
            }
        }

#region Delegate Command

        public DelegateCommand<object> _ColumnChooserCommand;

        /// <summary>
        /// Gets the column chooser command.
        /// </summary>
        /// <value>The column chooser command.</value>
        public DelegateCommand<object> ColumnChooserCommand
        {
            get { return _ColumnChooserCommand; }
            set
            {
                _ColumnChooserCommand = value;
                RaisePropertyChanged("ColumnChooserCommand");
            }
        }
#endregion

#endif
        private List<OrderInfo> _ordersListDetails;

        /// <summary>
        /// Gets or sets the orders details.
        /// </summary>
        /// <value>The orders details.</value>
        public List<OrderInfo> OrdersListDetails
        {
            get { return _ordersListDetails; }
            set { _ordersListDetails = value; }
        }

        private List<OrderInfo> _ordersDetails;

        /// <summary>
        /// Gets or sets the orders details.
        /// </summary>
        /// <value>The orders details.</value>
        public List<OrderInfo> OrdersDetails
        {
            get { return _ordersDetails; }
            set { _ordersDetails = value; }
        }
 #if !SFDATAGRID_STORE
        private List<string> _comboBoxItemsSource;

        /// <summary>
        /// Gets or sets the ComboBoxItemsSource.
        /// </summary>
        /// <value>The ComboBoxItemsSource.</value>
        public List<string> ComboBoxItemsSource
        {
            get { return _comboBoxItemsSource; }
            set { _comboBoxItemsSource = value; }
        }

        string[] styleName = new string[]
        {
            "Windows7",
            "Metro",
            "Blend",
            "GlassyGreen",
            "Office2007Black",
            "Office2007Blue",
            "Office2007Silver",
            "Office2010Black",
            "Office2010Blue",
            "Office2010Silver"
        };
#endif
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (OrdersDetails != null)
            {
                this.OrdersDetails.Clear();
            }
            if (this.OrdersListDetails != null)
            {
                this.OrdersListDetails.Clear();
            }
        }
    }
}
