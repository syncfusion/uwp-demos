using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace ExpenseAnalysisDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TransactionDetailsView : UserControl, IDisposable
    {
        public TransactionDetailsView()
        {
            this.InitializeComponent();
            stackPanel.Orientation = Orientation.Vertical;
            positive.Height = 130;
            positive.Width = 194;
            negative.Height = 130;
            negative.Width = 194;
            balance.Height = 130;
            balance.Width = 194;
            this.Unloaded += OnTransactionDetailsViewUnloaded;
        }

        private void OnTransactionDetailsViewUnloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            this.Resources.Clear();
            this.Content = null;
            this.Unloaded -= OnTransactionDetailsViewUnloaded;
        }
    }
}
