using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DataGrid
{
    /// <summary>
    /// This class represents the FilteringTextBox for Filtering 
    /// </summary>
    class FilteringTextBox
    {
        /// <summary>
        /// Dependency property for CommandParameter
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(FilteringTextBox), new PropertyMetadata(null, PropertyChangedCallback));

        /// <summary>
        /// Call back for CommandParameter Dependency Property 
        /// </summary>
        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            TextBox textBox = depObj as TextBox;
            if (textBox != null)
            {
                textBox.TextChanged += textBox_TextChanged;
            }
        }

        /// <summary>
        /// Occurs when Text in FilteringTextBox get changed
        /// </summary>
        static void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            if (textBox != null)
            {
                ICommand command = textBox.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(textBox.Text);
                }
            }
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
    }
}
