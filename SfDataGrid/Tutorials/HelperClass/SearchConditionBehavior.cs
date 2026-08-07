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
    /// This class represents the FilteringComboBox
    /// </summary>
    class FilteringComboBox
    {
        /// <summary>
        /// Dependency property for CommandProperty
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
        typeof(FilteringComboBox), new PropertyMetadata(null, PropertyChangedCallback));

        /// <summary>
        /// Call back for CommandProperty
        /// </summary>
        /// <param name="depObj"></param>
        /// <param name="args"></param>
        public static void PropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ComboBox comboBox = depObj as ComboBox;
            if (comboBox != null)
            {
                comboBox.SelectionChanged += comboBox_SelectionChanged;
            }
        }

        /// <summary>
        /// Occurs when ComboBox Selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                ICommand command = comboBox.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(comboBox.SelectedItem);
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
