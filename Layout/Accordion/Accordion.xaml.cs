using Common;
using Syncfusion.UI.Xaml.Controls.Layout;
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

namespace Layout.Accordion
{
    public sealed partial class Accordion : SampleLayout
    {
        public Accordion()
        {
            this.InitializeComponent();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                Mode.FontSize = 16;
            }
        }
        private void LoadValues(object sender, RoutedEventArgs e)
        {
            Mode.Items.Clear();
            Mode.Items.Add(AccordionSelectionMode.One);
            Mode.Items.Add(AccordionSelectionMode.OneOrMore);
            Mode.Items.Add(AccordionSelectionMode.ZeroOrMore);
            Mode.Items.Add(AccordionSelectionMode.ZeroOrOne);
            Mode.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (this.controlView.Visibility == Visibility.Visible)
            //{
            //    this.controlView.Visibility = Visibility.Collapsed;
            //    this.setting.Visibility = Visibility.Visible;
            //    (sender as Button).Content = "Done";
            //}
            //else
            //{
            //    this.controlView.Visibility = Visibility.Visible;
            //    this.setting.Visibility = Visibility.Collapsed;
            //    (sender as Button).Content = "Options";
            //}
        }

        public override void Dispose()
        {
            Mode.Loaded -= LoadValues;
        }
    }
}
