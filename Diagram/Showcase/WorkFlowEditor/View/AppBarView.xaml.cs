using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorkFlowEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppBarView : Page
    {
        public AppBarView()
        {
            this.InitializeComponent();
        }
         public ICommand ExportFiles
        {
            get { return (ICommand)GetValue(ExportFilesProperty); }
            set { SetValue(ExportFilesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExportFiles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExportFilesProperty =
            DependencyProperty.Register("ExportFiles", typeof(ICommand), typeof(AppBarView), new PropertyMetadata(null));

        public ICommand ImportFiles
        {
            get { return (ICommand)GetValue(ImportFilesProperty); }
            set { SetValue(ImportFilesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImportFiles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImportFilesProperty =
            DependencyProperty.Register("ImportFiles", typeof(ICommand), typeof(AppBarView), new PropertyMetadata(null));

      
        public ICommand Delete
        {
            get { return (ICommand)GetValue(DeleteProperty); }
            set { SetValue(DeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Delete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteProperty =
            DependencyProperty.Register("Delete", typeof(ICommand), typeof(AppBarView), new PropertyMetadata(null));



        public ICommand Save
        {
            get { return (ICommand)GetValue(SaveProperty); }
            set { SetValue(SaveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Save.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaveProperty =
            DependencyProperty.Register("Save", typeof(ICommand), typeof(AppBarView), new PropertyMetadata(null));

        public ICommand Load
        {
            get { return (ICommand)GetValue(LoadProperty); }
            set { SetValue(LoadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Load.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadProperty =
            DependencyProperty.Register("Load", typeof(ICommand), typeof(AppBarView), new PropertyMetadata(null));

    

        public ICommand Clear
        {
            get { return (ICommand)GetValue(ClearProperty); }
            set { SetValue(ClearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClearProperty =
            DependencyProperty.Register("Clear", typeof(ICommand), typeof(AppBarView), new PropertyMetadata(null));

        private void Viewbox_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
            if (Delete != null)
            {
                Delete.Execute(null);
            }
        }

       

        private void Save_Released(object sender, PointerRoutedEventArgs e)
        {
            if (Save != null)
            {
                Save.Execute(null);
            }
        }

        private void Load_Released(object sender, PointerRoutedEventArgs e)
        {
            if (Load != null)
            {
                Load.Execute(null);
            }
        }

   
        private void Clear_Clicked(object sender, PointerRoutedEventArgs e)
        {
            if (Clear != null)
            {
                Clear.Execute(null);
            }
        }
    


        private void ExportFiles_Clicked(object sender, PointerRoutedEventArgs e)
        {
            ExportFiles.Execute(null);            
        }

        private void ImportFiles_Clicked(object sender, PointerRoutedEventArgs e)
        {
            ImportFiles.Execute(null);
        }

        private void Delete_Clicked(object sender, PointerRoutedEventArgs e)
        {
            (this.DataContext as ProcessAutomationViewModel).DeleteFiles.Execute(null);
        }

        private void Rename_Clicked(object sender, PointerRoutedEventArgs e)
        {
        }

        private void ClearSelection_Clicked(object sender, PointerRoutedEventArgs e)
        {

        }


    }

    public class VisibiliyCommandBinding : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ICommand cmd = value as ICommand;
            if (cmd.CanExecute(null))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class VisibilityInverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility vis = (Visibility)value;
            if (vis == Visibility.Collapsed)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
       
}
