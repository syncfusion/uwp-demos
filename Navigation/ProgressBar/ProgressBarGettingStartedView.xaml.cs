using Common;
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

namespace SampleBrowser.ProgressBar
{
    public sealed partial class ProgressBarGettingStartedView : SampleLayout
    {
        public ProgressBarGettingStartedView()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.controlView.Visibility == Visibility.Visible)
            {
                this.controlView.Visibility = Visibility.Collapsed;
                this.setting.Visibility = Visibility.Visible;
                (sender as Button).Content = "Done";
            }
            else
            {
                this.controlView.Visibility = Visibility.Visible;
                this.setting.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Options";
            }
        }

        public override void Dispose()
        {
            if (progressLine != null)
                progressLine.Dispose();
            
            if (progressSolid != null)
                progressSolid.Dispose();
            
            if (DataContext is GettingStartedProperties)
                (DataContext as GettingStartedProperties).Dispose();
            DataContext = null;
            progressLine = null;
            progressSolid = null;
            
        }       
    }
}
