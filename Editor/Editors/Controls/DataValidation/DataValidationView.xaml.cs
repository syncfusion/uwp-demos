using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Editors;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.DataValidation
{
    public sealed partial class DataValidationView : UserControl , IDisposable
    {
        public DataValidationView()
        {
            this.InitializeComponent();
        }
        void TextBoxExtDemo_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= TextBoxExtDemo_Loaded;
            firstName.Focus(FocusState.Keyboard);
        }
        void Country_Text_changed(object sender, RoutedEventArgs e)
        {
            (this.DataContext as TextBoxExtProperties1).Cities.Clear();
            (this.DataContext as TextBoxExtProperties1).City = null;
        }

        public void Dispose()
        {
            this.Loaded -= TextBoxExtDemo_Loaded;
        }
    }
}
