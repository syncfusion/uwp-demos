using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationalChartDemo
{
   public class OrgChartDiagram:SfDiagram
    {
       public Syncfusion.UI.Xaml.Diagram.Selector SFSelector = new Syncfusion.UI.Xaml.Diagram.Selector();
       protected override Syncfusion.UI.Xaml.Diagram.Selector GetSelectorForItemOverride(object item)
       {
           return SFSelector;
       }
    }
}
