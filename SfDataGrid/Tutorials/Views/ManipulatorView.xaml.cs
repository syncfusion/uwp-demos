using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace DataGrid
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ManipulatorView : Page
    {
        public ManipulatorView()
        {
            this.InitializeComponent();
            
        }


        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            //var pp = e.GetCurrentPoint(null);
            //var gt = this.grid.TransformToVisual(this);
            //Point p = gt.TransformPoint(new Point(0, 0));
            //Rect rect = new Rect(p, new Size(this.grid.ActualWidth, this.grid.ActualHeight));
            //if (!rect.Contains(pp.Position) && this.Tag is Popup)
            //    (this.Tag as Popup).IsOpen = false;

            base.OnPointerPressed(e);
        }

        
        public void CancelClick(object sender, RoutedEventArgs e)
        {
            (this.Tag as Popup).IsOpen = false;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                int x;
                double y;
                var data = this.DataContext as OrderInfo;
                data.OrderID = int.TryParse(this.txtOrderID.Text, out x) ? x : data.OrderID;
                data.CustomerID = this.txtCustomerID.Text;
                data.EmployeeID = this.txtEmployeeID.Text;
                data.ShipCity = this.txtShipCity.Text;
                data.ShipCountry = this.txtShipCountry.Text;
                data.Freight = double.TryParse(this.txtFreight.Text, out y) ? y : data.Freight;

            }
            (this.Tag as Popup).IsOpen = false;
        }

    }
}
