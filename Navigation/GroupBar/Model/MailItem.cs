using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.GroupBar
{
    public class MailItem
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        private string from;
        public string From
        {
            get { return from; }
            set { from = value; }
        }
        private string sender;
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        private string to;
        public string To
        {
            get { return to; }
            set { to = value; }
        }
        private string subject;
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private string content;
        public string Message
        {
            get { return content; }
            set { content = value; }
        }

        private bool isUnRead;
        public bool IsUnRead
        {
            get { return isUnRead; }
            set { isUnRead = value; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
    }
}
