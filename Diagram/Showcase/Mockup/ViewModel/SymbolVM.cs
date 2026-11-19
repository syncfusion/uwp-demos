using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Mockup.ViewModel
{
    public class SymbolVM : ISymbol
    {
        static Random r = new Random();

        public SymbolVM()
        {
            //if (r.Next(0, 10) < 5)
            //{
            //    AAA = "1";
            //}
            //else
            //{
            //    AAA = null;
            //}
        }

        public string SymbolName { get; set; }

        public object Symbol
        {
            get;
            set;
        }

        //public object AAA { get; set; }

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
        
        private string _mOldGroupName;

        public void SameGroupName()
        {
            if (GroupName != "All" && GroupName != "Search")
                _mOldGroupName = GroupName;
            GroupName = "All";
        }
        
        public void OldGroupName()
        {
            if(!string.IsNullOrEmpty(_mOldGroupName))
                GroupName = _mOldGroupName;
        }

        public void SymbolSearch()
        {
            if (GroupName != "All" && GroupName != "Search")
                _mOldGroupName = GroupName;
            GroupName = "Search";
        }
    }


    public class SymbolCollection : ObservableCollection<ISymbol>
    {

    }
}
