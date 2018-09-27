using Syncfusion.UI.Xaml.Controls.Media;
using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DiagramBuilder.View
{
    public sealed partial class BrushPicker : UserControl
    {
        public BrushPicker()
        {
            this.InitializeComponent();
                 
        }
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(BrushPicker), new PropertyMetadata(null, OnColorChanged));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BrushPicker brush = d as BrushPicker;
            //(brush.Brush as SolidColorBrush).Color = brush.Color;
            if ((brush.Brush as SolidColorBrush).Color != brush.Color)
            {
                brush.Brush = new SolidColorBrush(brush.Color);
            }
        }


        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(BrushPicker), new PropertyMetadata(new SolidColorBrush(), OnBrushChanged));


        private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BrushPicker brush = d as BrushPicker;
            if((brush.Brush as SolidColorBrush).Color != brush.Color)
            {
                brush.Color = (brush.Brush as SolidColorBrush).Color;
            }
        }

    }
}
