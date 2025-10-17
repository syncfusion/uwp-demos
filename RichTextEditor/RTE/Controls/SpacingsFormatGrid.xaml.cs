#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Controls.Input;
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
    /// Represents the SpacingsFormatGrid control.
    /// </summary>
    public sealed partial class SpacingsFormatGrid : UserControl
    {
        #region Properties
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
        /// Gets the right indent box.
        /// </summary>
        /// <value>
        /// The right indent box.
        /// </value>
        public SfNumericUpDown RightIndentBox
        {
            get
            {
                return rightIndentBox;
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
        /// Gets the line spacing box.
        /// </summary>
        /// <value>
        /// The line spacing box.
        /// </value>
        public SfNumericUpDown LineSpacingBox
        {
            get
            {
                return lineSpacingBox;
            }
        }
        /// <summary>
        /// Gets the before spacing box.
        /// </summary>
        /// <value>
        /// The before spacing box.
        /// </value>
        public SfNumericUpDown BeforeSpacingBox
        {
            get
            {
                return beforeSpacingBox;
            }
        }
        /// <summary>
        /// Gets the after spacing box.
        /// </summary>
        /// <value>
        /// The after spacing box.
        /// </value>
        public SfNumericUpDown AfterSpacingBox
        {
            get
            {
                return afterSpacingBox;
            }
        }
        /// <summary>
        /// Gets the line spacing type box.
        /// </summary>
        /// <value>
        /// The line spacing type box.
        /// </value>
        public ComboBox LineSpacingTypeBox
        {
            get
            {
                return lineSpacingTypeBox;
            }
        }
        /// <summary>
        /// Gets the spacings apply button.
        /// </summary>
        /// <value>
        /// The spacings apply button.
        /// </value>
        public Button SpacingsApplyButton
        {
            get
            {
                return spacingsApplyButton;
            }
        }
        /// <summary>
        /// Gets the spacings cancel button.
        /// </summary>
        /// <value>
        /// The spacings cancel button.
        /// </value>
        public Button SpacingsCancelButton
        {
            get
            {
                return spacingsCancelButton;
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Initializes a new instance of the <see cref="SpacingsFormatGrid"/> class.
        /// </summary>
        public SpacingsFormatGrid()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            leftIndentBox = null;
            rightIndentBox = null;
            firstlineIndentBox = null;
            lineSpacingBox = null;
            lineSpacingTypeBox = null;
            afterSpacingBox = null;
            beforeSpacingBox = null;
            spacingsApplyButton = null;
            spacingsCancelButton = null;
        }
        #endregion
    }
}
