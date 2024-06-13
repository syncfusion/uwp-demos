#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
