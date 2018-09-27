using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.Calculate
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {

            // Product Showcase
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CalculateSamples.ArrayIcalcDataDemo).AssemblyQualifiedName,
                Header = "ArrayICalc",
                ProductIcons="Icons/Calculate.png",
                SearchKeys = new string[] {"Array","Formula","Calculate"},
                Product = "Calculate",
                Category = Categories.Miscellaneous,
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(CalculateSamples.ComputeFormulaDemo).AssemblyQualifiedName,
                Header = "Compute Formula",
                SearchKeys = new string[] { "Compute", "Formula", "Calculate" },
                Product = "Calculate",
                Category = Categories.Miscellaneous,
            });
        }
    }
}
