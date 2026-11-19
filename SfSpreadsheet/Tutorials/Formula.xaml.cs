using System;
using Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Reflection;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpreadsheetSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Formula : SampleLayout, IDisposable
    {
        public Formula()
        {
            this.InitializeComponent();
            OpenWorkbook();
        }

        async void OpenWorkbook()
        {
            var assembly = typeof(Formula).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.SfSpreadsheet.Assets.Formula.xlsx";
            try
            {
                using (var fileStream = assembly.GetManifestResourceStream(resourcePath))
                {
                    await this.spreadsheet.Open(fileStream);
                }
            }
            catch
            {

            }
        }

        public override void Dispose()
        {
            this.spreadsheet.Dispose();
            this.ribbon.Dispose();
            Resources.Clear();
            base.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
