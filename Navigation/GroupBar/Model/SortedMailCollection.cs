using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.GroupBar
{
    public class SortedMailCollection
    {
        private string header;

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        private ObservableCollection<MailItem> mailCollection = new ObservableCollection<MailItem>();
        public ObservableCollection<MailItem> MailCollection
        {
            get { return mailCollection; }
            set { mailCollection = value; }
        }

    }
}
