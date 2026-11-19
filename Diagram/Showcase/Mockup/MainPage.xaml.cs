﻿using Mockup.Utility;
using Mockup.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mockup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Exit = new Command(OnExitCommand);
        }

        public ICommand Exit
        {
            get { return (ICommand)GetValue(ExitProperty); }
            set { SetValue(ExitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Exit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExitProperty =
            DependencyProperty.Register("Exit", typeof(ICommand), typeof(MainPage), new PropertyMetadata(null));

        public async void OnExitCommand(object param)
        {
            IDiagramBuilderVM viewModel = this.DataContext as IDiagramBuilderVM;
            await viewModel.PrepareExit();
            Exit = null;
            Frame.GoBack();
        }
    }
}
