using Common;
using Editors;
using Syncfusion.UI.Xaml.Controls.Input;
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

namespace SampleBrowser.Editors.Controls.ComboBox
{
    public sealed partial class ComboBox : SampleLayout
    {
        public ComboBox()
        {
            this.InitializeComponent();
            if(DeviceFamily.GetDeviceFamily()==Devices.Mobile)
            {
                EmployeesCombo.FontSize = 12;
                ProductsCombo.FontSize = 12;
                ComponentsCombo.FontSize = 12;
                view.Margin = new Thickness(10, 0, 20, 20);
            }
            ProductsCombo.LostFocus += ComponentsCombo_LostFocus;
        }

        private void ComponentsCombo_LostFocus(object sender, RoutedEventArgs e)
        {
            ComboBoxProperties comboboxproperties = new ComboBoxProperties();
            for (int i = 0; i < comboboxproperties.Products.Count; i++)
            {
                if (ProductsCombo.SelectedItem == null)
                {
                    ComponentsCombo.IsEnabled = false;
                }
                else
                {
                    if (comboboxproperties.Products[i] == ProductsCombo.SelectedItem)
                    {
                        ComponentsCombo.IsEnabled = false;
                    }
                    else
                    {
                        ComponentsCombo.IsEnabled = true;
                    }
                }
            }
        }

        private void comboBoxMode_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(ComboBoxModes));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 0;
        }
        public override void Dispose()
        {
            if (EmployeesCombo != null)
                EmployeesCombo.Dispose();
            if (ProductsCombo != null)
                ProductsCombo.Dispose();
            if (ComponentsCombo != null)
                ComponentsCombo.Dispose();
            this.ProductsCombo.ItemsSource = null;
            this.EmployeesCombo.ItemsSource = null;
            this.ComponentsCombo.ItemsSource = null;
            if (this.DataContext is IDisposable)
                (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            comboBoxMode.Loaded -= comboBoxMode_Loaded_1;
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
