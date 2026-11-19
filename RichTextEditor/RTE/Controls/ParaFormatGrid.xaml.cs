using Syncfusion.UI.Xaml.RichTextBoxAdv;
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
    /// Represents the ParaFormatGrid control.
    /// </summary>
    public sealed partial class ParaFormatGrid : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets the left align button.
        /// </summary>
        public ToggleButton LeftAlignButton
        {
            get
            {
                return leftAlignButton;
            }
        }
        /// <summary>
        /// Gets the center align button.
        /// </summary>
        public ToggleButton CenterAlignButton
        {
            get
            {
                return centerAlignButton;
            }
        }
        /// <summary>
        /// Gets the right align button.
        /// </summary>
        public ToggleButton RightAlignButton
        {
            get
            {
                return rightAlignButton;
            }
        }
        /// <summary>
        /// Gets the justify align button.
        /// </summary>
        public ToggleButton JustifyAlignButton
        {
            get
            {
                return justifyAlignButton;
            }
        }

        /// <summary>
        /// Gets the increase indent button.
        /// </summary>
        public Button IncreaseIndentButton
        {
            get
            {
                return increaseIndentButton;
            }
        }
        /// <summary>
        /// Gets the decrease indent button.
        /// </summary>
        public Button DecreaseIndentButton
        {
            get
            {
                return decreaseIndentButton;
            }
        }
        /// <summary>
        /// Gets the spacings menu button.
        /// </summary>
        public Button SpacingsMenuButton
        {
            get
            {
                return spacingsMenuButton;
            }
        }
        /// <summary>
        /// Gets the list menu button.
        /// </summary>
        public Button ListMenuButton
        {
            get
            {
                return listMenuButton;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizedEditor"/> class.
        /// </summary>
        public ParaFormatGrid(SfRichTextBoxAdv richTextBox)
        {
            this.InitializeComponent();
            leftAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.ParagraphFormat.TextAlignment"), Mode = BindingMode.TwoWay, Converter = new LeftAlignmentToggleConverter() });
            centerAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.ParagraphFormat.TextAlignment"), Mode = BindingMode.TwoWay, Converter = new CenterAlignmentToggleConverter() });
            rightAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.ParagraphFormat.TextAlignment"), Mode = BindingMode.TwoWay, Converter = new RightAlignmentToggleConverter() });
            justifyAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.ParagraphFormat.TextAlignment"), Mode = BindingMode.TwoWay, Converter = new JustifyAlignmentToggleConverter() });
            increaseIndentButton.Command = richTextBox.IncreaseIndentCommand;
            decreaseIndentButton.Command = richTextBox.DecreaseIndentCommand;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            leftAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            centerAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            rightAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            justifyAlignButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            leftAlignButton = null;
            centerAlignButton = null;
            rightAlignButton = null;
            justifyAlignButton = null;
            increaseIndentButton = null;
            decreaseIndentButton = null;
            spacingsMenuButton = null;
            listMenuButton = null;
        }
        #endregion
    }
}
