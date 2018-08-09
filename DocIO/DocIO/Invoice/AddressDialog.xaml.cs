
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Invoice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Address : Page,IDisposable
    {
        public event EventHandler CloseRequested;
        public event EventHandler UpdateRequested;
        BillingInformation info;
        /// <summary>
        /// 
        /// </summary>
        public Address():this(new BillingInformation())
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="billInfo"></param>
        public Address(BillingInformation billInfo)
        {
            this.InitializeComponent();
            var bounds = Window.Current.Bounds;
            this.Height = bounds.Height;
            info = billInfo;
            this.DataContext = info;
        }
        ///// <summary>
        ///// Invoked when this page is about to be displayed in a Frame.
        ///// </summary>
        ///// <param name="e">Event data that describes how this page was reached.  The Parameter
        ///// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}
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
            BillingInfoEventArgs args = new BillingInfoEventArgs();
            args.BillingInformation = info;
            if (UpdateRequested != null)
                UpdateRequested(this, args);
        }

        private void invoiceNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 0;
            if (int.TryParse(invoiceNo.Text, out value))
            {
                invoiceNo.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 64, 81));
            }
            else
            {

                invoiceNo.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 37, 37));
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
            info = null;
                     
        }
        /// <summary>
        /// Unlinks the child elements from page.
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
    }
    /// <summary>
    /// 
    /// </summary>
    public class BillingInfoEventArgs : EventArgs,IDisposable
    {
        BillingInformation m_billInfo;
        public BillingInformation BillingInformation
        {
            get { return m_billInfo; }
            set { m_billInfo = value; }
        }

        public void Dispose()
        {
            m_billInfo = null;
        }
    }
}
