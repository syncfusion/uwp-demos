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
using System.Text.RegularExpressions;

using Syncfusion.UI.Xaml.Controls.Barcode;
using Common;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BarcodeControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Code39Ext :UserControl
    {
        public Code39Ext()
        {
            this.InitializeComponent();
            barCodeTip.Text = "0-9 A-Z a-z";
            Code39ExtendedSetting code39Ex = new Code39ExtendedSetting();
            code39Ex.BarHeight = 120;
            barcode.SymbologySettings = code39Ex;
            barcodeTxt.Text = "SYNCFUSION";
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            barcodeTxt.GotFocus -= barcodeTxt_GotFocus;
            barcodeTxt.GotFocus += barcodeTxt_GotFocus;
        }

        void barcodeTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.SelectAll();
        }

        private bool ValidateText()
        {
            string expression = @"^[\x00-\x7F]+$";
            bool success = false;

            Regex validator = new Regex(expression, RegexOptions.Singleline);
            if (!validator.Match(barcodeTxt.Text).Success)
            {
                errorNotify.Visibility = Windows.UI.Xaml.Visibility.Visible;
                success = false;
            }
            else
            {
                errorNotify.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                success = true;
            }

            return success;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool flag = ValidateText();
            if (flag)
                barcode.Text = barcodeTxt.Text;
        }

        public void Dispose()
        {
            barcodeTxt.GotFocus -= barcodeTxt_GotFocus;
        }
    }
}
