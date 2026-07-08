
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Input.SpellChecker
{
    public class TextSpellEditor:IEditorProperties
    {
        TextBox textbox;
        public TextSpellEditor(Control control)
        {
            ControlToCheck = control;
        }
        public Control ControlToCheck
        {
            get
            {
                return textbox;
            }
            set
            {
                textbox = value as TextBox;
            }
        }

        public string SelectedText
        {
            get
            {
                return textbox.SelectedText;
            }
            set
            {
                textbox.SelectedText = value;
            }
        }

        public string Text
        {
            get
            {
                return textbox.Text;
            }
            set
            {
                textbox.Text = value;
            }
        }

        public void Select(int selectionStart, int selectionLength)
        {
            textbox.Select(selectionStart, selectionLength);
        }

        public bool HasMoreTextToCheck()
        {
            return false;
        }

        public void Focus()
        {
            
        }

        public void UpdateTextField()
        {
            throw new NotImplementedException();
        }
    }
}
