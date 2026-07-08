using System;
using System.IO;
using Syncfusion.PMML;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.ApplicationModel;
using Syncfusion.UI.Xaml.Grid;
using Windows.UI.Xaml.Documents;

namespace SampleBrowser.UWP.PredictiveAnalytics
{
    public class ViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Private properties
        private bool m_IsBusy;
        private string m_PmmlPath;
        private Paragraph m_PMML;
        private Paragraph m_RCode;
        private Paragraph m_CSharpCode;
        private string m_PurchasedItems;
        private string m_InputRecords;
        private ObservableCollection<Glass> m_GlassCollection;
        private ObservableCollection<Bfeed> m_BfeedCollection;
        private ObservableCollection<Audit> m_AuditCollection;
        private ObservableCollection<Wine> m_WineCollection;
        private ObservableCollection<BreastCancer> m_BreastCancerCollection;
        private ObservableCollection<Iris> m_IrisCollection;
        private ObservableCollection<Titanic> m_TitanicCollection;
        private ObservableCollection<RecommendedGroceries> m_RecommendedGroceries;
        #endregion

        
        #region Public properties
        public string PMMLPath
        {
            get
            {
                return m_PmmlPath;
            }
            set
            {
                m_PmmlPath = value;
                RaisePropertyChanged("PMMLPath");
            }
        }
        public bool IsBusy
        {
            get
            {
                return m_IsBusy;
            }
            set
            {
                m_IsBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
        public Paragraph CSharpCode
        {
            get
            {
                return m_CSharpCode;
            }
            set
            {
                m_CSharpCode = value;
                RaisePropertyChanged("CSharpCode");
            }
        }
        public Paragraph RCode
        {
            get
            {
                return m_RCode;
            }
            set
            {
                m_RCode = value;
                RaisePropertyChanged("RCode");
            }
        }
        public Paragraph PMML
        {
            get
            {
                return m_PMML;
            }
            set
            {
                m_PMML = value;
                RaisePropertyChanged("PMML");
            }
        }
        public string PurchasedItems
        {
            get
            {
                return m_PurchasedItems;
            }
            set
            {
                m_PurchasedItems = value;
                RaisePropertyChanged("PurchasedItems");
            }
        }
        public string InputRecords
        {
            get
            {
                return m_InputRecords;
            }
            set
            {
                m_InputRecords = value;
                RaisePropertyChanged("InputRecords");
            }
        }
        public ObservableCollection<RecommendedGroceries> RecommendedGroceries
        {
            get
            {
                return m_RecommendedGroceries;
            }
            set
            {
                m_RecommendedGroceries = value;
                RaisePropertyChanged("RecommendedGroceries");
            }
        }
        public ObservableCollection<Glass> GlassCollection
        {
            get
            {
                return m_GlassCollection;
            }
            set
            {
                m_GlassCollection = value;
                RaisePropertyChanged("GlassCollection");
            }
        }
        public ObservableCollection<Bfeed> BfeedCollection
        {
            get
            {
                return m_BfeedCollection;
            }
            set
            {
                m_BfeedCollection = value;
                RaisePropertyChanged("BfeedCollection");
            }
        }
        public ObservableCollection<Audit> AuditCollection
        {
            get
            {
                return m_AuditCollection;
            }
            set
            {
                m_AuditCollection = value;
                RaisePropertyChanged("AuditCollection");
            }
        }
        public ObservableCollection<Wine> WineCollection
        {
            get
            {
                return m_WineCollection;
            }
            set
            {
                m_WineCollection = value;
                RaisePropertyChanged("WineCollection");
            }
        }
        public ObservableCollection<BreastCancer> BreastCancerCollection
        {
            get
            {
                return m_BreastCancerCollection;
            }
            set
            {
                m_BreastCancerCollection = value;
                RaisePropertyChanged("BreastCancerCollection");
            }
        }
        public ObservableCollection<Iris> IrisCollection
        {
            get
            {
                return m_IrisCollection;
            }
            set
            {
                m_IrisCollection = value;
                RaisePropertyChanged("IrisCollection");
            }
        }
        public ObservableCollection<Titanic> TitanicCollection
        {
            get
            {
                return m_TitanicCollection;
            }
            set
            {
                m_TitanicCollection = value;
                RaisePropertyChanged("TitanicCollection");
            }
        }
        public IList<ClusterDetails> ClusterInformation
        {
            get;
            set;
        }

        public Style PredictedColumnStyle { get; set; }
        #endregion        

        /// <summary>
        /// Gets the PMML path from AppX
        /// </summary>
        /// <param name="pmmlPath">complete PMML path</param>
        /// <returns>PMML path</returns>
        public string GetPMMLPath(string pmmlPath)
        {
            return pmmlPath.Replace("ms-appx:///", "");
        }

        /// <summary>
        /// Merge the input and output cell(s) 
        /// </summary>
        /// <param name="inputDataTable">input table</param>
        /// <param name="outputTable">output table</param>
        /// <returns></returns>
        public ObservableCollection<BusinessObject> MergeTable(Table inputTable, Table outputTable, Table inputDataTable)
        {
            ObservableCollection<BusinessObject> rows = new ObservableCollection<BusinessObject>();

            for (int i = 0; i < outputTable.RowCount; i++)
            {
                Dictionary<string, object> row = new Dictionary<string, object>();

                for (int j = 0; j < inputDataTable.ColumnCount; j++)
                {
                    row[inputDataTable.ColumnNames[j]] = inputTable[i, j];
                }

                for (int j = 0; j < outputTable.ColumnCount; j++)
                {
                    row[outputTable.ColumnNames[j]] = outputTable[i, j];
                }

                BusinessObject baseObject = new BusinessObject();
                baseObject.DictValueProperty = row;

                rows.Add(baseObject);
            }

            return rows;

        }

        /// <summary>
        /// Takes corresponding page input values
        /// </summary>
        /// <param name="inputTable">input table</param>
        /// <param name="skip">start index/ items to skip</param>
        /// <param name="take">number of items to be taken</param>
        /// <returns></returns>
        public Table Take(Table inputTable, int skip, int take)
        {
            int rowCount = skip;

            Table currentPageTable = new Table(take - skip, inputTable.ColumnCount);

            for (int i = 0; i < inputTable.ColumnCount; i++)
            {
                currentPageTable.ColumnNames[i] = inputTable.ColumnNames[i];
            }

            int row = 0;
            for (int i = skip; i < take; i++)
            {
                currentPageTable[row] = inputTable[i];
                row++;
            }

            return currentPageTable;
        }

        /// <summary>
        /// Add column values to Grid
        /// </summary>
        /// <param name="enumDataList">enumerable collection</param>
        public SfDataGrid AddColumnToGrid(IEnumerable<BusinessObject> enumDataList, SfDataGrid sfDataGrid, Table inputTable)
        {
            int index = 0;
            sfDataGrid.Columns.Clear();
            var firstRow = (BusinessObject)enumDataList.FirstOrDefault();
            if (firstRow != null)
                foreach (KeyValuePair<string, object> item in firstRow.DictValueProperty)
                {
                    var cellValue = item.Value.ToString();

                    double result = 0.0;

                    // Aligns columns with numeric values on "right" and text variables on "left"
                    if (double.TryParse(cellValue, out result))
                    {
                        var column = new Syncfusion.UI.Xaml.Grid.GridNumericColumn();
                        column.HeaderText = item.Key;
                        column.TextAlignment = TextAlignment.Right;
                        column.MappingName = "DictValueProperty[" + item.Key.ToString() + "]";

                        if (index >= inputTable.ColumnNames.Length)
                        {
                            // Applies style "predictedColumnColor" for predicted result columns
                            if (PredictedColumnStyle != null)
                                column.CellStyle = PredictedColumnStyle;                            
                        }
                        sfDataGrid.Columns.Add(column);
                    }
                    else
                    {
                        var textColumn = new Syncfusion.UI.Xaml.Grid.GridTextColumn();
                        textColumn.HeaderText = item.Key;
                        textColumn.MappingName = "DictValueProperty[" + item.Key.ToString() + "]";
                        if (index >= inputTable.ColumnNames.Length)
                        {
                            // Applies style "predictedColumnColor" for predicted result columns
                            if(PredictedColumnStyle != null)
                                textColumn.CellStyle = PredictedColumnStyle;
                        }
                        sfDataGrid.Columns.Add(textColumn);
                    }
                    index++;
                }
            return sfDataGrid;
        }

        #region PropertyChanges event
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        #endregion

        //Dispose method
        public void Dispose()
        {
            if (GlassCollection != null)
                GlassCollection.Clear();
            if (BfeedCollection != null)
                BfeedCollection.Clear();
            if (AuditCollection != null)
                AuditCollection.Clear();
            if (WineCollection != null)
                WineCollection.Clear();
            if (BreastCancerCollection != null)
                BreastCancerCollection.Clear();
            if (IrisCollection != null)
                IrisCollection.Clear();
            if (TitanicCollection != null)
                TitanicCollection.Clear();
            if (RecommendedGroceries != null)
                RecommendedGroceries.Clear();
            if (ClusterInformation != null)
                ClusterInformation.Clear();
            if (PMML != null)
                PMML = null;
            if (RCode != null)
                RCode = null;
            if (CSharpCode != null)
                CSharpCode = null;
        }
    }
}