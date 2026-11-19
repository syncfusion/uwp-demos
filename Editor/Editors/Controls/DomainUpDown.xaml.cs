using Common;
using Syncfusion.UI.Xaml.Controls;
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

namespace SampleBrowser.Editors.Controls.DomainUpDown
{
    public sealed partial class DomainUpDown : SampleLayout
    {
        public DomainUpDown()
        {
            this.InitializeComponent();
        }

        private void spinalignment_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(SpinButtonsAlignment));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }
        public override void Dispose()
        {
            if (technologydomainupdown != null)
                technologydomainupdown.Dispose();
            if (productdomainupdown != null)
                productdomainupdown.Dispose();
            this.spinalignment.ItemsSource = null;
            spinalignment.Loaded -= spinalignment_Loaded_1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.controlView.Visibility == Visibility.Visible)
            {
                this.controlView.Visibility = Visibility.Collapsed;
                this.settings.Visibility = Visibility.Visible;
                (sender as Button).Content = "Done";
            }
            else
            {
                this.controlView.Visibility = Visibility.Visible;
                this.settings.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Options";
            }
        }
    }
}
