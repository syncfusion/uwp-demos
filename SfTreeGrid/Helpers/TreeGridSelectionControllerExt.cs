using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.TreeGrid;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace TreeGrid
{
    using Syncfusion.UI.Xaml.Grid.Helpers;
    using Syncfusion.UI.Xaml.ScrollAxis;
    using Key = Windows.System.VirtualKey;
    /// <summary>
    /// This class represents the TreeGridSelectionControllerExt
    /// </summary>
    public class TreeGridSelectionControllerExt : TreeGridRowSelectionController
    {
        public TreeGridSelectionControllerExt(SfTreeGrid treeGrid) : base(treeGrid)
        {

        }

        protected override void ProcessKeyDown(KeyRoutedEventArgs args)
        {
            var currentKey = args.Key;
            if (TreeGrid.FlowDirection == FlowDirection.RightToLeft)
                ChangeFlowDirectionKey(ref currentKey);

            var previousCurrentCellIndex = this.CurrentCellManager.CurrentRowColumnIndex;
            var node = TreeGrid.GetNodeAtRowIndex(this.CurrentCellManager.CurrentRowColumnIndex.RowIndex);

            if (currentKey == Key.Left || currentKey == Key.Right)
            {
                if (this.CurrentCellManager.CurrentRowColumnIndex.RowIndex <= this.TreeGrid.GetHeaderIndex())
                    return;

                if (node != null)
                {
                    if (node.IsExpanded && currentKey == Key.Left)
                        TreeGrid.CollapseNode(node);
                    else if (!node.IsExpanded && currentKey == Key.Right)
                        TreeGrid.ExpandNode(node);
                }
                args.Handled = true;
                return;
            }
            else if (currentKey == Key.Space)
            {
                if (node != null)
                {
                    var isCheckedValue = node.IsChecked;
                    if (isCheckedValue == true)
                        //node.SetCheckedState(null);
                        node.SetCheckedState(false);
                    else if (isCheckedValue == null)
                        node.SetCheckedState(false);
                    else
                        node.SetCheckedState(true);
                }
                args.Handled = true;
                return;
            }
            base.ProcessKeyDown(args);
        }

        protected override void ProcessOnTapped(TappedRoutedEventArgs e, RowColumnIndex currentRowColumnIndex)
        {
            if (currentRowColumnIndex.RowIndex <= this.TreeGrid.GetHeaderIndex())
                return;

            var node = TreeGrid.GetNodeAtRowIndex(currentRowColumnIndex.RowIndex);
            if (node != null)
            {
                if (node.IsExpanded)
                    TreeGrid.CollapseNode(node);
                else if (!node.IsExpanded)
                    TreeGrid.ExpandNode(node);
            }
            base.ProcessOnTapped(e, currentRowColumnIndex);
        }
    }
}
