#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid
{
    /// <summary>
    /// This class represents the DataVirtualizationViewModel
    /// </summary>
    public class DataVirtualizationViewModel : IDisposable
    {
        public DataVirtualizationViewModel()
        {
            var repository= new EmployeeInfoRepository();
            viewSource = new GridVirtualizingCollectionView(repository.GetEmployeesDetails(1000000));
        }

        private VirtualizingCollectionView viewSource;

        /// <summary>
        /// Get or set the ViewSource
        /// </summary>
        public VirtualizingCollectionView ViewSource
        {
            get { return viewSource; }
            set { viewSource = value; }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (ViewSource != null)
            {
                viewSource.Dispose();
                viewSource.Clear();
                ViewSource.Dispose();
                ViewSource.Clear();
            }
        }
    }
}
