using Common;
using Syncfusion.UI.Xaml.Controls;
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

namespace Input.SpellChecker
{
    public sealed partial class SpellCheckerView : SampleLayout
    {
        TextSpellEditor SpellEditor;
        public SpellCheckerView()
        {
            SpellChecker = new SfSpellChecker();
            this.InitializeComponent();
            SpellEditor = new TextSpellEditor(txtbox);
            Editor = SpellEditor;
            SpellChecker.PerformSpellCheckUsingContextMenu(Editor);

           
        }

        public IEditorProperties Editor
        {
            get;
            set;
        }

        public SfSpellChecker SpellChecker
        {
            get;
            set;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SpellChecker.PerformSpellCheckUsingDialog(Editor);
        }

        public static bool IsMobileFamily()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                return true;
            }
            return false;
        }
        private void ignoreChecked(object sender, RoutedEventArgs e)
        {
            string _str = (sender as CheckBox).Tag.ToString();
            bool _ischecked = (bool)(sender as CheckBox).IsChecked;
            switch (_str)
            {
                case "1":
                    SpellChecker.IgnoreUrl = _ischecked;
                    break;
                case "2":
                    SpellChecker.IgnoreUpperCaseWords = _ischecked;
                    break;
                case "3":
                    SpellChecker.IgnoreAlphaNumericWords = _ischecked;
                    break;
                case "4":
                    SpellChecker.IgnoreEmailAddress = _ischecked;
                    break;
                case "5":
                    SpellChecker.IgnoreMixedCaseWords = _ischecked;
                    break;
            }
        }
        public override void Dispose()
        {
            if (SpellChecker != null)
                SpellChecker.Dispose();
            SpellChecker = null;
            GC.Collect();
        }
    }

    
}
