#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Syncfusion.Calculate.Helpers;
using Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CalculateSamples
{
    public sealed partial class ArrayIcalcDataDemo : SampleLayout
    {
        private Random r = new Random();
        private ArrayCalcData data;
        int nRows;
        int nCols;
        public ArrayIcalcDataDemo()
        {
            this.InitializeComponent();
            ButtonAutomationPeer peer = new ButtonAutomationPeer(generateBtn);

            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();


            ButtonAutomationPeer peer1 = new ButtonAutomationPeer(setValueButton);


            this.rowTxtBox.Text = "0";
            this.colTxtBox.Text = "0";
            this.valueTxtBox.Text = data[0, 0].ToString();
            IInvokeProvider invokeProv1 = peer1.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv1.Invoke();
        }

        private void setValueButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.nRows == 0)
            {
                //MessageBox.Show("Generate data first.");
                return;
            }

            try
            {
                int row = int.Parse(this.rowTxtBox.Text);
                int col = int.Parse(this.colTxtBox.Text);
                string val = (double.Parse(this.valueTxtBox.Text)).ToString();

                this.data[row, col] = val;

                ShowObject();
            }
            catch (IndexOutOfRangeException ex1)
            {
                this.dataTxtBox.Text = ex1.Message;
            }
        }

        private void generateBtn_Click(object sender, RoutedEventArgs e)
        {

            //create some sample data
            this.nRows = r.Next(1, 5) + 2;
            this.nCols = r.Next(2, 4) + 1;
            double[,] a = new double[nRows, nCols];//{{1.1,2.1},{3.1,4.1}};
            for (int row = 0; row < nRows; ++row)
                for (int col = 0; col < nCols; ++col)
                    a[row, col] = ((double)r.Next(100)) / 10;

            //create a ArrayCalcData object and pass it this array
            this.data = new ArrayCalcData(a);

            //create an CalcEngine object using this ArrayCalcData object
            CalcEngine engine = new CalcEngine(this.data);

            //Turn on dependency tracking so values set with the Set button
            //take effect immediately
            engine.UseDependencies = true;

            //call RecalculateRange so any formulas in the data can be initially computed.
            engine.RecalculateRange(RangeInfo.Cells(1, 1, nRows + 1, nCols + 1), data);
            ShowObject();
        }

        /// <summary>
        /// Displays the ArrayCalcData values in a TextBox.
        /// </summary>
        private void ShowObject()
        {
            this.dataTxtBox.Text = "";
            for (int i = 0; i <= this.nRows; ++i)
            {
                for (int j = 0; j <= this.nCols; ++j)
                {
                    this.dataTxtBox.Text += this.data[i, j].ToString() + "\t";
                }
                this.dataTxtBox.Text += "\r\n";
            }
        }
    }
}
