#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

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
    public sealed partial class UpcBarcode : UserControl
    {
        public UpcBarcode()
        {
            this.InitializeComponent();
            barCodeTip.Text = "1 2 3 4 5 6 7 8 9 0\nText length should be 11 or 12!!!";
            UpcBarcodeSetting codeUpc = new UpcBarcodeSetting();
            codeUpc.BarHeight = 120;
            barcodeTxt.Text = "01234567890";
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
            string expression = @"^[\x00-\x7F]";
            bool success = false;

            Regex validator = new Regex(expression, RegexOptions.Singleline);
            if (!validator.Match(barcodeTxt.Text).Success || barcodeTxt.Text.Length != 11 && barcodeTxt.Text.Length != 12)
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
