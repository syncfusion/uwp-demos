using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DataGrid
{
    public class ContextMenuViewModel : MenuFlyoutItem, IDisposable , INotifyPropertyChanged
    {
        private List<OrderInfo> _ordersListDetails;

        /// <summary>
        /// Gets or sets the orders details.
        /// </summary>
        /// <value>The orders details.</value>
        public List<OrderInfo> OrdersListDetails
        {
            get
            {
                return _ordersListDetails;
            }
            set
            {
                _ordersListDetails = value;
                OnPropertyChanged("OrdersListDetails");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenuViewModel"/> class.
        /// </summary>
        public ContextMenuViewModel()
        {
            _gridCutCommand = new DelegateCommand(Cut);
            _gridCopyCommand = new DelegateCommand(Copy);
            _gridPasteCommand = new DelegateCommand(Paste);
            _gridDeleteCommand = new DelegateCommand(Delete);
            _expandAllCommand = new DelegateCommand(ExpandAll);
            _collapseAllCommand = new DelegateCommand(CollapseAll);
            _sortAscendingCommand = new DelegateCommand(SortAscending);
            _sortDescendingCommand = new DelegateCommand(SortDescending);
            _clearSummaryCommand = new DelegateCommand(ClearSummary);
            _groupColumnCommand = new DelegateCommand(GroupColumn);
            _showGroupDropAreaCommand = new DelegateCommand(ShowGroupDropArea);
            _clearSortingCommand = new DelegateCommand(ClearSorting);
            _clearFilteringCommand = new DelegateCommand(ClearFiltering);
            _clearGroupingCommand = new DelegateCommand(ClearGrouping);
            this.GenerateOrders();
        }

        #region Orders

        /// <summary>
        /// Creates the order details data.
        /// </summary>
        private void GenerateOrders()
        {
            OrdersListDetails = new OrderInfoRepository().GetListOrdersDetails(200);
        }

        #endregion Orders

        #region Cut

        private DelegateCommand _gridCutCommand;

        /// <summary>
        /// Gets and sets the cut command
        /// </summary>
        public DelegateCommand GridCutCommand
        {
            get { return _gridCutCommand; }
            set { _gridCutCommand = value; }
        }

        /// <summary>
        /// Cut the specified record.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridRecordContextMenuInfo"/>.
        /// </param>
        public void Cut(object param)
        {
            if (param is GridRecordContextMenuInfo)
            {
                var grid = (param as GridRecordContextMenuInfo).DataGrid;
                var copypasteoption = grid.GridCopyOption;
                grid.GridCopyOption = GridCopyOption.CutData;
                grid.GridCopyPaste.Cut();
                grid.GridCopyOption = copypasteoption;
            }
        }

        #endregion Cut

        #region Copy

        private DelegateCommand _gridCopyCommand;

        /// <summary>
        /// Gets and sets the copy command
        /// </summary>
        public DelegateCommand GridCopyCommand
        {
            get { return _gridCopyCommand; }
            set { _gridCopyCommand = value; }
        }

        /// <summary>
        /// Copy the specified record.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridRecordContextMenuInfo"/>.
        /// </param>
        public void Copy(object param)
        {
            if (param is GridRecordContextMenuInfo)
            {
                var grid = (param as GridRecordContextMenuInfo).DataGrid;
                grid.GridCopyPaste.Copy();
            }
        }

        #endregion Copy

        #region Paste

        private DelegateCommand _gridPasteCommand;

        /// <summary>
        /// Gets and sets the paste command
        /// </summary>
        public DelegateCommand GridPasteCommand
        {
            get
            {
                if (_gridPasteCommand != null)
                    _gridPasteCommand = new DelegateCommand(Paste, CanPaste);

                return _gridPasteCommand;
            }
            set { _gridPasteCommand = value; }
        }

        /// <summary>
        /// Paste the specified record.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridRecordContextMenuInfo"/>.
        /// </param>
        public void Paste(object param)
        {
            if (param is GridRecordContextMenuInfo)
            {
                var grid = (param as GridRecordContextMenuInfo).DataGrid;
                grid.GridCopyPaste.Paste();
            }
        }

        /// <summary>
        /// Determines whether the paste command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridRecordContextMenuInfo"/>
        /// </param>
        /// <returns>
        /// <b>true</b> if the clipboard has content, Otherwise <b>false</b>
        /// </returns>
        public bool CanPaste(object param)
        {
            var content = Clipboard.GetContent();
            if (content != null && (content.Contains(StandardDataFormats.Text)))
                return true;
            return false;
        }

        #endregion Paste

        #region Delete

        private DelegateCommand _gridDeleteCommand;

        /// <summary>
        /// Gets and sets the expand command
        /// </summary>
        public DelegateCommand DeleteCommand
        {
            get
            {
                if (_gridDeleteCommand != null)
                    _gridDeleteCommand = new DelegateCommand(Delete);
                return _gridDeleteCommand;
            }
            set { _gridDeleteCommand = value; }
        }

        /// <summary>
        /// Expand the specified record in group caption menu.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridRecordContextMenuInfo"/>.
        /// </param>
        private void Delete(object param)
        {
            if (param is GridRecordContextMenuInfo)
            {
                var grid = (param as GridRecordContextMenuInfo).DataGrid;
                var record = (param as GridRecordContextMenuInfo).Record as OrderInfo;
                if (record != null)
                    (grid.DataContext as ContextMenuViewModel).OrdersListDetails.Remove(record);
                grid.View.Refresh();
             
            }
        }

        #endregion Delete

        #region ExpandAll

        private DelegateCommand _expandAllCommand;

        /// <summary>
        /// Gets and sets the expandall command
        /// </summary>
        public DelegateCommand ExpandAllCommand
        {
            get
            {
                if (_expandAllCommand != null)
                    _expandAllCommand = new DelegateCommand(ExpandAll, CanExpandAll);
                return _expandAllCommand;
            }
            set { _expandAllCommand = value; }
        }

        /// <summary>
        /// Expand all groups in datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridContextMenuInfo"/>.
        /// </param>
        public void ExpandAll(object param)
        {
            if (param is Syncfusion.UI.Xaml.Grid.GridContextMenuInfo)
            {
                var grid = (param as Syncfusion.UI.Xaml.Grid.GridContextMenuInfo).DataGrid;
                grid.ExpandAllGroup();
            }
        }

        /// <summary>
        /// Determines whether the Expand all command can be executed..
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridContextMenuInfo"/>.
        /// </param>
        /// <returns>
        /// <b>true</b> if the Specified datagrid can expand, Otherwise <b>false</b>
        /// </returns>
        public bool CanExpandAll(object param)
        {
            if (param is Syncfusion.UI.Xaml.Grid.GridContextMenuInfo)
            {
                var grid = (param as Syncfusion.UI.Xaml.Grid.GridContextMenuInfo).DataGrid;
                if (grid.View.TopLevelGroup != null && grid.View.TopLevelGroup.Groups.Count > 0)
                    return grid.View.TopLevelGroup.Groups.Any(x => !x.IsExpanded);
            }
            return false;
        }

        #endregion ExpandAll

        #region CollapseAll

        private DelegateCommand _collapseAllCommand;

        /// <summary>
        /// Gets and sets the remove command
        /// </summary>
        public DelegateCommand CollapseAllCommand
        {
            get
            {
                if (_collapseAllCommand != null)
                    _collapseAllCommand = new DelegateCommand(CollapseAll, CanCollapseAll);

                return _collapseAllCommand;
            }
            set { _collapseAllCommand = value; }
        }

        /// <summary>
        /// Collapse all groups in datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridContextMenuInfo"/>.
        /// </param>
        public void CollapseAll(object param)
        {
            if (param is GridContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                grid.CollapseAllGroup();
            }
        }

        /// <summary>
        /// Determines whether the collapse all command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridContextMenuInfo"/>.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified datagrid can collapse,Otherwise <b>false</b>
        /// </returns>
        private bool CanCollapseAll(object param)
        {
            if (param is Syncfusion.UI.Xaml.Grid.GridContextMenuInfo)
            {
                var grid = (param as Syncfusion.UI.Xaml.Grid.GridContextMenuInfo).DataGrid;
                if (grid.View.TopLevelGroup != null && grid.View.TopLevelGroup.Groups.Count > 0)
                    return grid.View.TopLevelGroup.Groups.Any(x => x.IsExpanded);
            }
            return false;
        }

        #endregion CollapseAll

        #region SortAscending

        private DelegateCommand _sortAscendingCommand;

        /// <summary>
        /// Gets and sets the sort ascending command
        /// </summary>
        public DelegateCommand SortAscendingCommand
        {
            get
            {
                if (_sortAscendingCommand != null)
                    _sortAscendingCommand = new DelegateCommand(SortAscending, CanSortAscending);

                return _sortAscendingCommand;
            }
            set { _sortAscendingCommand = value; }
        }

        /// <summary>
        /// Sort the specific column in ascending order.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        private void SortAscending(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                var column = (param as GridColumnContextMenuInfo).Column;
                grid.SortColumnDescriptions.Clear();
                grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Ascending });
            }
        }

        /// <summary>
        /// Determines whether the sort ascending command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specific column can be sort in ascending order, Otherwise<b>false</b>
        /// </returns>
        private static bool CanSortAscending(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                var column = (param as GridColumnContextMenuInfo).Column;
                var sortColumn = grid.SortColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName);
                if (sortColumn != null)
                {
                    if ((sortColumn as SortColumnDescription).SortDirection == ListSortDirection.Ascending)
                        return false;
                }
                return grid.AllowSorting;
            }
            return false;
        }

        #endregion SortAscending

        #region SortDescending

        private DelegateCommand _sortDescendingCommand;

        /// <summary>
        /// Gets and sets the sort descending command
        /// </summary>
        public DelegateCommand SortDescendingCommand
        {
            get
            {
                if (_sortDescendingCommand != null)
                    _sortDescendingCommand = new DelegateCommand(SortDescending, CanSortDescending);

                return _sortDescendingCommand;
            }
            set { _sortDescendingCommand = value; }
        }

        /// <summary>
        /// Sort the specified column in descending order.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        private void SortDescending(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                var column = (param as GridColumnContextMenuInfo).Column;
                grid.SortColumnDescriptions.Clear();
                grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Descending });
            }
        }

        /// <summary>
        /// Determines whether the sort descending command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column can be sort in descending order, Otherwise<b>false</b>
        /// </returns>
        private bool CanSortDescending(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                var column = (param as GridColumnContextMenuInfo).Column;
                var sortColumn = grid.SortColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName);
                if (sortColumn != null)
                {
                    if ((sortColumn as SortColumnDescription).SortDirection == ListSortDirection.Descending)
                        return false;
                }
                return grid.AllowSorting;
            }
            return false;
        }

        #endregion SortDescending

        #region clearSummary

        private DelegateCommand _clearSummaryCommand;

        /// <summary>
        /// Gets and sets the clear summary command
        /// </summary>
        public DelegateCommand ClearSummaryCommand
        {
            get { return _clearSummaryCommand; }
            set { _clearSummaryCommand = value; }
        }

        /// <summary>
        /// Clears group summary from each group.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridRecordContextMenuInfo"/>.
        /// </param>
        private void ClearSummary(object param)
        {
            if (param is GridRecordContextMenuInfo)
            {
                var grid = (param as GridRecordContextMenuInfo).DataGrid;
                if (grid.GroupSummaryRows.Any())
                    grid.GroupSummaryRows.Clear();
            }
        }

        #endregion clearSummary

        #region GroupColumn

        private DelegateCommand _groupColumnCommand;

        /// <summary>
        /// Gets and sets the group column command
        /// </summary>
        public DelegateCommand GroupColumnCommand
        {
            get
            {
                if (_groupColumnCommand != null)
                    _groupColumnCommand = new DelegateCommand(GroupColumn, CanGroupColumn);
                return _groupColumnCommand;
            }
            set
            {
                _groupColumnCommand = value;
            }
        }

        /// <summary>
        /// Determines whether the group column command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column can group, Otherwise<b>false</b>
        /// </returns>
        public bool CanGroupColumn(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridColumnContextMenuInfo).DataGrid;
                var column = (param as GridColumnContextMenuInfo).Column;
                bool canGroup = false;
                if (!grid.GroupColumnDescriptions.Any(x => x.ColumnName == column.MappingName))
                {
                    var groupcolumn = column.ReadLocalValue(GridColumn.AllowGroupingProperty);
                    if (grid.AllowGrouping)
                        canGroup = true;
                    if (groupcolumn != DependencyProperty.UnsetValue || canGroup)
                        canGroup = column.AllowGrouping;
                }
                return canGroup;
            }
            return false;
        }

        /// <summary>
        /// Group the specified column.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        public void GroupColumn(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridColumnContextMenuInfo).DataGrid;
                var column = (param as GridColumnContextMenuInfo).Column;
                if (!grid.GroupColumnDescriptions.Any(x => x.ColumnName == column.MappingName))
                    grid.GroupColumnDescriptions.Add(new GroupColumnDescription() { ColumnName = column.MappingName });
            }
        }

        #endregion GroupColumn

        #region ShowGroupDropArea

        private DelegateCommand _showGroupDropAreaCommand;

        /// <summary>
        /// Gets and sets the show group drop area command
        /// </summary>
        public DelegateCommand ShowGroupDropAreaCommand
        {
            get { return _showGroupDropAreaCommand; }
            set { _showGroupDropAreaCommand = value; }
        }

        /// <summary>
        /// Show group drop area of the datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        private void ShowGroupDropArea(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridColumnContextMenuInfo).DataGrid;
                grid.IsGroupDropAreaExpanded = !grid.IsGroupDropAreaExpanded;
            }
        }

        #endregion ShowGroupDropArea

        #region ClearSorting

        private DelegateCommand _clearSortingCommand;

        /// <summary>
        /// Gets and sets the clear sorting command
        /// </summary>
        public DelegateCommand ClearSortingCommand
        {
            get
            {
                if (_clearSortingCommand != null)
                    _clearSortingCommand = new DelegateCommand(ClearSorting, CanClearSorting);
                return _clearSortingCommand;
            }
            set
            {
                _groupColumnCommand = value;
            }
        }

        /// <summary>
        /// Clears sorting from specified column.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        private void ClearSorting(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                var column = (param as GridColumnContextMenuInfo).Column;
                if (grid.SortColumnDescriptions.Any(x => x.ColumnName == column.MappingName))
                    grid.SortColumnDescriptions.Remove(grid.SortColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName));
            }
        }

        /// <summary>
        /// Determines whether the clear sorting command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column sorted, Otherwise<b>false</b>
        /// </returns>
        private bool CanClearSorting(object param)
        {
            if (param == null)
                return false;

            var grid = (param as GridColumnContextMenuInfo).DataGrid;
            var column = (param as GridColumnContextMenuInfo).Column;
            return grid.SortColumnDescriptions.Any(x => x.ColumnName == column.MappingName);
        }

        #endregion ClearSorting

        #region ClearFiltering

        private DelegateCommand _clearFilteringCommand;

        /// <summary>
        /// Gets and sets the clear filtering command
        /// </summary>
        public DelegateCommand ClearFilteringCommand
        {
            get
            {
                if (_clearFilteringCommand != null)
                    _clearFilteringCommand = new DelegateCommand(ClearFiltering, CanClearFiltering);
                return _clearFilteringCommand;
            }
            set
            {
                _clearFilteringCommand = value;
            }
        }

        /// <summary>
        /// Clears filter from specified column.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        private void ClearFiltering(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var column = (param as GridColumnContextMenuInfo).Column;
                if (column.FilterPredicates.Any())
                    column.FilterPredicates.Clear();
            }
        }

        /// <summary>
        /// Determines whether the clear filtering command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridColumnContextMenuInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column filtered, Otherwise<b>false</b>
        /// </returns>
        private bool CanClearFiltering(object param)
        {
            if (param is GridColumnContextMenuInfo)
            {
                var column = (param as GridColumnContextMenuInfo).Column;
                return column.FilterPredicates.Any();
            }
            return false;
        }

        #endregion ClearFiltering

        #region ClearGroups

        private DelegateCommand _clearGroupingCommand;

        /// <summary>
        /// Gets and sets the clear groups command
        /// </summary>
        public DelegateCommand ClearGroupingCommand
        {
            get
            {
                if (_clearGroupingCommand != null)
                    _clearGroupingCommand = new DelegateCommand(ClearGrouping, CanClearGrouping);
                return _clearGroupingCommand;
            }
            set
            {
                _clearGroupingCommand = value;
            }
        }

        /// <summary>
        /// Clear all groups from datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridContextMenuInfo"/>.
        /// </param>
        private void ClearGrouping(object param)
        {
            if (param is GridContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                grid.GroupColumnDescriptions.Clear();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.Grid.GridContextMenuInfo"/>.
        /// </param>
        /// <returns></returns>
        private bool CanClearGrouping(object param)
        {
            if (param is GridContextMenuInfo)
            {
                var grid = (param as GridContextMenuInfo).DataGrid;
                return grid.GroupColumnDescriptions.Any();
            }
            return false;
        }

        #endregion ClearGroups

        #region Dispose method

        /// <summary>
        /// Dispose the resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposing)
        {
            GridCutCommand = null;
            GridCopyCommand = null;
            GridPasteCommand = null;
            DeleteCommand = null;
            ExpandAllCommand = null;
            CollapseAllCommand = null;
            SortAscendingCommand = null;
            SortDescendingCommand = null;
            ClearSummaryCommand = null;
            GroupColumnCommand = null;
            ShowGroupDropAreaCommand = null;
            ClearSortingCommand = null;
            ClearFilteringCommand = null;
            ClearGroupingCommand = null;
            if (OrdersListDetails != null)
                OrdersListDetails = null;
        }

        #endregion Dispose method

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Members
    }

}