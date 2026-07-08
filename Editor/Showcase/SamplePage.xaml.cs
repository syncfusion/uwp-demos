using Common;
using Syncfusion.UI.Xaml.Utils;
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

namespace UnitConverter
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SamplePage : Page
    {
        public SamplePage()
        {
            this.InitializeComponent();
            this.Loaded += SamplePage_Loaded;
            if(tabControl!=null)
            tabControl.LayoutUpdated += TabControl_LayoutUpdated;


        }

        private void TabControl_LayoutUpdated(object sender, object e)
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                ScrollViewer headergrid = FindChildControl<ScrollViewer>(tabControl, "PART_ScrollViewer") as ScrollViewer;
                if(headergrid != null)
                headergrid.Margin = new Thickness(0, 0, 0, 7);
            }
        }

        private DependencyObject FindChildControl<T>(DependencyObject control, string controlname)
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                FrameworkElement fe = child as FrameworkElement;
                if (fe == null) return null;

                if (child is T && fe.Name == controlname)
                {
                    return child;
                }
                else
                {
                    DependencyObject nextLevel = FindChildControl<T>(child, controlname);
                    if (nextLevel != null)
                        return nextLevel;
                }
            }
            return null;
        }

        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        void SamplePage_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        string lastView = "Currency";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.controlView.Visibility == Visibility.Visible)
            {
                this.controlView.Visibility = Visibility.Collapsed;
                this.settings.Visibility = Visibility.Visible;
                (sender as Button).Content = "Cancel";
                this.converterTitle.Text = "Options";
            }
            else
            {
                this.controlView.Visibility = Visibility.Visible;
                this.settings.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Converters Options";
                this.converterTitle.Text = lastView;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.currencyView.Visibility = this.timeView.Visibility = this.volumeView.Visibility = this.weightView.Visibility = this.temperatureView.Visibility = this.dataView.Visibility = this.lengthView.Visibility = this.areaView.Visibility = Visibility.Collapsed;
            switch ((sender as Button).Content.ToString())
            {
                case "Currency Converter":
                    this.currencyView.Visibility = Visibility;
                    break;

                case "Time Converter":
                    this.timeView.Visibility = Visibility;
                    break;

                case "Volume Converter":
                    this.volumeView.Visibility = Visibility;
                    break;

                case "Weight Converter":
                    this.weightView.Visibility = Visibility;
                    break;

                case "Temperature Converter":
                    this.temperatureView.Visibility = Visibility;
                    break;

                case "Data Converter":
                    this.dataView.Visibility = Visibility;
                    break;

                case "Length Converter":
                    this.lengthView.Visibility = Visibility;
                    break;

                case "Area Converter":
                    this.areaView.Visibility = Visibility;
                    break;

            }
            this.controlView.Visibility = Visibility.Visible;
            this.settings.Visibility = Visibility.Collapsed;
            Options.Content = "Converters Options";
            this.lastView = this.converterTitle.Text = (sender as Button).Content.ToString().Replace(" Converter", "");



        }

        private void pageRoot_Unloaded(object sender, RoutedEventArgs e)
        {
            //Loaded -= SamplePage_Loaded;
            //backButton.Click -= Back;
            //Unloaded -= pageRoot_Unloaded;
            //tabControl.Dispose();
            
        }
    }
}
