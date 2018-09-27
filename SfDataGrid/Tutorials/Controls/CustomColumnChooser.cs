using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace DataGrid
{
    /// <summary>
    /// This class represents the CustomColumnChooser
    /// </summary>
    public sealed class CustomColumnChooser : Control
    {
        #region Field
        SfDataGrid dataGrid;
        bool isPointerPressed;
        Point pointerPressedPoint;
        #endregion

        public CustomColumnChooser()
        {
            this.DefaultStyleKey = typeof(CustomColumnChooser);
        }
        public CustomColumnChooser(SfDataGrid dataGrid, CustomColumnChooserViewModel viewModel, ObservableCollection<ColumnChooserItems> totalColumns)
        {
            this.DefaultStyleKey = typeof(CustomColumnChooser);
            this.DataContext = viewModel;
            this.dataGrid = dataGrid;
            var res = new DataGrid.ColumnChooserDemo();
            this.Style = res.Resources["customstyle"] as Style;
        }

        protected override void OnApplyTemplate()
        {
            (this.DataContext as CustomColumnChooserViewModel).ColumnChooserCommand = new DelegateCommand<object>(CommandHandler);
        }

        #region ColumnChooser Positioning

        /// <summary>
        /// Occurs when the mouse pointer is moved 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            PointerPoint pp = e.GetCurrentPoint(null);

            double deltaV = pp.Position.Y;
            double deltaH = pp.Position.X;
            if (isPointerPressed && pp.Properties.IsLeftButtonPressed && deltaH != pointerPressedPoint.X && deltaV != pointerPressedPoint.Y)
            {
                this.CapturePointer(e.Pointer);
                (this.Parent as Popup).HorizontalOffset = deltaH - pointerPressedPoint.X;
                (this.Parent as Popup).VerticalOffset = deltaV - pointerPressedPoint.Y;
                e.Handled = true;
            }

        }

        /// <summary>
        /// Occurs when the mouse pointer is released 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            pointerPressedPoint.X = -1;
            pointerPressedPoint.Y = -1;
            isPointerPressed = false;
            this.ReleasePointerCapture(e.Pointer);
            e.Handled = true;
        }

        /// <summary>
        /// Occurs when the mouse pointer is pressed 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            isPointerPressed = true;
            pointerPressedPoint = e.GetCurrentPoint(this).Position;
            e.Handled = true;
        }

        #endregion

        #region Button Handler
        private void CommandHandler(object obj)
        {
            if (obj != null)
            {
                if (obj.ToString().Equals("OkButton"))
                {
                    ClickOKButton((this.DataContext as CustomColumnChooserViewModel).ColumnCollection, this.dataGrid);
                    (this.Parent as Popup).IsOpen = false;
                }
                else
                {
                    (this.Parent as Popup).IsOpen = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// Occurs when the OK Button is clicked
        /// </summary>
        /// <param name="e"></param>
        public void ClickOKButton(ObservableCollection<ColumnChooserItems> ColumnCollection, SfDataGrid dataGrid)
        {
            foreach (var item in ColumnCollection)
            {
                var column = dataGrid.Columns.FirstOrDefault(v => v.MappingName == item.Name);
                if (column != null)
                {
                    if (item.IsChecked == false && !column.IsHidden)
                    {
                        column.IsHidden = true;
                    }
                    else if (item.IsChecked == true && column.IsHidden == true)
                    {
                        column.IsHidden = false;
                    }
                }

            }
        }

    }
}
