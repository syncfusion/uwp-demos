using Syncfusion.UI.Xaml.Controls.Input;
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

namespace FloorPlannerDemo
{
    public sealed partial class AddAnnotation : UserControl
    {
        public AddAnnotation()
        {
            this.InitializeComponent();
            text.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        void text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "Text")
            {
                (sender as TextBox).Select(0, 3);
            }
        }

        private void text_Loaded_1(object sender, RoutedEventArgs e)
        {
           
        }


        public FloorPlannerViewModel DataViewModel
        {
            get { return (FloorPlannerViewModel)GetValue(DataViewModelProperty); }
            set { SetValue(DataViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataViewModelProperty =
            DependencyProperty.Register("DataViewModel", typeof(FloorPlannerViewModel), typeof(AddAnnotation), new PropertyMetadata(null,OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddAnnotation a = d as AddAnnotation;
            if (a != null)
            {
                (a.DataViewModel as FloorPlannerViewModel).textbox = a.text;
                a.text.Select(0, 4);
            }
        }

        private void text_Loaded_2(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focus(Windows.UI.Xaml.FocusState.Keyboard);
            (sender as TextBox).Select(0, 4);
        }
    }
}
