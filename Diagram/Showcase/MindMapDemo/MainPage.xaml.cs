using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Diagram;
using System.Collections.ObjectModel;
using Windows.UI;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Animation;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MindMapDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            MapViewModel m = new MapViewModel();            
            m.GoBack = new DelegateCommand<object>(OnGoBack);
            this.DataContext = m;
        }

        private void OnGoBack(object obj)
        {
            MapViewModel m = this.DataContext as MapViewModel;
            m.GoBack = null;
            this.DataContext = null;
            Frame.GoBack();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

    }
       
}
