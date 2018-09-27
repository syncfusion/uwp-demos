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
using Syncfusion.Calculate.Helpers;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CalculateSamples
{
    public sealed partial class ComputeFormulaDemo : SampleLayout
    {
        private Syncfusion.Calculate.Helpers.CalcEngine engine;
        CalcData data;
        public ComputeFormulaDemo()
        {
            this.InitializeComponent();
            data = new CalcData();
            engine = new Syncfusion.Calculate.Helpers.CalcEngine(data);
            this.formula.GotFocus += formula_GotFocus;

        }
        void formula_GotFocus(object sender, RoutedEventArgs e)
        {
            this.blockResultValue.Text = "";
        }

        private void generateBtn_Click(object sender, RoutedEventArgs e)
        {
            //text box and is not entered into the grid.
            this.engine.UseDependencies = false;
            this.blockResultValue.Text = this.engine.ParseAndComputeFormula(this.formula.Text);
            this.engine.UseDependencies = true;
        }
    }
}
