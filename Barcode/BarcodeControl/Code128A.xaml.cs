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
    public sealed partial class Code128A : UserControl
    {
        public Code128A()
        {
            this.InitializeComponent();
            barCodeTip.Text = "NUL (0x00) SOH (0x01) STX (0x02) ETX (0x03) EOT"
+ "(0x04) ENQ (0x05) ACK (0x06) BEL (0x07) BS (0x08) HT (0x09) LF (0x0A) VT"
+ "(0x0B) FF (0x0C) CR (0x0D) SO (0x0E) SI (0x0F) DLE (0x10) DC1 (0x11) DC2"
+ "(0x12) DC3 (0x13) DC4 (0x14) NAK (0x15) SYN (0x16) ETB (0x17) CAN"
+ "(0x18) EM (0x19) SUB (0x1A) ESC (0x1B) FS (0x1C) GS (0x1D) RS (0x1E) US"
+ "(0x1F) SPACE (0x20) \" ! # $ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? @ A"
+ "B C D E F G H I J K L M N O P Q R S T U V W X Y Z [ / ]^ _";
            Code128ASetting code128A = new Code128ASetting();
            code128A.BarHeight = 120;
            barcode.SymbologySettings = code128A;
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
            string expression = @"^[\000-\137]*$";
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
