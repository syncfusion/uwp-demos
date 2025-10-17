#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
    /// Represents the InsertTableGrid control.
    /// </summary>
    public sealed partial class InsertTableGrid : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets the row size box.
        /// </summary>
        /// <value>
        /// The row size box.
        /// </value>
        public ComboBox RowSizeBox
        {
            get
            {
                return rowSizeBox;
            }
        }
        /// <summary>
        /// Gets the column size box.
        /// </summary>
        /// <value>
        /// The column size box.
        /// </value>
        public ComboBox ColumnSizeBox
        {
            get
            {
                return columnSizeBox;
            }
        }
        /// <summary>
        /// Gets the table insert button.
        /// </summary>
        /// <value>
        /// The table insert button.
        /// </value>
        public Button TableInsertButton
        {
            get
            {
                return tableInsertButton;
            }
        }
        /// <summary>
        /// Gets the table cancel button.
        /// </summary>
        /// <value>
        /// The table cancel button.
        /// </value>
        public Button TableCancelButton
        {
            get
            {
                return tableCancelButton;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertTableGrid"/> class.
        /// </summary>
        public InsertTableGrid()
        {
            this.InitializeComponent();
            rowSizeBox.SelectedIndex = 0;
            columnSizeBox.SelectedIndex = 0;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            rowSizeBox = null;
            columnSizeBox = null;
            tableInsertButton = null;
            tableCancelButton = null;
        }
        #endregion
    }
}
