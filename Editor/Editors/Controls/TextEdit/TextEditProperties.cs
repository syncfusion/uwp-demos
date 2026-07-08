using Common;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editors
{
    public class TextBoxExtProperties : NotificationObject
    {
        private string watermark = "Type here";

        public string Watermark
        {
            get { return watermark; }
            set { watermark = value; RaisePropertyChanged("Watermark"); }
        }

    }
}
