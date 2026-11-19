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
