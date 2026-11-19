using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Input.Picker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GettingStarted : SampleLayout
    {
        private Dictionary<string, SolidColorBrush> _colors;

        public Dictionary<string, SolidColorBrush> ColorsInfo
        {
            get { return _colors; }
            set { _colors = value; }
        }

        public ObservableCollection<object> Items { get; set; }

        public ObservableCollection<string> Headers { get; set; }
        public GettingStarted()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();
            ColorsInfo = Colors();
            Headers.Add("Color");
            
            foreach(string item in ColorsInfo.Keys)
            {
                Items.Add(item);
            }
            this.DataContext = this;
            picker.SelectionChanged += SfPicker_ValueChanged;
            bordercolor.SelectionChanged += bordercolor_SelectionChanged;
            backcolor.SelectionChanged += backcolor_SelectionChanged;
            forecolor.SelectionChanged += forecolor_SelectionChanged;
            selectbackcolor.SelectionChanged += selectbackcolor_SelectionChanged;         
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                picker.Width = 250;
                content.HorizontalAlignment = HorizontalAlignment.Stretch;
                content.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            }

            picker.SelectedIndex = 17;
        }

        public override void Dispose()
        {
            bordercolor.SelectionChanged -= bordercolor_SelectionChanged;
            backcolor.SelectionChanged -= backcolor_SelectionChanged;
            forecolor.SelectionChanged -= forecolor_SelectionChanged;
            selectbackcolor.SelectionChanged -= selectbackcolor_SelectionChanged;
            backcolor.Items.Clear();
            forecolor.Items.Clear();
            bordercolor.Items.Clear();
            selectbackcolor.Items.Clear();
            backcolor = null;
            forecolor = null;
            bordercolor = null;
            selectbackcolor = null;
            ColorsInfo.Clear();
            ColorsInfo = null;
            Items.Clear();
            Items = null;
            Headers.Clear();
            Headers = null;
            picker.SelectionChanged -= SfPicker_ValueChanged;
            picker.Dispose();
            picker = null;
            base.Dispose();
        }
    

        private Dictionary<string, SolidColorBrush> Colors()
        {
            var _color = typeof(Colors).GetRuntimeProperties().Select(c => new
                {
                    Brush = new SolidColorBrush((Color)c.GetValue(null)),
                    Name = c.Name
                });
            return _color.Take(30).ToDictionary(x => x.Name, x => x.Brush);
        }

        private void SfPicker_ValueChanged(object source, SelectionChangedEventArgs args)
        {
            if(args.AddedItems!=null)
            {
                string item = args.AddedItems[0].ToString();
                TextBlock txt = new TextBlock() { Text = item+ " Color changed",Margin=new Thickness(5) };
                 EventLog.Children.Insert(0, txt);
                picker.SelectionBackground = ColorsInfo[item] as SolidColorBrush;
            }

        }

        private void backcolor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(backcolor.SelectedItem!=null)
            picker.Background = ColorsInfo[backcolor.SelectedItem.ToString()]; 
        }

        private void forecolor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (forecolor.SelectedItem != null)
                picker.Foreground = ColorsInfo[forecolor.SelectedItem.ToString()];
        }

        private void bordercolor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bordercolor.SelectedItem != null)
                picker.BorderBrush = ColorsInfo[bordercolor.SelectedItem.ToString()];
        }

        private void selectbackcolor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectbackcolor.SelectedItem != null)
                picker.SelectionBackground = ColorsInfo[selectbackcolor.SelectedItem.ToString()];
        }
    }

   
}
