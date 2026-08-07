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

namespace DiagramBuilder.View
{
    public sealed partial class PropertyEditor : UserControl
    {
        public PropertyEditor()
        {
            this.InitializeComponent();
        }

        public Visibility TextEditing
        {
            get { return (Visibility)GetValue(TextEditingProperty); }
            set { SetValue(TextEditingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextEditing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextEditingProperty =
            DependencyProperty.Register("TextEditing", typeof(Visibility), typeof(PropertyEditor), new PropertyMetadata(Visibility.Visible));

        public Visibility PropertyEditing
        {
            get { return (Visibility)GetValue(PropertyEditingProperty); }
            set { SetValue(PropertyEditingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PropertyEditing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyEditingProperty =
            DependencyProperty.Register("PropertyEditing", typeof(Visibility), typeof(PropertyEditor), new PropertyMetadata(Visibility.Visible));
    }
}
