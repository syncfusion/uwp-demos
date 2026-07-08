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
    public sealed partial class ConditionalFormat : Page
    {
        #region Private Fields

        GridConditionalFormat conditionformat = new GridConditionalFormat();
        private string _formatingtype;
        private GridConditionType _type;

        #endregion

        #region Constructor

        public ConditionalFormat()
        {
            this.InitializeComponent();
            Loaded += ConditionalFormatting_Loaded;
        }

        #endregion

        #region Properties

        internal string FormatingType
        {
            get { return _formatingtype; }
            set { _formatingtype = value; }
        }

        internal GridConditionType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #endregion

        #region Events

        void ConditionalFormatting_Loaded(object sender, RoutedEventArgs e)
        {
            switch (Type)
            {
                case GridConditionType.GreaterThan:
                    Headertext.Text = "Format Cells that are Greater Than";
                    Andblock.Visibility = Visibility.Collapsed;
                    startvalue.Visibility = Visibility.Collapsed;

                    break;
                case GridConditionType.LessThan:
                    Headertext.Text = "Format Cells that are Greater Than";
                    Andblock.Visibility = Visibility.Collapsed;
                    startvalue.Visibility = Visibility.Collapsed;
                    break;
                case GridConditionType.Contains:
                    Headertext.Text = "Format Cells that Contains";
                    Andblock.Visibility = Visibility.Collapsed;
                    startvalue.Visibility = Visibility.Collapsed;
                    break;
                case GridConditionType.Equal:
                    Headertext.Text = "Format Cells that Equal ";
                    Andblock.Visibility = Visibility.Collapsed;
                    startvalue.Visibility = Visibility.Collapsed;
                    break;
                case GridConditionType.Between:
                    Headertext.Text = "Format Cells that are Between";
                    Andblock.Visibility = Visibility.Visible;
                    startvalue.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Submit(object sender, RoutedEventArgs e)
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
                    condition.Value1 = Endvalue.Text;
                    if (Type.Equals(GridConditionType.Between))
                        condition.Value2 = startvalue.Text;
                    if (Type.Equals(GridConditionType.Contains))
                        conditionformat.ConditionalFormatType = GridConditionalFormatType.SpecificText;
                    condition.ConditionType = Type;
                    conditionformat.Condition = condition;
                    cellgrid.Model[i, j].ConditionalFormats = new GridConditionalFormats();
                    cellgrid.Model[i, j].ConditionalFormats.Add(conditionformat);

                }
            }
            cellgrid.InvalidateCell(Range);
            startvalue.Text = "";
            Endvalue.Text = "";
            (this.Tag as Popup).IsOpen = false;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            (this.Tag as Popup).IsOpen = false;
        }

        private void Colorcomboselectionchanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
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
                    conditionformat.Style.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case 4:
                    conditionformat.Style.Background = new SolidColorBrush(Colors.White);
                    conditionformat.Style.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case 5:
                    break;
            }
        }

        #endregion
    }
}
