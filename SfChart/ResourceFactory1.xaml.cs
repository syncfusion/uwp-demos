using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Syncfusion.SampleBrowser.UWP.SfChart
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResourceFactory : Page
    {
        public ResourceFactory()
        {
            this.InitializeComponent();

            labelTemplate1 = ResourceGrid.Resources["labelTemplate1"] as DataTemplate;

            labelTemplate2 = ResourceGrid.Resources["labelTemplate2"] as DataTemplate;

            labelTemplate3 = ResourceGrid.Resources["labelTemplate3"] as DataTemplate;

            labelTemplate4 = ResourceGrid.Resources["labelTemplate"] as DataTemplate;

            labelTemplate5 = ResourceGrid.Resources["labelTemplateStepArea"] as DataTemplate;

            labelTemplate6 = ResourceGrid.Resources["labelTemplateStepArea1"] as DataTemplate;

            labelTemplate7 = ResourceGrid.Resources["histogramLabelTemplate"] as DataTemplate;

            labelTemplate210 = ResourceGrid.Resources["scatterAdornmentTemplate"] as DataTemplate;

            labelTemplate24 = ResourceGrid.Resources["waterfallAdornment"] as DataTemplate;

	    splineRangeAreaAdornment = ResourceGrid.Resources["splineRangeAreaAdornment"] as DataTemplate;
        }

        public DataTemplate labelTemplate1 { get; set; }

        public DataTemplate labelTemplate2 { get; set; }

        public DataTemplate labelTemplate3 { get; set; }

        public DataTemplate labelTemplate4 { get; set; }

        public DataTemplate labelTemplate5 { get; set; }

        public DataTemplate labelTemplate6 { get; set; }

        public DataTemplate labelTemplate7 { get; set; }

        public DataTemplate labelTemplate210 { get; set; }

        public DataTemplate labelTemplate24 { get; set; }

	public DataTemplate splineRangeAreaAdornment { get; set; }
    }
}
