using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.GroupBar
{
    public class MailCategory
    {
        public string Header { get; set; }

        private ObservableCollection<MailItem> _mailCollection;

        public ObservableCollection<MailItem> MailCollection
        {
            get { return _mailCollection; }
            set { _mailCollection = value; }
        }
    }
}
