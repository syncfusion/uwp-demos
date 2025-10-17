#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Category 
    {
        private String categoryName;

        public String CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public double Height { get; set; }

        public bool IsExpand { get; set; }

        private ObservableCollection<ITileItem> products;

        public ObservableCollection<ITileItem> Products
        {
            get { return products; }
            set { products = value;  }
        }

        public Category()
        {
            Products = new ObservableCollection<ITileItem>();
        }
    }

}
