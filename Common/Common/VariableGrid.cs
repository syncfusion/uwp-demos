#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
