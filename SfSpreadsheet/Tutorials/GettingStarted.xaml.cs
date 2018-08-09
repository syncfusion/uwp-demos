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
    public sealed partial class GettingStarted : SampleLayout, IDisposable
    {
        public GettingStarted()
        {
            this.InitializeComponent();
            OpenWorkbook();
        }

        async void OpenWorkbook()
        {
            var assembly = typeof(GettingStarted).GetTypeInfo().Assembly;
            string resourcePath = "Syncfusion.SampleBrowser.UWP.SfSpreadsheet.Assets.GettingStarted.xlsx";
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

        //void ZoomSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    var spreadsheet = this.spreadsheet;
        //    if (spreadsheet.ActiveSheet != null && e.NewValue != spreadsheet.ActiveSheet.Zoom)
        //        spreadsheet.SetZoomFactor(spreadsheet.ActiveSheet.Name, (int)e.NewValue);
        //}

        //private void ZoomDecreaseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var value = (int)((ZoomSlider.Value - 10) / 10);
        //    ZoomSlider.Value = value * 10;
        //}

        //private void ZoomIncreaseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var value = (int)((ZoomSlider.Value + 10) / 10);
        //    ZoomSlider.Value = value * 10;
        //}
    }
}
