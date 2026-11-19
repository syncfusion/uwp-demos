using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Mockup.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using System.ComponentModel;
using Windows.UI;
using System.Collections;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Mockup.View.Property
{

    public sealed partial class StencilView : Page
    {
        ObservableCollection<ISymbol> collecion;
        List<DuplicateItemClass> duplicatesWithIndices;
        List<string> dupList;
        List<string> ilist;
        public StencilView()
        {
            this.InitializeComponent();

            List<string> list = new List<string>() { "a", "a", "b", "b", "r", "t" };

           

            collecion = new ObservableCollection<ISymbol>();
            collecion = this.grid.Resources["symbolcollection"] as SymbolCollection;
            stencil.SymbolSource = collecion;
            //stencil.Constraints = StencilConstraints.Default;
            stencil.HeaderVisibility = Visibility.Collapsed;



            ilist = new List<string>();

            foreach(var item in stencil.SymbolSource as IEnumerable<object>)
            {
                ilist.Add((item as ISymbol).Key.ToString());
            }

            object duplicates = ilist.GroupBy(a => a).SelectMany(ab => ab.Skip(1).Take(1)).ToList();

    //        var dups = ilist.GroupBy(x => x)
    //           .Where(x => x.Count() > 1)
    //           .Select(x => x)
    //           .ToList();

    //        var xxx = ilist.Select((value, index) => new { value, index })
    //.Where(a => (duplicates as List<string>).Any(s => string.Equals(a.value, s)))
    //.Select(a => a.index);

            dupList = ilist.Select((value, index) => new { value, index })
    .Where(a => (duplicates as List<string>).Any(s => string.Equals(a.value, s)))
    .Select(a => a.value).ToList();

            var indexxx = ilist.Select((value, index) => new { value, index })
    .Where(a => (duplicates as List<string>).Any(s => string.Equals(a.value, s)))
    .Select(a => a.index).ToList();


            duplicatesWithIndices = ilist.Select((value, index) => new { value, index })
    .Where(a => (duplicates as List<string>).Any(s => string.Equals(a.value, s)))
    .Select(a => new DuplicateItemClass()
    {
        Name = a.value,
        Index = a.index
    }).ToList();

            //duplicatesWithIndices = xxx.ToList();

    //        var duplicatesWithIndices = ilist.Select((Name, Index) => new { Name, Index })
    //.GroupBy(x => x.Name)
    //.Select(xg => new
    //{
    //    Name = xg.Key,
    //    Indices = xg.Select(x => x.Index)
    //})
    //.Where(x => x.Indices.Count() > 1);

            
            //this.stencil.Constraints = StencilConstraints.Default & ~StencilConstraints.Filters;

            SymbolFilterProvider all = new SymbolFilterProvider { SymbolFilter = All, Content = "Limited Symbols" };
            SymbolFilterProvider category = new SymbolFilterProvider { SymbolFilter = Filter, Content = "Category" };

            this.stencil.SymbolFilters = new SymbolFilters()
            {
                all,
                category
            };
            this.stencil.SelectedFilter = all;
            stencil.DoubleTapped += stencil_DoubleTapped;
            //ComboBox box = new ComboBox();
            Binding bind = new Binding();
            bind.Path = new PropertyPath("SelectedFilter");
            bind.Source = stencil;
            this.SetBinding(SelectedFilterProperty, bind);
            this.Loaded += StencilView_Loaded;
            this.stencil.Constraints = this.stencil.Constraints | StencilConstraints.ShowPreview;
        }

        public void SetConstraints(StencilConstraints constraints)
        {
            stencil.Constraints = constraints;
        }
        
        TextBox searchBox;
        Button button=null ;
        ToggleButton _mAllFilter;
        ToggleButton _mCategoryFilter;
        ToggleButton _mSearchSymbol;
        Border _mSearchBorder;
        void StencilView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (SymbolVM symbol in this.stencil.SymbolSource as IEnumerable<object>)
            {
                symbol.SameGroupName();
            }
            _mAll = 0;
            SymbolFilterProvider all = new SymbolFilterProvider { SymbolFilter = All, Content = "Limited Symbols" };
            this.stencil.SelectedFilter = all;

            _mAllFilter = FindVisualChild_Depth((sender as StencilView).stencil, "PART_AllFilter") as ToggleButton;
            if (_mAllFilter != null)
                _mAllFilter.Checked += button_Checked;

            _mCategoryFilter = FindVisualChild_Depth((sender as StencilView).stencil, "PART_CategoryFilter") as ToggleButton;
            if (_mCategoryFilter != null)
                _mCategoryFilter.Checked += button_Checked;

            _mSearchSymbol = FindVisualChild_Depth((sender as StencilView).stencil, "PART_SearchSymbol") as ToggleButton;
            if (_mSearchSymbol != null)
                _mSearchSymbol.Checked += button_Checked;

            _mSearchBorder = FindVisualChild_Depth((sender as StencilView).stencil, "PART_SearchSymbolTextBox") as Border;
            searchBox = FindVisualChild_Depth((sender as StencilView).stencil, "searchbox") as TextBox;
            if (searchBox != null)
            {
                //searchBox.TextChanged += searchBox_TextChanged;
            }
        }

        void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        void button_Checked(object sender, RoutedEventArgs e)
        {            
            if (sender as ToggleButton == _mAllFilter)
            {
                _mCategoryFilter.IsChecked = false;
                _mSearchSymbol.IsChecked = false;
                stencil.HeaderVisibility = Visibility.Collapsed;

                foreach (SymbolVM symbol in this.stencil.SymbolSource as IEnumerable<object>)
                {
                    symbol.SameGroupName();
                }
                _mAll = 0;
                SymbolFilterProvider all = new SymbolFilterProvider { SymbolFilter = All, Content = "Limited Symbols" };
                this.stencil.SelectedFilter = all;
                _mSearchBorder.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else if (sender as ToggleButton == _mCategoryFilter)
            {
                _mAllFilter.IsChecked = false;
                _mSearchSymbol.IsChecked = false;

                stencil.HeaderVisibility = Visibility.Visible;
                foreach (SymbolVM symbol in this.stencil.SymbolSource as IEnumerable<object>)
                {
                    symbol.OldGroupName();
                }
                SymbolFilterProvider all = new SymbolFilterProvider { SymbolFilter = Category, Content = "Category" };
                this.stencil.SelectedFilter = all;
                _mSearchBorder.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else if (sender as ToggleButton == _mSearchSymbol)
            {
                if (searchBox != null)
                    searchBox.Text = "Search Here";
                _mAllFilter.IsChecked = false;
                _mCategoryFilter.IsChecked = false;
                stencil.HeaderVisibility = Visibility.Collapsed;
                _mSearchBorder.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            SymbolSearch((sender as TextBox).Text);

            //foreach (SymbolVM symbol in stencil.SymbolSource)
            //{
            //    if (symbol.Key.ToString().Contains((sender as TextBox).Text))
            //        symbol.SymbolSearch();
            //    else
            //        symbol.OldGroupName();
            //}

            //SymbolFilterProvider search = new SymbolFilterProvider { Filter = Filter, Content = "Search" };
            //this.stencil.SelectedFilter = search;      
        }

        void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SymbolSearch((sender as TextBox).Text);
            //if (!string.IsNullOrEmpty((sender as TextBox).Text))
            //{
            //    foreach (SymbolVM symbol in stencil.SymbolSource)
            //    {
            //        if (symbol.Key.ToString().Contains((sender as TextBox).Text))
            //            symbol.SymbolSearch();
            //        else
            //            symbol.OldGroupName();
            //    }
            //}

            //SymbolFilterProvider search = new SymbolFilterProvider { Filter = Filter, Content = "Search" };
            //this.stencil.SelectedFilter = search;
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            //searchBox.Visibility = Visibility.Visible;
            //button.Visibility = Visibility.Collapsed;
        }

        internal FrameworkElement FindVisualChild_Depth(DependencyObject dobj, string searchName) 
        {
            int cnt = VisualTreeHelper.GetChildrenCount(dobj);
            for (int i = 0; i < cnt; i++)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(dobj, i) as FrameworkElement;
                if (child is FrameworkElement && child.Name == searchName)
                {
                    return child as FrameworkElement;
                }
                else if (child != null)
                {
                    child = FindVisualChild_Depth(child, searchName);
                    if (child is FrameworkElement && child.Name == searchName)
                    {
                        return child as FrameworkElement;
                    }
                }
            }
            return null;
        }

        public SymbolFilterProvider SelectedFilter
        {
            get { return (SymbolFilterProvider)GetValue(SelectedFilterProperty); }
            set { SetValue(SelectedFilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedFilter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFilterProperty =
            DependencyProperty.Register("SelectedFilter", typeof(SymbolFilterProvider), typeof(StencilView), new PropertyMetadata(null, OnSelectedFilterChanged));


        private static void OnSelectedFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StencilView view = d as StencilView;
            if(e.NewValue != null)
            {
                if ((e.NewValue as SymbolFilterProvider).Content.ToString() == "All" || (e.NewValue as SymbolFilterProvider).Content.ToString() == "Category")
                {
                    if (view.searchBox != null)
                        view.searchBox.Visibility = Visibility.Collapsed;
                    if (view.button != null)
                        view.button.Visibility = Visibility.Visible;

                    foreach (SymbolVM symbol in view.stencil.SymbolSource as IEnumerable<object>)
                    {
                        if ((e.NewValue as SymbolFilterProvider).Content.ToString() == "All")
                        symbol.SameGroupName();
                        else if((e.NewValue as SymbolFilterProvider).Content.ToString() == "Category")
                            symbol.OldGroupName();
                    }
                    SymbolFilterProvider all = new SymbolFilterProvider { SymbolFilter = StaticAll, Content = "All" };
                    view.stencil.SelectedFilter = all;
                }           
            }
        }

        public static bool StaticAll(SymbolFilterProvider source, object symbol)
        {
            //if ((symbol.Symbol as TextBlock).Text == "AAA")
            //{
            //    return false;
            //}

            return true;
        }

        int _mAll;
        public bool All(SymbolFilterProvider source, object symbol)
        {
    //        var indexxx = dupList.Select((value, index) => new { value, index })
    //.Where(a => string.Equals(a.value, symbol.Key.ToString()))
    //.Select(a => a.index).ToList();

            if (dupList.Contains((symbol as ISymbol).Key.ToString()))
            {
                var indexxx = ilist.Select((value, index) => new { value, index })
    .Where(a => string.Equals(a.value, (symbol as ISymbol).Key.ToString()))
    .Select(a => a.index).ToList();
                if(indexxx[0] == _mAll)
                {
                    _mAll++;
                    return true;
                }
                else
                {
                    _mAll++;
                    return false;
                }
            }
            else
	        {
                _mAll++;
                return true;
	        }
        }

        public bool Category(SymbolFilterProvider source, object symbol)
        {
            //if ((symbol.Symbol as TextBlock).Text == "AAA")
            //{
            //    return false;
            //}

            return true;
        }

        void stencil_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
        }

        private void GetParent(object sender, out Grid g)
        {
            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);

            if (parent.GetType() == typeof(Grid))
            {
                if ((parent as Grid).Name == "rootgrid")
                {
                    g = (parent as Grid);
                }
                else
                {
                    GetParent(parent, out g);
                }
            }

            else
            {
                GetParent(parent, out g);
            }
        }

        

        public string MappingName
        {
            get { return (string)GetValue(MappingNameProperty); }
            set { SetValue(MappingNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MappingName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MappingNameProperty =
            DependencyProperty.Register("MappingName", typeof(string), typeof(StencilView), new PropertyMetadata("GroupName"));

        

        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //(stencil.SymbolSource as IEnumerable<ISymbol>).ToList().Clear();
            //foreach (SymbolVM symbol in collecion)
            //{
            //    if (symbol.Key.ToString().Contains((sender as TextBox).Text))
            //        (stencil.SymbolSource as IEnumerable<ISymbol>).ToList().Add(symbol);
            //    //else
            //    //    symbol.Visibility = Visibility.Collapsed;
            //}
            //SymbolFilterProvider chart = new SymbolFilterProvider { Filter = Filter, Content = "Chart" };
            //this.stencil.SelectedFilter = chart;
            //stencil.InvalidateGroupingFiltering();

            //this.stencil.SearchSymbol((sender as TextBox).Text);
            searchedSymbols = new List<string>();
            foreach (SymbolVM symbol in stencil.SymbolSource as IEnumerable<object>)
            {
                if (symbol.Key.ToString().Contains((sender as TextBox).Text))
                {
                    symbol.SymbolSearch();
                    searchedSymbols.Add(symbol.Key.ToString());
                }
                else
                    symbol.OldGroupName();
            }

            SymbolFilterProvider search = new SymbolFilterProvider { SymbolFilter = Filter, Content = "Search" };
            this.stencil.SelectedFilter = search;
            searchedSymbols = null;
        }

        List<string> searchedSymbols;

        public bool Filter(SymbolFilterProvider source, object symbol)
        {
            if ((symbol as SymbolVM).GroupName.Equals(source.Content.ToString()))
            {
                if (dupList.Contains((symbol as ISymbol).Key.ToString()))
                {
                    if (searchedSymbols.Contains((symbol as ISymbol).Key.ToString()))
                    {
                        var indexxx = searchedSymbols.Select((value, index) => new { value, index })
                            .Where(a => string.Equals(a.value, (symbol as ISymbol).Key.ToString()))
                            .Select(a => a.index).ToList();

                        bool flag=false;
                        for (int i = indexxx.Count; i > 1; i--)
                        {
                            searchedSymbols.RemoveAt(indexxx[i - 1]);
                            flag = true;
                        }
                        if (flag)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return true;
            }
            return false;            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //searchbox.Text = "";
            //searchbox.Visibility = Visibility.Visible;
        }

        public void SymbolCategory(string category)
        {
            foreach (SymbolVM symbol in stencil.SymbolSource as IEnumerable<object>)
            {
                if (category == "All")
                {
                    symbol.SameGroupName();
                }
                else if (category == "Category")
                {
                    symbol.OldGroupName();
                }
            }

            //SymbolFilterProvider all = new SymbolFilterProvider { Filter = All, Content = "All" };
            //this.stencil.SelectedFilter = all;
        }

        public void SymbolSearch(string text)
        {
            searchedSymbols = new List<string>();
            foreach (SymbolVM symbol in stencil.SymbolSource as IEnumerable<object>)
            {
                bool contains = System.Text.RegularExpressions.Regex.IsMatch(symbol.Key.ToString(), text, System.Text.RegularExpressions.RegexOptions.IgnoreCase); 
                //if (symbol.Key.ToString().Contains(text) && !string.IsNullOrEmpty(text))
                if (contains && !string.IsNullOrEmpty(text))
                {
                    symbol.SymbolSearch();
                    searchedSymbols.Add(symbol.Key.ToString());
                }
                else
                    symbol.OldGroupName();
            }
            SymbolFilterProvider search = new SymbolFilterProvider { SymbolFilter = Filter, Content = "Search" };
            this.stencil.SelectedFilter = search;
        }

        public void All()
        {
            _mAllFilter.IsChecked = true;
            //stencil.HeaderVisibility = Visibility.Collapsed;
            //foreach (SymbolVM symbol in this.stencil.SymbolSource)
            //{
            //    symbol.SameGroupName();
            //}
            //SymbolFilterProvider all = new SymbolFilterProvider { Filter = All, Content = "All" };
            //this.stencil.SelectedFilter = all;
        }

        public void Category()
        {
            _mCategoryFilter.IsChecked = true;
            //stencil.HeaderVisibility = Visibility.Visible;
            //foreach (SymbolVM symbol in this.stencil.SymbolSource)
            //{
            //    symbol.OldGroupName();
            //}
            //SymbolFilterProvider all = new SymbolFilterProvider { Filter = All, Content = "Category" };
            //this.stencil.SelectedFilter = all;
        }

        public void SearchSymbol()
        {
            _mSearchSymbol.IsChecked = true;
            //foreach (SymbolVM symbol in stencil.SymbolSource)
            //{
            //    if (symbol.Key.ToString().Contains(symbolName))
            //        symbol.SymbolSearch();
            //    else
            //        symbol.OldGroupName();
            //}

            //SymbolFilterProvider search = new SymbolFilterProvider { Filter = Filter, Content = "Search" };
            //this.stencil.SelectedFilter = search;            
        }

        private void searchallbutton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void searchbox_GotFocus(object sender, RoutedEventArgs e)
        {
            //if ((e.OriginalSource as TextBox).FocusState == Windows.UI.Xaml.FocusState.Unfocused)
            //    VisualStateManager.GoToState(sender as TextBox, "UnFocused", true);
            //else
            //    VisualStateManager.GoToState(sender as TextBox, "Focused", true);
        }

        private void header_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void header_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void searchbox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = new SolidColorBrush(Colors.White);
            (sender as TextBox).Foreground = GetColorFromHexa("#414042");
            if ((sender as TextBox).Text.Equals("Search Here"))
                (sender as TextBox).Text = string.Empty;
        }

        private void searchbox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = GetColorFromHexa("#2e3138");
            (sender as TextBox).Foreground = GetColorFromHexa("#808285");
        }

        public SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    255,
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16)
                )
            );
        }

        private void PART_SearchSymbol_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as ToggleButton, "MouseEnter", true);
        }

        private void PART_SearchSymbol_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked.Value)
                VisualStateManager.GoToState(sender as ToggleButton, "MouseLeaveOnChecked", true);
            else
                VisualStateManager.GoToState(sender as ToggleButton, "MouseLeaveOnUnChecked", true);
        }

        #region Stencil Tab checked event 
        private void PART_AllFilter_Checked(object sender, RoutedEventArgs e)
        {
            if((sender as ToggleButton).Name.Equals("PART_AllFilter"))
            {
                if (_mCategoryFilter != null)
                    _mCategoryFilter.IsChecked = false;
                if (_mSearchSymbol != null)
                    _mSearchSymbol.IsChecked = false;
            }
            else if ((sender as ToggleButton).Name.Equals("PART_CategoryFilter"))
            {
                _mAllFilter.IsChecked = false;
                _mSearchSymbol.IsChecked = false;
            }
            else if ((sender as ToggleButton).Name.Equals("PART_SearchSymbol"))
            {
                _mAllFilter.IsChecked = false;
                _mCategoryFilter.IsChecked = false;
                if(searchBox != null)
                    searchBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
        #endregion
    }

    class DuplicateItemClass
    {
        public string Name { get; set; }
        public int Index { get; set; }
    }
}

