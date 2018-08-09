using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Windows.UI.Xaml;

namespace DiagramBuilder.ViewModel
{
    public class SymbolVM : ISymbol
    {
            static Random r = new Random();

        public SymbolVM()
        {
        }

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
            return new SymbolVM()
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


    public class SymbolCollection : ObservableCollection<object>
    {

    }
}
