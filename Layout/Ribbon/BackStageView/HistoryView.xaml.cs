using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SfRibbon.Ribbon
{
    public sealed partial class HistoryView : UserControl
    {
        public HistoryView()
        {
            this.InitializeComponent();
           

            History = new ObservableCollection<History>();

            History.Add(new History { FontIcon = "E", Time = "Today, 4:21 AM" });
            History.Add(new History { FontIcon = "E", Time = "Today, 2:52 AM" });
            History.Add(new History { FontIcon = "e", Time = "Today, 1:10 AM" });
            History.Add(new History { FontIcon = "D", Time = "Today, 1:07 AM" });
            History.Add(new History { FontIcon = "E", Time = "Today, 12:25 PM" });
            History.Add(new History { FontIcon = "5", Time = "Today, 11:02 AM" });
            History.Add(new History { FontIcon = "4", Time = "Today, 10:37 AM" });
            History.Add(new History { FontIcon = "E", Time = "Today, 9:32 AM" });
            History.Add(new History { FontIcon = "7", Time = "Yesterday, 5:05 PM" });
            History.Add(new History { FontIcon = "2", Time = "Yesterday, 3:51 PM" });
            History.Add(new History { FontIcon = "E", Time = "Yesterday, 2:33 PM" });
            History.Add(new History { FontIcon = "2", Time = "Yesterday, 1:59 PM" });
            History.Add(new History { FontIcon = "E", Time = "Yesterday, 1:54 PM" });
            this.DataContext = this;
        }

        private ObservableCollection<History> history;

        public ObservableCollection<History> History
        {
            get { return history; }
            set { history = value; }
        }


    }

    public class History
    {
        public string FontIcon { get; set; }
        public string Time { get; set; }
    }
}
