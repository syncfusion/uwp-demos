using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Syncfusion.SampleBrowser.UWP.SfDataGrid;

namespace DataGrid
{
    /// <summary>
    /// This class represents the StyleSelector for TableSummary
    /// </summary>
    class TableSummaryStyleSelectorsforConditionalFormating : StyleSelector
    {
        /// <summary>
        /// Converts the SummaryRow into Style
        /// </summary>
        protected override Windows.UI.Xaml.Style SelectStyleCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            if ((item as SummaryRecordEntry).SummaryRow.ShowSummaryInRow)
            {
                var res = new DataGrid.ConditionalFormatting();
                var col = res.Resources["tableSummaryCell"] as Style;
                if (col != null)
                    return col;
               
            }
            else
            {
                var res = new DataGrid.ConditionalFormatting();
                var col = res.Resources["normaltableSummaryCell"] as Style;
                if (col != null)
                    return col;
              
            }
            return base.SelectStyleCore(item, container);
        }
    }
#if !SFDATAGRID_STORE
    /// <summary>
    /// This class represents the StyleSelector for TableSummary
    /// </summary>
    class TableSummaryStyleSelectorsforSummaries : StyleSelector
    {
        /// <summary>
        /// Converts the SummaryRow into Style
        /// </summary>
        protected override Windows.UI.Xaml.Style SelectStyleCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            if ((item as SummaryRecordEntry).SummaryRow.ShowSummaryInRow)
            {
                var res = new DataGrid.Summaries();
                var style = res.Resources["tableSummaryCell"] as Style;
                if (style != null)
                    return style;

            }
            else
            {
                var res = new DataGrid.Summaries();
                var style = res.Resources["normaltableSummaryCell"] as Style;
                if (style != null)
                    return style;

            }
            return base.SelectStyleCore(item, container);
        }
    }
    /// <summary>
    /// This class represents the StyleSelector for GroupSummary
    /// </summary>
    class GroupSummaryStleSelector : StyleSelector
    {
        /// <summary>
        /// Converts the SummaryRow into Style
        /// </summary>
        protected override Windows.UI.Xaml.Style SelectStyleCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            if ((item as SummaryRecordEntry).SummaryRow.ShowSummaryInRow)
            {
                var res = new DataGrid.Summaries();
                var style = res.Resources["groupSummaryCell"] as Style;
                if (style != null)
                    return style;
            }
            else
            {
                var res = new DataGrid.Summaries();
                var style = res.Resources["normalgroupSummaryCell"] as Style;
                if (style != null)
                    return style;
            }
            return base.SelectStyleCore(item, container);
        }
    }        
#endif

}
