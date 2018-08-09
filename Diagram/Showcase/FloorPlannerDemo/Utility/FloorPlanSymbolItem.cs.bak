using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Syncfusion.SampleBrowser.UWP.Diagram;

namespace FloorPlannerDemo
{
    
    public class FloorPlanSymbolItem : ISymbol
    {
        public object Symbol
        {
            get;
            set;
        }

        public DataTemplate SymbolTemplate
        {
            get;
            set;
        }

        public string GroupName { get; set; }


        public ISymbol Clone()
        {
            return new FloorPlanSymbolItem()
            {
                Symbol = this.Symbol,
                SymbolTemplate = this.SymbolTemplate
            };
        }


        public object Key
        {
            get;
            set;
        }
    }

    public class SymbolCollection : ObservableCollection<ISymbol>
    {

    }

    public class StencilDataTemplator : DataTemplateSelector
    {
       
        protected override DataTemplate SelectTemplateCore
           (object item, DependencyObject container)
        {
            var resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("ms-appx:///Template/FloorPlanDictionary.xaml", UriKind.Absolute)
            };

            if (item != null && item is FloorPlannerViewModel)
            {
                if ((item as FloorPlannerViewModel).ValueType == "Prop")
                {
                    return resourceDictionary["floorplanstencil"] as DataTemplate;
                }
                else if ((item as FloorPlannerViewModel).ValueType == "Shape")
                {
                    return resourceDictionary["shapestencil"] as DataTemplate;
                }
                else if ((item as FloorPlannerViewModel).ValueType == "Text")
                {
                    return resourceDictionary["FloorPlanText"] as DataTemplate;
                }               
                return null;
            }
           
            return null;
        }

       
    }

    public static class FrameworkElementExtensions
    {
        public static FrameworkElement FindDescendantByName(this FrameworkElement element, string name)
        {
            if (element == null || string.IsNullOrWhiteSpace(name)) { return null; }

            if (name.Equals(element.Name, StringComparison.OrdinalIgnoreCase))
            {
                return element;
            }
            var childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                var result = (VisualTreeHelper.GetChild(element, i) as FrameworkElement).FindDescendantByName(name);
                if (result != null) { return result; }
            }
            return null;
        }
    }
}
