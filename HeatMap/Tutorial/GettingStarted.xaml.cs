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
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HeatMap
{
    public partial class GettingStarted1:SampleLayout
    {
        public GettingStarted1()
        {
            this.InitializeComponent();
            PopulateData();
            this.DataContext = productList;
        }

        private ObservableCollection<ProductInfoModel> productList = new ObservableCollection<ProductInfoModel>();

        private void PopulateData()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                ProductInfoModel productInfo = new ProductInfoModel();
                productInfo.ProductName = productName[i];
                productInfo.Y2007 = r.Next(0, 51);
                productInfo.Y2008 = r.Next(0, 51);
                productInfo.Y2009 = r.Next(0, 51);
                productInfo.Y2010 = r.Next(0, 51);
                productInfo.Y2011 = r.Next(0, 51);
                productInfo.Y2012 = r.Next(0, 51);
                productInfo.Y2013 = r.Next(0, 51);
                productInfo.Y2014 = r.Next(0, 51);
                productInfo.Y2015 = r.Next(0, 51);
                productInfo.Y2016 = r.Next(0, 51);
                this.productList.Add(productInfo);
            }
        }

        string[] productName = new string[]
        {
            "Alice Mutton",
            "Boston Crab Meat",
            "Raclette Courdavault",
            "Gorgonzola Telino",
            "Chartreuse verte",
            "Fløtemysost",
            "Carnarvon Tigers",
            "Vegie-spread",
            "Tarte au sucre",
            "Konbu",
            "Valkoinen suklaa",
            "Perth Pasties",
            "Vegie-spread",
            "Tofu",
        };

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
