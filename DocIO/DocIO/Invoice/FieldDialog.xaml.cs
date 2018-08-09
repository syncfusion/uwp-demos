using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Invoice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dialog : Page,IDisposable
    {
        #region Fields
        public event EventHandler CloseRequested;
        public event EventHandler UpdateRequested;
        InvoiceItem m_invoiceItem;
        ProductList m_productlist;
        const double DefaultTaxInPercent = 7;
        bool onInit;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Dialog()
            : this(null, "Add Fields", new ProductList())
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public Dialog(ProductList productList)
            : this(null, "Add Fields", productList)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="title"></param>
        /// <param name="productList"></param>
        public Dialog(InvoiceItem newItem, string title, ProductList productList)
            : this(newItem, title, productList, 0)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="title"></param>
        public Dialog(InvoiceItem newItem, string title, ProductList productList, int productIndex)
        {
            this.InitializeComponent();
            var bounds = Window.Current.Bounds;
            this.Height = bounds.Height;

            if (newItem == null)
            {
                newItem = new InvoiceItem();
                newItem.ItemName = productList[0].Name;
                newItem.Rate = productList[0].Rate;
            }
            m_invoiceItem = newItem;
            onInit = true;
            this.DataContext = m_invoiceItem;
            this.Title.Text = title;
            double currentRate = m_invoiceItem.Rate;

            m_productlist = productList;
            this.item.ItemsSource = m_productlist;
            this.item.DisplayMemberPath = "Name";
            this.item.SelectedItem = m_productlist[productIndex];
            if (currentRate != 0)
                this.rate.Value = currentRate;
            if (!newItem.Taxes.Equals("0"))
            {
                this.taxesTextBlock.Text = newItem.Taxes.ToString();
            }
            if (newItem.Rate!=0)
            {
                this.rate.Value = newItem.Rate;

                UpdateTotalAmount();
            }

            if (newItem.Quantity==0)
            {
                newItem.Quantity = 1;
                this.quantity.Value = 1;
                CalculateTax();
                UpdateTotalAmount();
            }
            onInit = false;
        }
        #endregion
        #region Implementation

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CloseRequested != null)
                CloseRequested(this, EventArgs.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updtButton_Click(object sender, RoutedEventArgs e)
        {
            FieldsUpdateEventArgs args = new FieldsUpdateEventArgs();
            args.UpdatedFields = m_invoiceItem;

            if (UpdateRequested != null)
            {
                UpdateRequested(this, args);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)e.OriginalSource;
            textBox.SelectAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (onInit)
                return;
            int value = 0;
            if (int.TryParse((string)quantity.Value, out value))
            {
                CalculateTax();
                UpdateTotalAmount();
                quantity.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 64, 81));
            }
            else
            {
                quantity.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 37, 37));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rate_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (onInit)
                return;
            Product product = item.SelectedItem as Product;
            m_invoiceItem.ItemName = product.Name;
            if (!m_invoiceItem.Rate.Equals(product.Rate.ToString()))
            {
                m_invoiceItem.Rate = product.Rate;
                rate.Value = m_invoiceItem.Rate;
                CalculateTax();
                UpdateTotalAmount();
            }
        }

        #endregion
        #region Helper Method
        /// <summary>
        /// 
        /// </summary>
        public void InitializeFocus()
        {
            this.item.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }
        /// <summary>
        /// 
        /// </summary>
        public Product SelectedProduct
        {
            set
            {
                if (value != null)
                {
                    item.SelectedItem = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void UpdateTotalAmount()
        {

            int quantityValue = GetQuantityAsInt();
            double rateValue = (double)rate.Value;
            double calculatedTax = m_invoiceItem.Taxes;
            double taxedAmount = (quantityValue * rateValue) + calculatedTax;
            this.totalAmount.Text = "$" + taxedAmount.ToString();
        }
        public int GetQuantityAsInt()
        {
            return (quantity.Value is int) ? (int)quantity.Value : (int)(double)quantity.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CalculateTax()
        {
            double currentRate = (double)rate.Value;
            int currentQuantity = GetQuantityAsInt();

            {
                double calculatedTax = 0.0;
                calculatedTax = (currentRate * currentQuantity) * (DefaultTaxInPercent / 100);
                m_invoiceItem.Taxes = calculatedTax;
                taxesTextBlock.Text = "$" + calculatedTax.ToString();
            }
        }
        #endregion

        private void quantity_ValueChanged(object sender,Syncfusion.UI.Xaml.Controls.Input.ValueChangedEventArgs e)
        {
            if (onInit)
                return;
            if (rate != null)
            {
                double value = (double) rate.Value;
                {
                    rate.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 64, 81));
                    CalculateTax();
                    UpdateTotalAmount();
                }
            }
        }
        #region Implementation
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            UnlinkChildrens(this);
            this.Resources.Clear();
            Loaded -= cancelButton_Click;
            Loaded -= updtButton_Click;
            m_invoiceItem = null;
            m_productlist = null;
        }
        /// <summary>
        /// Unlink the child elements from the page.
        /// </summary>
        /// <param name="element"></param>
        void UnlinkChildrens(UIElement element)
        {
            if (element == null)
                return;
            if (element is Panel)
            {
                for (int i = 0; i < (element as Panel).Children.Count; i++)
                {
                    UIElement childElement = (element as Panel).Children[i];
                    UnlinkChildrens(childElement);
                    (element as Panel).Children.Remove(childElement);
                    i--;
                }
            }
            else if (element is ItemsControl)
            {
                for (int j = 0; j < (element as ItemsControl).Items.Count; j++)
                {
                    UIElement childElement = ((element as ItemsControl).Items[j] as UIElement);
                    if (childElement == null)
                    {
                        //(element as ItemsControl).Items.RemoveAt(j);
                        //j--;
                    }
                    else
                    {
                        UnlinkChildrens(childElement);
                        (element as ItemsControl).Items.Remove(childElement);
                        j--;
                    }
                }
            }
            else if (element is ContentControl)
            {
                UnlinkChildrens((element as ContentControl).Content as UIElement);
                (element as ContentControl).Content = null;
            }
            else if (element is UserControl)
            {
                UnlinkChildrens((element as UserControl).Content as UIElement);
                (element as UserControl).Content = null;
            }
        }
        #endregion
        #endregion
    }



    /// <summary>
    /// 
    /// </summary>
    public class FieldsUpdateEventArgs : EventArgs,IDisposable
    {
        private InvoiceItem m_invoiceItem;
        public InvoiceItem UpdatedFields
        {
            get
            {
                return m_invoiceItem;
            }
            set
            {
                m_invoiceItem = value;
            }
        }

        public void Dispose()
        {
            m_invoiceItem = null;
        }
    }
}
