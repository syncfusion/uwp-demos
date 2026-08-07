using Syncfusion.UI.Xaml.Controls.Input;
using Syncfusion.UI.Xaml.Controls.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Represents the ListFormatGrid control.
    /// </summary>
    public sealed partial class ListFormatGrid : UserControl
    {
        #region Properties
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
        /// Gets the font style box.
        /// </summary>
        /// <value>
        /// The font style box.
        /// </value>
        public ComboBox FontStyleBox
        {
            get
            {
                return fontStyleBox;
            }
        }
        /// <summary>
        /// Gets the font size box.
        /// </summary>
        /// <value>
        /// The font size box.
        /// </value>
        public SfNumericUpDown FontSizeBox
        {
            get
            {
                return fontSizeBox;
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
        /// Gets the list level box.
        /// </summary>
        /// <value>
        /// The list level box.
        /// </value>
        public ComboBox ListLevelBox
        {
            get
            {
                return listLevelBox;
            }
        }
        /// <summary>
        /// Gets the level pattern box.
        /// </summary>
        /// <value>
        /// The level pattern box.
        /// </value>
        public ComboBox LevelPatternBox
        {
            get
            {
                return levelPatternBox;
            }
        }
        /// <summary>
        /// Gets the number format box.
        /// </summary>
        /// <value>
        /// The number format box.
        /// </value>
        public TextBox NumberFormatBox
        {
            get
            {
                return numberFormatBox;
            }
        }
        /// <summary>
        /// Gets the start at box.
        /// </summary>
        /// <value>
        /// The start at box.
        /// </value>
        public SfNumericUpDown StartAtBox
        {
            get
            {
                return startAtBox;
            }
        }
        /// <summary>
        /// Gets the follow character box.
        /// </summary>
        /// <value>
        /// The follow character box.
        /// </value>
        public ComboBox FollowCharacterBox
        {
            get
            {
                return followCharacterBox;
            }
        }
        /// <summary>
        /// Gets the restart level box.
        /// </summary>
        /// <value>
        /// The restart level box.
        /// </value>
        public ComboBox RestartLevelBox
        {
            get
            {
                return restartLevelBox;
            }
        }
        /// <summary>
        /// Gets the firstline indent box.
        /// </summary>
        /// <value>
        /// The firstline indent box.
        /// </value>
        public SfNumericUpDown FirstlineIndentBox
        {
            get
            {
                return firstlineIndentBox;
            }
        }
        /// <summary>
        /// Gets the left indent box.
        /// </summary>
        /// <value>
        /// The left indent box.
        /// </value>
        public SfNumericUpDown LeftIndentBox
        {
            get
            {
                return leftIndentBox;
            }
        }
        /// <summary>
        /// Gets the list apply button.
        /// </summary>
        /// <value>
        /// The list apply button.
        /// </value>
        public Button ListApplyButton
        {
            get
            {
                return listApplyButton;
            }
        }
        /// <summary>
        /// Gets the list cancel button.
        /// </summary>
        /// <value>
        /// The list cancel button.
        /// </value>
        public Button ListCancelButton
        {
            get
            {
                return listCancelButton;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ListFormatGrid"/> class.
        /// </summary>
        public ListFormatGrid()
        {
            this.InitializeComponent();
            ObservableCollection<string> FontNames = new ObservableCollection<string>()
                {
                    "Arial", "Arial Black", "Calibri", "Comic Sans MS", "Courier New", "Georgia",
                    "Lucida Sans Unicode", "Portable User Interface", "Times New Roman", "Trebuchet MS",
                    "Verdana", "Webdings" , "Symbol" , "Wingdings"
                };
            fontFamilyBox.ItemsSource = FontNames;
            listLevelBox.SelectedIndex = 0;
        }
        #endregion

        #region Implementaion
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            listLevelBox = null;
            levelPatternBox = null;
            numberFormatBox = null;
            followCharacterBox = null;
            startAtBox = null;
            restartLevelBox = null;
            fontFamilyBox = null;
            fontStyleBox = null;
            fontSizeBox = null;
            singleStrikeThroughButton = null;
            doubleStrikeThroughButton = null;
            underlineButton = null;
            superscriptButton = null;
            subscriptButton = null;
            firstlineIndentBox = null;
            leftIndentBox = null;
        }
        #endregion
    }
}
