using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ITileItem
    {
        string Description
        {
            get;
            set;
        }

        string Tags
        {
            get;
            set;
        }

        string Type
        {
            get;
            set;
        }

        string GroupHeader
        {
            get;
            set;
        }

        string Image
        {
            get;
            set;
        }

        string Header
        {
            get;
            set;
        }

        string Assembly
        {
            get;
            set;
        }

        int ColumnSpan
        {
            get;

            set;
        }

        int RowSpan
        {
            get;

            set;
        }

        bool EnablePreview
        {
            get; 
            
            set;
        }
    }
    public class TagHelper
    {
        public bool IsPreview { get; set; }
        public bool HasOptions { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsNew { get; set; }
        public TagHelper()
        {

        }
    }
}
