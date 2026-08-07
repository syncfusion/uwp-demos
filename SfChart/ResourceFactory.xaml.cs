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

            labelTemplate8 = ResourceGrid.Resources["labelTemplate01"] as DataTemplate;

            labelTemplate9 = ResourceGrid.Resources["DataLabelTemplate"] as DataTemplate;

            labelTemplate10 = ResourceGrid.Resources["AdornmentLabelTemplate"] as DataTemplate;

            labelTemplate11 = ResourceGrid.Resources["adornment"] as DataTemplate;

            labelTemplate12 = ResourceGrid.Resources["stAdornment1"] as DataTemplate;

            labelTemplate13 = ResourceGrid.Resources["stAdornment2"] as DataTemplate;

            labelTemplate14 = ResourceGrid.Resources["stAdornment3"] as DataTemplate;

            labelTemplate15 = ResourceGrid.Resources["adornment"] as DataTemplate;

            labelTemplate16 = ResourceGrid.Resources["adornShape"] as DataTemplate;

            labelTemplate17 = ResourceGrid.Resources["dtTemp"] as DataTemplate;

            labelTemplate117 = ResourceGrid.Resources["vstemp"] as DataTemplate;

            labelTemplate18 = ResourceGrid.Resources["vsTemp"] as DataTemplate;

            labelTemplate19 = ResourceGrid.Resources["wmsTemp"] as DataTemplate;

            labelTemplate20 = ResourceGrid.Resources["wmTemp"] as DataTemplate;

            labelTemplate210 = ResourceGrid.Resources["scatterAdornmentTemplate"] as DataTemplate;

            labelTemplate21 = ResourceGrid.Resources["populationLabelTemplate1"] as DataTemplate;

            labelTemplate22 = ResourceGrid.Resources["populationLabelTemplate2"] as DataTemplate;

            labelTemplate23 = ResourceGrid.Resources["populationLabelTemplate3"] as DataTemplate;

            labelTemplate24 = ResourceGrid.Resources["waterfallAdornment"] as DataTemplate;

            labelTemplate25 = ResourceGrid.Resources["adornment1"] as DataTemplate;

            splineRangeAreaAdornment = ResourceGrid.Resources["splineRangeAreaAdornment"] as DataTemplate;
        }

        public DataTemplate labelTemplate1 { get; set; }

        public DataTemplate labelTemplate2 { get; set; }

        public DataTemplate labelTemplate210 { get; set; }

        public DataTemplate labelTemplate3 { get; set; }

        public DataTemplate labelTemplate4 { get; set; }

        public DataTemplate labelTemplate5 { get; set; }

        public DataTemplate labelTemplate6 { get; set; }

        public DataTemplate labelTemplate7 { get; set; }

        public DataTemplate labelTemplate117 { get; set; }

        public DataTemplate labelTemplate8 { get; set; }

        public DataTemplate labelTemplate9 { get; set; }

        public DataTemplate labelTemplate10 { get; set; }

        public DataTemplate labelTemplate11 { get; set; }

        public DataTemplate labelTemplate12 { get; set; }

        public DataTemplate labelTemplate13 { get; set; }

        public DataTemplate labelTemplate14 { get; set; }

        public DataTemplate labelTemplate15 { get; set; }

        public DataTemplate labelTemplate16 { get; set; }

        public DataTemplate labelTemplate17 { get; set; }

        public DataTemplate labelTemplate18 { get; set; }

        public DataTemplate labelTemplate19 { get; set; }

        public DataTemplate labelTemplate20 { get; set; }

        public DataTemplate labelTemplate21 { get; set; }

        public DataTemplate labelTemplate22 { get; set; }

        public DataTemplate labelTemplate23 { get; set; }

        public DataTemplate labelTemplate24 { get; set; }

        public DataTemplate labelTemplate25 { get; set; }

        public DataTemplate splineRangeAreaAdornment { get; set; }
    }
}
