#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Rotator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;

namespace Common
{
    public class ViewModel
    {
     

        public ObservableCollection<Category> Categories { get; set; }

        public List<GroupInfoList<ITileItem>> Tiles { get; set; }


        public List<SampleInfo> FeaturedSamples { get; set; }

        public ObservableCollection<Search> SearchResult { get; set; }

        public List<SampleInfo> ShowcaseSamples { get; set; }

        public Visibility SidePanelVisibility { get; set; }

        public Visibility WhatsNewVisibility { get; set; }

        public Visibility ShowcaseVisibility { get; set; }

        public string Header { get; set; }


        public List<SampleInfo> WhatsNewSamples { get; set; }

        public List<Search> SearchCollection { get; set; }

        public List<SampleInfo> SearchViewCollection { get; set; }

        public string WhatsNewDescription { get; set; }

        public ViewModel()
        {

            ShowcaseSamples = new List<SampleInfo>();
            SearchCollection = new List<Search>();
            SearchViewCollection = new List<SampleInfo>();
            WhatsNewSamples = new List<SampleInfo>();
            Tiles = new List<GroupInfoList<ITileItem>>();
            FeaturedSamples = new List<SampleInfo>();

            ShowcaseVisibility = WhatsNewVisibility = Visibility.Collapsed;
            WhatsNewDescription = "The following new samples have been added in the Volume 3 2025 release.";

            Categories = new ObservableCollection<Category>();

            foreach (var sample in SampleHelper.SampleViews)
            {
                if (sample.SampleType == SampleType.Showcase)
                {
                    ShowcaseSamples.Add(sample);
                    ShowcaseVisibility = Visibility.Visible;

                }
                else
                {
                    FeaturedSamples.Add(sample);

                    SearchCollection.Add(new Search()
                    {
                        ProductName = sample.Product,
                        SampleName = sample.Header,
                        SearchName = sample.Category + sample.Header });

                    SearchViewCollection.Add(new SampleInfo()
                    {
                        Product = sample.Product,
                        Header = sample.Header,
                        SampleView = sample.SampleView,
                        SampleCategory = sample.SampleCategory,
                        SampleType = sample.SampleType,
                        Description=sample.Description,
                        DesktopImage=sample.DesktopImage,
                        HasOptions=sample.HasOptions,
                        Category=sample.Category,
                        Tag=sample.Tag,
                        MobileImage=sample.MobileImage,
                        SearchKeys = new string[] { sample.Category.ToString(), sample.Header, sample.Product }
                    });

                    if (sample.Tag == Tags.NewWithWhatsNew || sample.Tag == Tags.PreviewWithWhatsNew || sample.Tag == Tags.UpdatedWithWhatsNew)
                    {
                        WhatsNewSamples.Add(sample);
                        WhatsNewVisibility = Visibility.Visible;
                        
                    }
                }
            }
           

            if (ShowcaseVisibility == Visibility.Collapsed && this.WhatsNewVisibility == Visibility.Collapsed)
                this.SidePanelVisibility = Visibility.Collapsed;

            MainCollection = new AllProductsCategory();

            AllProductsCategories = new Dictionary<string, Products>();
            foreach (var sample in FeaturedSamples)
            {
                if (AllProductsCategories != null && AllProductsCategories.ContainsKey(categName(sample.Category.ToString())))
                {
                    var currentProduct = AllProductsCategories[categName(sample.Category.ToString())];
                    GetProducts(currentProduct, sample);

                }
                else
                {
                    
                    var newProducts = new Products();
                    newProducts.Name = sample.Product;
                    newProducts.AllProducts = new Dictionary<string, FeatureSampleCategory>();
                    GetProducts(newProducts, sample);
             

                    AllProductsCategories.Add(categName( sample.Category.ToString()), newProducts);
                }
            }

            WhatNewCategories = new Dictionary<string, FeatureSampleCollection>();
            foreach (var sample in WhatsNewSamples)
            {
                if (WhatNewCategories != null && WhatNewCategories.ContainsKey(sample.Product))
                {
                    var currentProduct = WhatNewCategories[sample.Product];
                    currentProduct.Name = sample.Header;
                    currentProduct.AllSamples.Add(sample);
                }
                else
                {
                    var newSamples = new FeatureSampleCollection();
                    newSamples.Name = sample.Header;
                    newSamples.AllSamples = new List<SampleInfo>();
                    newSamples.AllSamples.Add(sample);
                    WhatNewCategories.Add(sample.Product, newSamples);
                }
            }

        }


