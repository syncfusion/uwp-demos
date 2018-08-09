
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
    using Common;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Windows.Globalization;
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Relational : SampleLayout
    {
        #region Private members

        Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.ViewModel.ViewModel vm;

        #endregion

        #region Constructor

        public Relational()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
            this.InitializeComponent();
            vm = new Syncfusion.SampleBrowser.UWP.PivotClient.PivotClient.ViewModel.ViewModel();
            pivotClient.ItemsSource = vm.ProductSalesData;
        }

        #endregion

        #region Overriden Public method

        public sealed override void Dispose()
        {
            if (pivotClient != null)
                pivotClient.Dispose();
            pivotClient = null;
            if (vm != null)
                vm.Dispose();
            vm = null;
            base.Dispose();
        }

        #endregion
    }
}