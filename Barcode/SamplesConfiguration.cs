using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.Barcode
{
    public class SamplesConfiguration
    {
        public SamplesConfiguration()
        {

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Codabar).AssemblyQualifiedName,
                Header = "Codabar",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
				ProductIcons = "Icons/barcode.png",
                SearchKeys = new string[] { "Codabar", "Coda", "bar" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Code11).AssemblyQualifiedName,
                Header = "Code11",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code11", "code", "11" }
            });
#if (!STORE_SB)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.UpcBarcode).AssemblyQualifiedName,
                Header = "UpcBarcode",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "UpcBarcode","Upc","Barcode" }
            });
#endif
#if (!STORE_SB)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Code128A).AssemblyQualifiedName,
                Header = "Code128A",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code128A", "128A", "code" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Code128B).AssemblyQualifiedName,
                Header = "Code128B",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code128B", "128B", "code" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView =typeof(BarcodeControl.Code128C).AssemblyQualifiedName,
                Header = "Code128C",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code128C", "128C", "code" }
            });
#endif
        
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView =typeof(BarcodeControl.Code32).AssemblyQualifiedName,
                Header = "Code32",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code32", "32", "code" }
            });
#if (!STORE_SB)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Code39).AssemblyQualifiedName,
                Header = "Code39",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code39", "39", "code" }
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Code39Ext).AssemblyQualifiedName,
                Header = "Code39Ext",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code39Ext", "39", "Ext" }

            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Code93).AssemblyQualifiedName,
                Header = "Code93",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code93", "93", "code" }
            });
            #if (!STORE_SB)
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.Code93Ext).AssemblyQualifiedName,
                Header = "Code93Ext",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "Code93Ext", "93", "Ext" }
            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                SampleView = typeof(BarcodeControl.QRBarcode).AssemblyQualifiedName,
                Header = "QRBarcode",
                Category = Categories.DataVisualization,
                Product = "Barcode",
                Tag = Tags.None,
                SearchKeys = new string[] { "QRBarcode", "QR", "Barcode" }
            });

        SampleHelper.SetTagsForProduct("Barcode", Tags.None);
        }
    }
}
