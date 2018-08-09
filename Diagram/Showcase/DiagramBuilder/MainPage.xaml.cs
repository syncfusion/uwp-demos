using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DiagramBuilder.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
using DiagramBuilder.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DiagramBuilder
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Binding exitBinding = new Binding();
            exitBinding.Path = new PropertyPath("Exit");
            exitBinding.Mode = BindingMode.TwoWay;
            SetBinding(ExitProperty, exitBinding);
            Binding ZoomInBinding = new Binding();
            ZoomInBinding.Path = new PropertyPath("ZoomIn");
            SetBinding(ZoomInProperty, ZoomInBinding);
            Binding ZoomOutBinding = new Binding();
            ZoomOutBinding.Path = new PropertyPath("ZoomOut");
            SetBinding(ZoomOutProperty, ZoomOutBinding);
          
            ZoomIn = new Command(OnZoomInCommand);
            ZoomOut = new Command(OnZoomOutCommand);

        }


        // Using a DependencyProperty as the backing store for Exit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExitProperty =
            DependencyProperty.Register("Exit", typeof(ICommand), typeof(MainPage), new PropertyMetadata(null));

        public ICommand ZoomOut
        {
            get { return (ICommand)GetValue(ZoomOutProperty); }
            set { SetValue(ZoomOutProperty, value); }
        }

        public static readonly DependencyProperty ZoomOutProperty =
           DependencyProperty.Register("ZoomOut", typeof(ICommand), typeof(MainPage), new PropertyMetadata(null));

        public ICommand ZoomIn
        {
            get { return (ICommand)GetValue(ZoomInProperty); }
            set { SetValue(ZoomInProperty, value); }
        }

        public static readonly DependencyProperty ZoomInProperty =
           DependencyProperty.Register("ZoomIn", typeof(ICommand), typeof(MainPage), new PropertyMetadata(null));

      
        public void OnZoomInCommand(object param)
        {
            IDiagramBuilderVM vm = this.DataContext as IDiagramBuilderVM;
            vm.ZoomIn.Execute(null);
            
        }
        public void OnZoomOutCommand(object param)
        {
            IDiagramBuilderVM vm = this.DataContext as IDiagramBuilderVM;
            vm.ZoomOut.Execute(null);
            
            
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
