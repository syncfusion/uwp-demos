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

namespace RTEDemo
{
    /// <summary>
    /// Represents the HighlightColorPicker control.
    /// </summary>
    public sealed partial class HighlightColorPicker : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets the highlight color grid view.
        /// </summary>
        internal GridView HighlightColorGridView
        {
            get
            {
                return highlightColorGridView;
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Initializes a new instance of the <see cref="HighlightColorPicker"/> class.
        /// </summary>
        public HighlightColorPicker()
        {
            this.InitializeComponent();
        }
        #endregion
    }
}
