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

namespace DataGrid
{
    public sealed partial class MultiColumnDropDownControl : SampleLayout
    {
        public MultiColumnDropDownControl()
        {
            this.InitializeComponent();
            this.DataContext = new CustomerInfoViewModel();
        }
        public sealed override void Dispose()
        {
            this.MultiColumnControl.Dispose();
            this.MultiColumnControl1.Dispose();
            this.DataContext = null;
            base.Dispose();
        }
    }
}
