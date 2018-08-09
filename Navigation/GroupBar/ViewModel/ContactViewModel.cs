using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.GroupBar
{
    public class ContactViewModel
    {
        private List<Contact> contacts;

        public List<Contact> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public ContactViewModel()
        {
            Contacts = GetContacts();
        }
        private List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact() { Name = "Alfreds", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Maria", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Anders", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Thomas", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Hardy", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Christina", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Berglunds", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Berguvsvägen", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Anil", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Victoria", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Peter", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Robert", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Red", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Brandon", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Charles", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Arya", PhoneNumber = "" });
            contacts.Add(new Contact() { Name = "Tom", PhoneNumber = "" });
            contacts.Sort();
            return contacts;
        }
    }
}
