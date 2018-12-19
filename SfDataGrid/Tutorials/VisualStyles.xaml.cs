using Common;
using MasterDetailsViewDemo;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DataGrid
{
    public sealed partial class VisualStyles : SampleLayout
    {
        public VisualStyles()
        {
            this.InitializeComponent();
            var datacontext = new DetailsViewViewModel();
            this.DataContext = datacontext;
            this.dataGrid.ItemsSource = datacontext.OrdersDetails;            
        }
       
        /// <summary>
        /// Occurs when combobox item is changed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StylesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var currentResource = this.Resources.MergedDictionaries.FirstOrDefault(resdict =>
            {
                if (resdict.Source.AbsoluteUri.Contains("ms-appx:///Tutorials/Styles"))
                    return true;
                return false;
            });
            if (currentResource != null)
                this.Resources.MergedDictionaries.Remove(currentResource);

            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Tutorials/Styles/" + comboBox.SelectedItem + ".xaml", UriKind.Absolute)
            });
        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.Resources.Clear();          
            this.dataGrid.Dispose();
            this.dataGrid.ItemsSource = null;
            (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
