#region Copyright
//  Copyright (c) Syncfusion Inc. 2001 - 2018. All rights reserved.
//  Use of this code is subject to the terms of our license.
//  A copy of the current license can be obtained at any time by e-mailing
//  licensing@syncfusion.com. Re-distribution in any form is strictly
//  prohibited. Any infringement will be prosecuted under applicable laws. 
// </copyright>
#endregion

namespace BI.PivotClient
{
    using Windows.Globalization;
    using Common;
    using ViewModel = Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.ViewModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GettingStarted : SampleLayout
    {
        #region Constructor

        public GettingStarted()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            InitializeComponent();
        }

        #endregion

        #region Dispose Method

        public sealed override void Dispose()
        {
            if (pivotClient1 != null)
                pivotClient1.Dispose();
            pivotClient1 = null;

            if (DataContext is ViewModel.ViewModel)
                (DataContext as ViewModel.ViewModel).Dispose();
            DataContext = null;

            base.Dispose();
        }

        #endregion
    }
}