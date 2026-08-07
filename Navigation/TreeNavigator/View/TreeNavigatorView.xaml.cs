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

namespace Navigation.TreeNavigator
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TreeNavigatorView : SampleLayout
    {
        /// <summary>
        /// 
        /// </summary>
        public TreeNavigatorView()
        {
            this.InitializeComponent();
            this.InitializeComponent();
          //  this.Loaded += TreeViewDemo_Loaded;
        }

        void TreeViewDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                tree.Width = 350;
                contentControl.Height = 450;
               // SettingsContent = null;
                contentControl.Width = 0;
            }
        }

        void NavigationMode_Loaded(object sender, RoutedEventArgs e)
        {
            navigation.Items.Add(Syncfusion.UI.Xaml.Controls.Navigation.NavigationMode.Default);
            navigation.Items.Add(Syncfusion.UI.Xaml.Controls.Navigation.NavigationMode.Extended);
            navigation.SelectedIndex = 1;
            navigation.SelectionChanged -= navigation_SelectionChanged;
            navigation.SelectionChanged += navigation_SelectionChanged;

        }

        void navigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tree.NavigationMode = (Syncfusion.UI.Xaml.Controls.Navigation.NavigationMode)navigation.SelectedItem;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            if (navigation != null)
            {
                navigation.SelectionChanged -= navigation_SelectionChanged;

                navigation.Loaded -= NavigationMode_Loaded;
            }
            if(DataContext is TreeViewModel)
            {
                if((DataContext as TreeViewModel).Models != null)
                {
                    (DataContext as TreeViewModel).Models.Clear();
                }
                DataContext = null;
            }
            if(tree != null)
            {
                tree.Dispose();
            }

            if (tree.Items != null && tree.Items.Count > 0)
                tree.Items.Clear();
            tree.ItemsSource = null;
            tree = null;
            GC.Collect();
        }

   
    }

    public class TreeDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is TreeModel)
            {
                return (value as TreeModel).Description;
            }
           if (value is TreeViewModel)
            {
                return null;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TreeHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is TreeModel)
            {
                return (value as TreeModel).Header;
            }
           if (value is TreeViewModel)
            {
                return null;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
