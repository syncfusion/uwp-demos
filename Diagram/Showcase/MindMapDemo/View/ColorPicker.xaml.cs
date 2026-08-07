using Syncfusion.UI.Xaml.Controls.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace MindMapDemo
{
    public sealed partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            this.InitializeComponent();
        }

        private void Picker_SelectedColorChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            SfColorPicker sf=(sender as SfColorPicker);
            if (this.DataContext != null)
            {
                if (this.DataContext is FloorPlannerDemo.FloorPlanNode)
                {
                    (this.DataContext as FloorPlannerDemo.FloorPlanNode).SelectedColor = sf.SelectedColor.ToString();
                }
            }
        }
    }
}
