using Common;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SampleBrowser.Editors.Controls.AutoComplete
{
    public sealed partial class EditAutoCompleteView : SampleLayout
    {
        public EditAutoCompleteView()
        {
            this.InitializeComponent();
            UpdateControl();
        }
        private Assembly assembly;

        private void UpdateControl()
        {
            assembly = Assembly.Load(new AssemblyName("Syncfusion.SfInput.UWP"));
            var list = new List<Field>();
            if (assembly != null)
            {
                foreach (Type type in assembly.ExportedTypes)
                {
                    if (type.Name.StartsWith("Sf") && type.Namespace == "Syncfusion.UI.Xaml.Controls.Input")
                    {
                        var item = new Field();
                        item.Name = type.Name;
                        item.Icon = "ms-appx:///Editors/Images/type.png";
                        list.Add(item);
                    }
                }
            }
            types.AutoCompleteSource = list;
            
            types.SuggestionMode = SuggestionMode.StartsWith;
        }
        private void autoCompleteMode_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(AutoCompleteMode));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = DeviceFamily.GetDeviceFamily() == Devices.Mobile ? 3 : 2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.controlView.Visibility == Visibility.Visible)
            {
                this.controlView.Visibility = Visibility.Collapsed;
                this.settings.Visibility = Visibility.Visible;
                (sender as Button).Content = "Done";
            }
            else
            {
                this.controlView.Visibility = Visibility.Visible;
                this.settings.Visibility = Visibility.Collapsed;
                (sender as Button).Content = "Options";
            }
        }

        private void suggestionMode_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((Windows.UI.Xaml.Controls.ComboBox)sender).ItemsSource = Enum.GetValues(typeof(SuggestionMode));
            ((Windows.UI.Xaml.Controls.ComboBox)sender).SelectedIndex = 1;
        }
        private void Types_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var list = new List<Field>();
            Type seltype = null;
            if (assembly != null)
            {
                foreach (Type type in assembly.ExportedTypes)
                {
                    if (type.Name == types.Text)
                    {
                        seltype = type;
                        break;
                    }
                }
            }

            if (seltype != null)
            {
                foreach (var field in seltype.GetRuntimeProperties())
                {
                    var item = new Field();
                    item.Name = field.Name;
                    item.Icon = "ms-appx:///Editors/Images/property.png";
                    list.Add(item);
                }

                foreach (var field in seltype.GetRuntimeMethods())
                {
                    if (field.Name.Contains("_"))
                        continue;
                    var item = new Field();
                    item.Name = field.Name;
                    item.Icon = "ms-appx:///Editors/Images/method.png";
                    list.Add(item);
                }

                foreach (var field in seltype.GetRuntimeEvents())
                {
                    var item = new Field();
                    item.Name = field.Name;
                    item.Icon = "ms-appx:///Editors/Images/event.png";
                    list.Add(item);
                }

                fields.AutoCompleteSource = list;
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var control = sender as TextBlock;
            string text = fields.Text;
            int index = control.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                var run1 = new Run() { Text = control.Text.Substring(0, index) };
                var run2 = new Run()
                {
                    Text = control.Text.Substring(index, text.Length),
                    Foreground = (SolidColorBrush)this.Resources["brush"]
                };
                var run3 = new Run() { Text = control.Text.Substring(index + text.Length) };

                control.Inlines.Clear();
                control.Inlines.Add(run1);
                control.Inlines.Add(run2);
                control.Inlines.Add(run3);
            }
        }

        private void FrameworkElement_OnLoaded1(object sender, RoutedEventArgs e)
        {
            var control = sender as TextBlock;
            string text = types.Text;
            int index = control.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                var run1 = new Run() { Text = control.Text.Substring(0, index) };
                var run2 = new Run()
                {
                    Text = control.Text.Substring(index, text.Length),
                    Foreground = (SolidColorBrush)this.Resources["brush"],
                };
                var run3 = new Run() { Text = control.Text.Substring(index + text.Length) };

                control.Inlines.Clear();
                control.Inlines.Add(run1);
                control.Inlines.Add(run2);
                control.Inlines.Add(run3);
            }
        }
        public override void Dispose()
        {
            if (types != null)
                types.Dispose();
            if (fields != null)
                fields.Dispose();
            if (muliple != null)
                muliple.Dispose();
            this.muliple.AutoCompleteSource = null;
            if (this.DataContext is IDisposable)
                (this.DataContext as IDisposable).Dispose();
            this.DataContext = null;
            suggestionMode.Loaded -= suggestionMode_Loaded_1;
            autoCompleteMode.Loaded -= autoCompleteMode_Loaded_1;
            types.TextChanged -= Types_OnTextChanged;
        }
    }
    public class Field
    {
        public string Name { get; set; }

        public string Icon { get; set; }
    }
}
