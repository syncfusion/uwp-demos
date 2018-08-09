using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;
using Common;

namespace Navigation.TreeNavigator
{
    public class TreeViewModel : NotificationObject
    {
        private List<TreeModel> models;

        public List<TreeModel> Models
        {
            get { return models; }
            set { models = value; }
        }

        public TreeViewModel()
        {
            Models = new List<TreeModel>();
        }
    }
}
