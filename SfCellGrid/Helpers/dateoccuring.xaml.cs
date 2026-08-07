using Syncfusion.UI.Xaml.CellGrid;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace tabcontrol
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class dateoccuring : Page
    {

        #region Private Fields

        GridConditionalFormat conditionformat = new GridConditionalFormat();

        #endregion

        #region Constructor

        public dateoccuring()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events
        private void submit(object sender, RoutedEventArgs e)
        {
            var cellgrid = this.DataContext as SfCellGrid;
            GridRangeInfo Range = cellgrid.SelectedRanges.ActiveRange;
            for (int i = Range.Top; i <= Range.Bottom; i++)
            {
                for (int j = Range.Left; j <= Range.Right; j++)
                {
                    var condition = new GridCondition();
                    condition.ColumnIndex = j;
                    condition.RowIndex = i;
                    //conditionformat.Condition = condition;
                    conditionformat.ConditionalFormatType = GridConditionalFormatType.DatesOccurring;
                    //conditionformat.ConditionalFormatType = GridConditionalFormatType.DatesOccurring;
                    cellgrid.Model[i, j].ConditionalFormats = new GridConditionalFormats();
                    cellgrid.Model[i, j].ConditionalFormats.Add(conditionformat);

                    cellgrid.InvalidateCell(i, j);
                }
            }
            (this.Tag as Popup).IsOpen = false;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            (this.Tag as Popup).IsOpen = false;
        }

        private void colorcomboselectionchanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs  e)
        {
            var index = colorcombo.SelectedIndex;
            switch (index)
            {
                case 0:
                    conditionformat.Style.Background = new SolidColorBrush(Colors.Tomato);
                    conditionformat.Style.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case 1:
                    conditionformat.Style.Background = new SolidColorBrush(Colors.LightYellow);
                    conditionformat.Style.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;
                case 2:
                    conditionformat.Style.Background = new SolidColorBrush(Colors.LightGreen);
                    conditionformat.Style.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case 3:
                    conditionformat.Style.Background = new SolidColorBrush(Colors.Tomato);
                    break;
                case 4:
                    conditionformat.Style.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case 5:

                    break;
            }
        }

        private void days_SelectionChanged_1(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var index = days.SelectedIndex;

            switch (index)
            {
                case 0:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.Yesterday;
                    break;
                case 1:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.Today;
                    break;
                case 2:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.Tomorrow;
                    break;
                case 3:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.InLast7Days;
                    break;
                case 4:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.LastWeek;
                    break;
                case 5:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.ThisWeek;
                    break;
                case 6:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.NextWeek;
                    break;
                case 7:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.LastMonth;
                    break;
                case 8:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.ThisMonth;
                    break;
                case 9:
                    conditionformat.Condition.TimePeriodType = GridTimePeriodType.NextMonth;
                    break;

            }
        }

        #endregion
    }
}
