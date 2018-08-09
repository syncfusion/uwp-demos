using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ShapesPath = Windows.UI.Xaml.Shapes.Path;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DiagramBuilder.View
{
    public sealed partial class SelectionCommands : UserControl
    {
        public SelectionCommands()
        {
            this.InitializeComponent();
        }
    }  
}
