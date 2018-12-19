using Common;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class ComboBoxColumn : SampleLayout
    {
        public ComboBoxColumn()
        {
            this.InitializeComponent();
            var dataContext = new ViewModel();
            this.DataContext = dataContext;
        }
        private void CurrentCellEndEditEventAction(object sender, CurrentCellEndEditEventArgs e)
        {
            var sfDataGrid = sender as SfDataGrid;
            if (sfDataGrid.CurrentColumn.MappingName != "ShipCountry")
                return;
            var datarow = this.sfdatagrid.RowGenerator.Items.FirstOrDefault(dr => dr.RowIndex == e.RowColumnIndex.RowIndex);
            datarow.Element.DataContext = null;
            this.sfdatagrid.UpdateDataRow(e.RowColumnIndex.RowIndex);
        }
    }
    public class CustomSelector : IItemsSourceSelector
    {
        public IEnumerable GetItemsSource(object record, object dataContext)
        {
            if (record == null)
                return null;

            var orderinfo = record as OrderDetails;
            var countryName = orderinfo.ShipCountry;

            var viewModel = dataContext as ViewModel;

            //Returns Shipcity collection based on CountryName.
            if (viewModel.ShipCities.ContainsKey(countryName))
            {
                ObservableCollection<ShipCityDetails> shipcities = null;
                viewModel.ShipCities.TryGetValue(countryName, out shipcities);
                return shipcities.ToList();
            }
            return null;
        }
    }
}
