using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    public class StockDatas
    {
        public StockDatas()
        {

        }

        public string Name { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Open { get; set; }

        public double Last { get; set; }

        public double Volume { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
