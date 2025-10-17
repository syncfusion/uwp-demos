#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Calculate.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateSamples
{
     public class CalcData : ICalcData
    {
        private Dictionary<int, object> m_data = new Dictionary<int, object>();

        public Dictionary<int, object> Data
        {
            get { return m_data; }
        }

        public void UpdateValues(object val, int row)
        {
            m_data[row] = val;
        }

        /// <summary>
        /// This event should be raised by the implementer of ICalcData whenever a value changes.
        /// </summary>
        public event ValueChangedEventHandler ValueChanged;
        /// <summary>
        /// Returns the value at the row and column.
        /// </summary>
        /// <param name="row">One-based row index.</param>
        /// <param name="col">One based column index.</param>
        /// <returns>The cell value.</returns>
        public object GetValueRowCol(int row, int col)
        {
            if (row == 2 && col == 1)
                throw new Exception("Test Exception");
            else
            {
                return m_data[row];
            }
        }
        /// <summary>
        /// A method to save the value through the ICalcData.SetValueRowCol implementation method
        /// and raise the ValueChanged event.
        /// </summary>
        /// <param name="row">The row index, one-based.</param>
        /// <param name="col">The column index, one-based.</param>
        /// <param name="val">The value.</param>
        public void SetValueRowCol(object value, int row, int col)
        {
            if (value.ToString().Trim().StartsWith("="))
            {
                ValueChangedEventArgs e1 = new ValueChangedEventArgs(row, col, value.ToString());
                ValueChanged(this, e1);
            }
            else
            {
                m_data[row] = value;
            }

        }
        /// <summary>
        /// A Virtual method that can be used to handle subscribing to any base object events necessary for implementing the
        /// ValueChanged event.
        /// </summary>
        public void WireParentObject()
        {
            //throw new System.NotImplementedException();
        }
    }
}