using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common;
using Syncfusion.UI.Xaml.Spreadsheet.GraphicCells;
using Syncfusion.UI.Xaml.SpreadsheetHelper;
using Syncfusion.UI.Xaml.Spreadsheet.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpreadsheetSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SpreadsheetShowcase : Page, IDisposable
    {
        public SpreadsheetShowcase()
        {
            this.InitializeComponent();
            OpenWorkbook();
            this.spreadsheet.AddGraphicChartCellRenderer(new GraphicChartCellRenderer());
            this.Unloaded += SpreadsheetShowcase_Unloaded;
            
        }

        private void SpreadsheetShowcase_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        async void OpenWorkbook()
        {
            var assembly = typeof(SpreadsheetSamples.SpreadsheetShowcase).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.SfSpreadsheet.Assets.Showcase.xlsx";
            using (var fileStream = assembly.GetManifestResourceStream(resourcePath))
            {
                await this.spreadsheet.Open(fileStream);
            }
        }

        #region IDisposable

        public void Dispose()
        {
            this.Unloaded -= SpreadsheetShowcase_Unloaded;
            this.ribbon.Dispose();
            this.spreadsheet.Dispose();
            Resources.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion
    }
}
