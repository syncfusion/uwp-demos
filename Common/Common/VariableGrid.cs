using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Common
{
    public class VariableGrid : GridView
    {
        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            ITileItem tile = item as ITileItem;
            if (tile != null)
            {
                GridViewItem griditem = element as GridViewItem;
                if (griditem != null)
                {
                    VariableSizedWrapGrid.SetColumnSpan(griditem, tile.ColumnSpan);
                    VariableSizedWrapGrid.SetRowSpan(griditem, tile.RowSpan);
                }
            }
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
