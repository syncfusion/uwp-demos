using Common;
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

namespace SampleBrowser.GroupBar
{
    public sealed partial class GroupBarView : SampleLayout
    {
        public GroupBarView()
        {
            this.InitializeComponent();
          
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).Tag.ToString() == "Mail")
            {
                string header = ((sender as ListView).SelectedItem as ListViewItem).Content.ToString();
                ContentView.Content = new MailView() { DataContext = new MailViewModel(header) };
            }
            else if ((sender as ListView).Tag.ToString() == "Contacts")
            {
                ContentView.Content = new ContactView() { DataContext = new ContactViewModel() };
            }
            else if ((sender as ListView).Tag.ToString() == "Tasks")
            {
                ContentView.Content = new TaskView();
            }
            else if ((sender as ListView).Tag.ToString() == "Notes")
            {
                ContentView.Content = new NoteView() { DataContext = new NoteViewModel() };
            }
        }

        private void groupBar_Collapsed(object sender, RoutedEventArgs e)
        {
            if (groupBar.IsCollapsed)
                groupBar.Width = 45.0;
        }

        private void groupBar_Expanded(object sender, RoutedEventArgs e)
        {
            
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                    groupBar.Width = 280.0;
            else
                groupBar.Width = 360.0;

        }

        public override void Dispose()
        {
            if (groupBar != null)
            {
                groupBar.Expanded -= groupBar_Expanded;
                groupBar.Collapsed -= groupBar_Collapsed;
                groupBar.Dispose();
                if (groupBar.Items.Count > 0)
                    groupBar.Items.Clear();
                groupBar = null;
            }
        }
    }
}
