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
    /// Represents the ReviewOptionsGrid control.
    /// </summary>
    public sealed partial class ReviewOptionsGrid : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets the show comments button.
        /// </summary>
        /// <value>
        /// The show comments button.
        /// </value>
        public Button ShowCommentsButton
        {
            get
            {
                return showCommentsButton;
            }
        }
        /// <summary>
        /// Gets the new comment button.
        /// </summary>
        /// <value>
        /// The new comment button.
        /// </value>
        public Button NewCommentButton
        {
            get
            {
                return newCommentButton;
            }
        }
        /// <summary>
        /// Gets the delete comment button.
        /// </summary>
        /// <value>
        /// The delete comment button.
        /// </value>
        public Button DeleteCommentButton
        {
            get
            {
                return deleteCommentButton;
            }
        }
        /// <summary>
        /// Gets the previous comment button.
        /// </summary>
        /// <value>
        /// The previous comment button.
        /// </value>
        public Button PreviousCommentButton
        {
            get
            {
                return previousCommentButton;
            }
        }
        /// <summary>
        /// Gets the next comment button.
        /// </summary>
        /// <value>
        /// The next comment button.
        /// </value>
        public Button NextCommentButton
        {
            get
            {
                return nextCommentButton;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewOptionsGrid"/> class.
        /// </summary>
        public ReviewOptionsGrid()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            showCommentsButton = null;
            newCommentButton = null;
            deleteCommentButton = null;
            previousCommentButton = null;
            nextCommentButton = null;
        }
        #endregion
    }
}
