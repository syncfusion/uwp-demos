using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.SfTreeGrid
{
    public class TreeViewModel : IDisposable
    {
        public TreeViewModel()
        {
            this.ItemsCollection = this.GetItems();
        }

        private ObservableCollection<TreeModel> _itemsCollection;
        /// <summary>
        /// Gets or sets the employee info.
        /// </summary>
        /// <value>The employee info.</value>
        public ObservableCollection<TreeModel> ItemsCollection
        {
            get
            {
                return _itemsCollection;
            }
            set
            {
                _itemsCollection = value;
            }
        }

        public ObservableCollection<TreeModel> GetItems()
        {
            ObservableCollection<TreeModel> items = new ObservableCollection<TreeModel>();
            items.Add(new TreeModel() { Name = "Inbox" });
            items.Add(new TreeModel() { Name = "Drafts" });
            items.Add(new TreeModel() { Name = "Sent Items" });
            items.Add(new TreeModel() { Name = "Deleted Items" });
            items.Add(new TreeModel() { Name = "Calendar" });
            items.Add(new TreeModel() { Name = "Contacts" });
            return items;
        }

        public ObservableCollection<TreeModel> GetSubItems(string name)
        {
            ObservableCollection<TreeModel> items = new ObservableCollection<TreeModel>();
            if (name == "Inbox")
            {
                items.Add(new TreeModel() { Name = "Development" });
                items.Add(new TreeModel() { Name = "HR Team" });
                items.Add(new TreeModel() { Name = "Team meeting" });
            }
            else if (name == "Drafts")
            {
                items.Add(new TreeModel() { Name = "Template" });
                items.Add(new TreeModel() { Name = "Personal" });
            }

            else if (name == "Sent Items")
            {
                items.Add(new TreeModel() { Name = "Team" });
                items.Add(new TreeModel() { Name = "Others" });
            }
            else if (name == "Deleted Items")
            {
            }
            else if (name == "Calendar")
            {
                items.Add(new TreeModel() { Name = "Team" });
                items.Add(new TreeModel() { Name = "My Calendar" });
            }
            else if (name == "Contacts")
            {
                items.Add(new TreeModel() { Name = "Team" });
                items.Add(new TreeModel() { Name = "Suggested" });
                items.Add(new TreeModel() { Name = "Others" });
            }
            return items;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isdisposable)
        {
            if (this.ItemsCollection != null)
            {
                this.ItemsCollection.Clear();
            }
        }
    }
}
