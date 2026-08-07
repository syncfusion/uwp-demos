using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.HubTile
{
    public class HubTileProperties
    {
        private static string header;

        public static  string Header
        {
            get
            {
                return header;
            }

            set
            {
                header = value;
            }
        }

        public static  string Title
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

        private static  string title;
    }
}
