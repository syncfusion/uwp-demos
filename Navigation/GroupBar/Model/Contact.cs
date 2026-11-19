using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.GroupBar
{
    public class Contact : IComparable<Contact>
    {
        private string contactName;
        public string Name
        {
            get { return contactName; }
            set { contactName = value; }
        }

        private string contactNumber;

        public string PhoneNumber
        {
            get { return contactNumber; }
            set { contactNumber = value; }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public int CompareTo(Contact other)
        {
            return string.Compare(this.Name, other.Name);
        }
    }
}
