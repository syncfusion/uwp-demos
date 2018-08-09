using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
//using Common;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using Syncfusion.XlsIO;
using Windows.UI.Popups;
//using SampleBrowser;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EssentialXlsIO
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class Customers 
    {

        #region Members
        private string m_salesPerson;
        private int m_salesJanJune;
        private int m_salesJulyDec;
        private int m_change;
        #endregion

        #region Properties
        public string SalesPerson
        {
            get
            {
                return m_salesPerson;
            }
            set
            {
                m_salesPerson = value;
            }
        }

        public int SalesJanJune
        {
            get
            {
                return m_salesJanJune;
            }
            set
            {
                m_salesJanJune = value;
            }
        }
        public int SalesJulyDec
        {
            get
            {
                return m_salesJulyDec;
            }
            set
            {
                m_salesJulyDec = value;
            }

        }
        public int Change
        {
            get
            {
                return m_change;
            }
            set
            {
                m_change = value;
            }

        }
        #endregion

        #region Intialization
        public Customers()
        {
        }
        public Customers(string name, int juneToJuly, int julyToDec, int change)
        {
            this.m_salesPerson = name;
            this.m_salesJanJune = juneToJuly;
            this.m_salesJulyDec = julyToDec;
            this.m_change = change;
        }
        #endregion

    }
}
