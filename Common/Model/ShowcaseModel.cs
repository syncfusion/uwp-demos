using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Common
{
     public class ShowcaseModel
    {
        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get
            {
                return imageSource;
            }

            set
            {
                imageSource = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        private string title;

        private string description;
    }
}