        private string categName(string name)
        {
            var categ = name;
            if (categ == "DataVisualization")
                categ = "Data Visualization";
            else if (categ == "FileFormat")
                categ = " ";
            else if (categ == "DataScience")
                categ = "Data Science";
            else if (categ == "BusinessIntelligence")
                categ = "Business Intelligence";

            return categ;
        }

        public void GetProducts(Products currentProducts, SampleInfo sample)
        {
            
            if (currentProducts.AllProducts != null&& currentProducts.AllProducts.ContainsKey(sample.Product))
            {
                var currentSampleCategories = currentProducts.AllProducts[sample.Product];
                if (sample.ProductIcons!=null)
                currentSampleCategories.ProductIcon = sample.ProductIcons;
                setSubCategory(currentSampleCategories, sample);
            }
            else
            {
               
                FeatureSampleCategory newSampleCategory = new FeatureSampleCategory();
                newSampleCategory.Name = sample.Product;
                if (SampleHelper.NewProduct!=null && SampleHelper.NewProduct.Contains(sample.Product))
                    newSampleCategory.ProductTag = Tags.New;
                else if (SampleHelper.PreviewProduct!=null && SampleHelper.PreviewProduct.Contains(sample.Product))
                    newSampleCategory.ProductTag = Tags.Preview;
                else if (SampleHelper.UpdatedProduct!=null && SampleHelper.UpdatedProduct.Contains(sample.Product))
                    newSampleCategory.ProductTag = Tags.Updated;
                if (sample.ProductIcons != null)
                    newSampleCategory.ProductIcon = sample.ProductIcons;
                newSampleCategory.AllSampleCategory = new Dictionary<string, FeatureSampleCollection>();
                setSubCategory(newSampleCategory, sample);
                currentProducts.AllProducts.Add(sample.Product, newSampleCategory);
                
            }
        }

        public void setSubCategory(FeatureSampleCategory currentSampleCategories, SampleInfo sample)
        {
            
            if (currentSampleCategories.AllSampleCategory != null && currentSampleCategories.AllSampleCategory.ContainsKey(sample.SampleCategory))
            {
                var FeatureSamples = currentSampleCategories.AllSampleCategory[sample.SampleCategory];
                FeatureSamples.AllSamples.Add(sample);

            }
            else
            {
                
                FeatureSampleCollection newSamples = new FeatureSampleCollection();
                newSamples.Name = sample.Header;
                newSamples.AllSamples = new List<SampleInfo>();
                newSamples.AllSamples.Add(sample);
                currentSampleCategories.AllSampleCategory.Add(sample.SampleCategory, newSamples);
           
            }
        }

        public AllProductsCategory MainCollection { get; set; }


        public Dictionary<string, Products> AllProductsCategories { get; set; }

        public Dictionary<string, FeatureSampleCollection> WhatNewCategories { get; set; }
     

    }

    public class AllProductsCategory
    {
        public String Name { get; set; }
        public Dictionary<string, Products> AllProductsCategories { get; set; }
    }

    public class Products
    {
        public String Name { get; set; }
        public Dictionary<string, FeatureSampleCategory> AllProducts { get; set; }

    }

    public class FeatureSampleCategory: NotificationObject
    {
        public String Name { get; set; }

        public Tags ProductTag { get; set; }

        public string ProductIcon { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
        public Dictionary<string, FeatureSampleCollection> AllSampleCategory { get; set; }
    }

    public class FeatureSampleCollection
    {
        public String Name { get; set; }
        public List<SampleInfo> AllSamples { get; set; }
    }

    public static class DemoCommon
    {
        public static string FindLicenseKey(string licensePath)
        {
            string licensetext = "";
            string licensefile = "SyncfusionLicense.txt";
            if (Application.Current != null)
            {
                Assembly CurrentAssembly = Application.Current.GetType().GetTypeInfo().Assembly;
                Stream stream = CurrentAssembly.GetManifestResourceStream(CurrentAssembly.GetName().Name + "." + licensefile);
                if (stream != null)
                {
                    stream.Position = 0;
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            licensetext = reader.ReadToEnd();
                        }
                    }
                }
            }
            return licensetext;
        }
    }

}
