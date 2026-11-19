using Common;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GridSplitter
{
    public sealed partial class GridSplitterDemo : SampleLayout
    {
        public GridSplitterDemo()
        {
            this.InitializeComponent();
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

        public override void Dispose()
        {
            if(gridsplitter_one != null)
            {
                gridsplitter_one.Dispose();
                gridsplitter_one = null;
            }

            if(gridsplitter_two != null)
            {
                gridsplitter_two.Dispose();
                gridsplitter_two = null;
            }

            if(gridsplitter_three != null)
            {
                gridsplitter_three.Dispose();
                gridsplitter_three = null;
            }
            calculator = null;
            if (Calendar != null)
            {
                Calendar.Dispose();
                Calendar = null;
            }

            GC.Collect();
        }
    }
}
