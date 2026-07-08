using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Layout.Accordion
{
    public sealed partial class DataBindingView : SampleLayout
    {
        public DataBindingView()
        {
            this.InitializeComponent();
            this.Loaded += AccordionView_Loaded;
        }
        void AccordionView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                accordion.Width = 300;
                accordion.Foreground = new SolidColorBrush(Colors.Black);
            }

        }        

        public override void Dispose()
        {
            Loaded -= AccordionView_Loaded;
            if (accordion != null)
            {
                accordion.Dispose();
                if (accordion.Items != null && accordion.Items.Count > 0)
                    accordion.Items.Clear();
                accordion.ItemsSource = null;
                accordion.ItemContainerStyle = null;
                accordion.ContentTemplate = null;
                accordion.HeaderTemplate = null;
                if (DataContext is EmployeeDetails)
                {
                    (DataContext as EmployeeDetails).Dispose();
                }

                DataContext = null;

            }
            accordion = null;
            GC.Collect();
        }
    }
}
