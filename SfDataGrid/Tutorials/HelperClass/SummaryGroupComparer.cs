using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Data;

namespace DataGrid
{
    /// <summary>
    /// This class represents the SumAggregateGroupComparer
    /// </summary>
    public class SumAggregateGroupComparer : IComparer<Group>, ISortDirection
    {
        public ListSortDirection SortDirection { get; set; }
        public int Compare(Group x, Group y)
        {
            //To handle the groups that are not in view
            if (x.ItemsCount == 0 && y.ItemsCount == 0)
                return 0;
            else if (x.ItemsCount == 0)
                return -1;
            else if (y.ItemsCount == 0)
                return 1;

            int cmp = 0;
            var xgroupSummarry = Convert.ToDouble((x as Group).GetSummaryValue(x.SummaryDetails.SummaryRow.SummaryColumns[0].MappingName, "Sum"));
            var ygroupSummarry = Convert.ToDouble((y as Group).GetSummaryValue(y.SummaryDetails.SummaryRow.SummaryColumns[0].MappingName, "Sum"));
            cmp = ((IComparable)xgroupSummarry).CompareTo(ygroupSummarry);
            if (this.SortDirection == ListSortDirection.Descending)
                cmp = -cmp;
            return cmp;
        }
    }

    /// <summary>
    /// This class represents the AvgAggregateGroupComparer
    /// </summary>
    public class AvgAggregateGroupComparer : IComparer<Group>, ISortDirection
    {
        public ListSortDirection SortDirection { get; set; }
        public int Compare(Group x, Group y)
        {
            //To handle the groups that are not in view
            if (x.ItemsCount == 0 && y.ItemsCount == 0)
                return 0;
            else if (x.ItemsCount == 0)
                return -1;
            else if (y.ItemsCount == 0)
                return 1;

            int cmp = 0;
            var xgroupSummarry = Convert.ToDouble((x as Group).GetSummaryValue(x.SummaryDetails.SummaryRow.SummaryColumns[1].MappingName, "Average"));
            var ygroupSummarry = Convert.ToDouble((y as Group).GetSummaryValue(y.SummaryDetails.SummaryRow.SummaryColumns[1].MappingName, "Average"));
            cmp = ((IComparable)xgroupSummarry).CompareTo(ygroupSummarry);
            if (this.SortDirection == ListSortDirection.Descending)
                cmp = -cmp;
            return cmp;
        }
    }
}
