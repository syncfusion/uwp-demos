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


        public event ValueChangedEventHandler ValueChanged;

        public object GetValueRowCol(int row, int col)
        {
            if (row == 2 && col == 1)
                throw new Exception("Test Exception");
            else
            {
                return m_data[row];
            }
        }

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

        public void WireParentObject()
        {
            //throw new System.NotImplementedException();
        }
    }
}