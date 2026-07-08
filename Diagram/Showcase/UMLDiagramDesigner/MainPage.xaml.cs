using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UMLDiagramDesigner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            this.InitializeComponent();
            UMLViewModel VM = new UMLViewModel();
            VM.GoBack = new DelegateCommand<object>(OnGoBack);
            this.DataContext = VM;
        }

        private async void OnGoBack(object parameter)
        {
            await (this.FindName("diagram1") as Diagram).SaveAs(null);
            UMLViewModel VM = this.DataContext as UMLViewModel;
            VM.GoBack = null;
            this.DataContext = null;
        }
    }
}
