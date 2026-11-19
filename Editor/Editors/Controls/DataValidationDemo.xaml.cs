using Common;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
using Editors;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.DataValidation
{
    public sealed partial class DataValidationDemo : SampleLayout
    {
        public DataValidationDemo()
        {
            this.InitializeComponent();
            this.Loaded += TextBoxExtDemo_Loaded;
        }

        void TextBoxExtDemo_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= TextBoxExtDemo_Loaded;
            firstName.Focus(FocusState.Keyboard);
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                firstName.FontSize = email.FontSize = country.FontSize = dob.FontSize = city.FontSize = 17;
                country.SuggestionBoxPlacement = Syncfusion.UI.Xaml.Controls.Input.SuggestionBoxPlacement.Top; city.SuggestionBoxPlacement = Syncfusion.UI.Xaml.Controls.Input.SuggestionBoxPlacement.Top;
                firstName.BorderBrush = email.BorderBrush = country.BorderBrush = city.BorderBrush = new SolidColorBrush(Color.FromArgb(0xA3, 0x00, 0x00, 0x00));
                firstName.Background = email.Background = country.Background = city.Background = dob.Background = new SolidColorBrush(Colors.White);                                
            }
        }

        void Country_Text_changed(object sender, RoutedEventArgs e)
        {
            (this.DataContext as TextBoxExtProperties1).Cities.Clear();
            (this.DataContext as TextBoxExtProperties1).City = null;
        }

        public override void Dispose()
        {
            if (firstName != null)
                firstName.Dispose();
            if (email != null)
                email.Dispose();
            if (country != null)
                country.Dispose();
            if (city != null)
                city.Dispose();

            this.Loaded -= TextBoxExtDemo_Loaded;
        }
    }
}
