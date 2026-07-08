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
    /// Represents the FontFormatGrid control.
    /// </summary>
    public sealed partial class FontFormatGrid : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets the bold button.
        /// </summary>
        /// <value>
        /// The bold button.
        /// </value>
        public ToggleButton BoldButton
        {
            get
            {
                return boldButton;
            }
        }
        /// <summary>
        /// Gets the italic button.
        /// </summary>
        /// <value>
        /// The italic button.
        /// </value>
        public ToggleButton ItalicButton
        {
            get
            {
                return italicButton;
            }
        }
        /// <summary>
        /// Gets the underline button.
        /// </summary>
        /// <value>
        /// The underline button.
        /// </value>
        public ToggleButton UnderlineButton
        {
            get
            {
                return underlineButton;
            }
        }
        /// <summary>
        /// Gets the single strike through button.
        /// </summary>
        /// <value>
        /// The single strike through button.
        /// </value>
        public ToggleButton SingleStrikeThroughButton
        {
            get
            {
                return singleStrikeThroughButton;
            }
        }
        /// <summary>
        /// Gets the double strike through button.
        /// </summary>
        /// <value>
        /// The double strike through button.
        /// </value>
        public ToggleButton DoubleStrikeThroughButton
        {
            get
            {
                return doubleStrikeThroughButton;
            }
        }
        /// <summary>
        /// Gets the subscript button.
        /// </summary>
        /// <value>
        /// The subscript button.
        /// </value>
        public ToggleButton SubscriptButton
        {
            get
            {
                return subscriptButton;
            }
        }
        /// <summary>
        /// Gets the superscript button.
        /// </summary>
        /// <value>
        /// The superscript button.
        /// </value>
        public ToggleButton SuperscriptButton
        {
            get
            {
                return superscriptButton;
            }
        }
        /// <summary>
        /// Gets the font family box.
        /// </summary>
        /// <value>
        /// The font family box.
        /// </value>
        public ComboBox FontFamilyBox
        {
            get
            {
                return fontFamilyBox;
            }
        }
        /// <summary>
        /// Gets the font size box.
        /// </summary>
        /// <value>
        /// The font size box.
        /// </value>
        public ComboBox FontSizeBox
        {
            get
            {
                return fontSizeBox;
            }
        }
        /// <summary>
        /// Gets the font color button.
        /// </summary>
        /// <value>
        /// The font color button.
        /// </value>
        public Button FontColorButton
        {
            get
            {
                return fontColorButton;
            }
        }
        /// <summary>
        /// Gets the highlight color button.
        /// </summary>
        /// <value>
        /// The highlight color button.
        /// </value>
        public Button HighlightColorButton
        {
            get
            {
                return highlightColorButton;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FontFormatGrid"/> class.
        /// </summary>
        public FontFormatGrid(SfRichTextBoxAdv richTextBox)
        {
            this.InitializeComponent();
            fontFamilyBox.ItemsSource = richTextBox.FontNames;
            fontFamilyBox.SetBinding(ComboBox.SelectedValueProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.FontFamily"), Mode = BindingMode.TwoWay, Converter = new FontFamilyStringConverter() });
            Binding binding = new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.FontSize"), Mode = BindingMode.TwoWay, Converter = new DoubleStringConverter() };
            fontSizeBox.SetBinding(ComboBox.SelectedValueProperty, binding);
            boldButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.Bold"), Mode = BindingMode.TwoWay });
            italicButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.Italic"), Mode = BindingMode.TwoWay });
            underlineButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.Underline"), Mode = BindingMode.TwoWay, Converter = new UnderlineToggleConverter() });
            singleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.StrikeThrough"), Mode = BindingMode.TwoWay, Converter = new SingleStrikeThroughToggleConverter() });
            doubleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.StrikeThrough"), Mode = BindingMode.TwoWay, Converter = new DoubleStrikeThroughToggleConverter() });
            subscriptButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.BaselineAlignment"), Mode = BindingMode.TwoWay, Converter = new SubscriptToggleConverter() });
            superscriptButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding() { Source = richTextBox, Path = new PropertyPath("Selection.CharacterFormat.BaselineAlignment"), Mode = BindingMode.TwoWay, Converter = new SuperscriptToggleConverter() });
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            boldButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            italicButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            underlineButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            singleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            doubleStrikeThroughButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            subscriptButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding());
            superscriptButton.SetBinding(Button.BackgroundProperty, new Binding());
            fontColorButton.SetBinding(Button.BackgroundProperty, new Binding());
            highlightColorButton.SetBinding(Button.BackgroundProperty, new Binding());
            fontFamilyBox.SetBinding(ComboBox.SelectedValueProperty, new Binding());
            fontSizeBox.SetBinding(ComboBox.SelectedValueProperty, new Binding());
            boldButton = null;
            italicButton = null;
            underlineButton = null;
            singleStrikeThroughButton = null;
            doubleStrikeThroughButton = null;
            subscriptButton = null;
            superscriptButton = null;
            fontColorButton = null;
            highlightColorButton = null;
            fontFamilyBox = null;
            fontSizeBox = null;
        }
        #endregion
    }
}
