using System.Collections.Specialized;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syncfusion.Data;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Grid;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OnDemandPaging : SampleLayout
    {
        private OrderInfoRepository repository;
        private List<OrderInfo> source;
        public OnDemandPaging()
        {
            this.InitializeComponent();
            this.DataContext = new OrderInfoViewModel();
            repository= new OrderInfoRepository();
            source = this.repository.GetListOrdersDetails(1000);
            sfGrid.SortColumnsChanging += OnSortColumnsChanging;
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                this.sfDataPager.DisplayMode = Syncfusion.UI.Xaml.Controls.DataPager.PageDisplayMode.PreviousNextNumeric;
            }
        }

        /// <summary>
        /// Occurs when SortColumn is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSortColumnsChanging(object sender, Syncfusion.UI.Xaml.Grid.GridSortColumnsChangingEventArgs e)
        {
            this.sfDataPager.PagedSource.ResetCache();
            (this.sfDataPager.PagedSource as PagedCollectionView).ResetCacheForPage(this.sfDataPager.PageIndex);
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action== NotifyCollectionChangedAction.Replace)
            {
                var sortDesc = e.AddedItems[0];
                if (sortDesc.SortDirection == ListSortDirection.Ascending)
                {
                    source = source.OfQueryable().AsQueryable().OrderBy(sortDesc.ColumnName).Cast<OrderInfo>().ToList();
                }
                else
                {
                    source = source.OfQueryable().AsQueryable().OrderByDescending(sortDesc.ColumnName).Cast<OrderInfo>().ToList();
                }
		        this.sfDataPager.MoveToPage(this.sfDataPager.PageIndex);
            }
        }

        /// <summary>
        /// Occurs when the paging is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnDemandPageLoading(object sender, Syncfusion.UI.Xaml.Controls.DataPager.OnDemandLoadingEventArgs args)
        {
            sfDataPager.LoadDynamicItems(args.StartIndex,source.Skip(args.StartIndex).Take(args.PageSize));
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();
            sfGrid.SortColumnsChanging -= OnSortColumnsChanging;
            sfDataPager.OnDemandLoading -= OnDemandPageLoading;
            sfGrid.Dispose();
            sfGrid.ItemsSource = null;
            sfDataPager.Dispose();
            (this.DataContext as OrderInfoViewModel).Dispose();
            base.Dispose();
        }
    }
}
