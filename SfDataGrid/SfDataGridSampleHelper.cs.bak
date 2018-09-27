using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.SampleBrowser.UWP.SfDataGrid
{
    class SamplesConfiguration
    {
        public SamplesConfiguration()
        {
#if !SFDATAGRID_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Expense Analysis",
                SampleCategory = "Expense Analysis",
                Description = "This sample demonstrates analyzing an individual's expenses using the grid and chart controls.",
                Category = Categories.Grids,
                DesktopImage = "ms-appx:///WhatsNewImages/Expense-Analysis.jpg",
                MobileImage = "ms-appx:///WhatsNewImages/Expense-Analysis.jpg",
                SampleView = typeof(ExpenseAnalysisDemo.HomePage).AssemblyQualifiedName,
                Tag = Tags.None,
                SampleType = SampleType.Showcase,
                Product = "DataGrid",

            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Getting Started",
                SampleCategory = "Getting Started",
                Category = Categories.Grids,
                ProductIcons = "Icons/Grid.png",
                SampleView = typeof(DataGrid.GettingStarted).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

#if !SFDATAGRID_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Data binding",
                SampleCategory = "Data Binding",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.DataBinding).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Complex and Indexer Properties",
                SampleCategory = "Data Binding",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.ComplexPropertyBinding).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });



            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Sorting",
                SampleCategory = "Data Presentation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Sorting).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Grouping",
                SampleCategory = "Data Presentation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Grouping).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Summaries",
                SampleCategory = "Data Presentation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Summaries).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Interval Grouping",
                SampleCategory = "Data Presentation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.IntervalGroupingDemo).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Custom Grouping",
                SampleCategory = "Data Presentation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.CustomGrouping).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Sort By Summary",
                SampleCategory = "Data Presentation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.SortBySummary).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

         
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Filtering",
                SampleCategory = "Data Filtering",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Filtering).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Advanced Filtering",
                SampleCategory = "Data Filtering",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.ExcelLikeFiltering).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
#if !SFDATAGRID_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Filter Row",
                SampleCategory = "Data Filtering",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.FilterRow).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Master-Details View",
                    SampleCategory = "Master Detail",
                    Category = Categories.Grids,
                    SampleView = typeof(DataGrid.MasterDetailsView).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Column Types",
                    SampleCategory = "Master Detail",
                    Category = Categories.Grids,
                    SampleView = typeof(DataGrid.DetailsViewColumnTypes).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Stacked Headers",
                    SampleCategory = "Master Detail",
                    Category = Categories.Grids,
                    SampleView = typeof(DataGrid.DetailsViewStackedHeaderRows).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Conditional Formating",
                    SampleCategory = "Master Detail",
                    Category = Categories.Grids,
                    SampleView = typeof(DataGrid.DetailsViewConditionalFormating).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });

                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Excel Exporting",
                    SampleCategory = "Master Detail",
                    Category = Categories.Grids,
                    SampleView = typeof(DataGrid.MasterDetailsExporting).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });
            }
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Data Virtualization",
                SampleCategory = "Data Virtualization",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.DataVirtualization).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Data Paging",
                SampleCategory = "Data Virtualization",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Paging).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "On-demand Paging",
                SampleCategory = "Data Virtualization",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.OnDemandPaging).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Incremental Loading",
                SampleCategory = "Data Virtualization",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.IncrementalLoading).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Editing",
                SampleCategory = "Editing",
                Category = Categories.Grids,
                Tag = Tags.None,
                SampleView = typeof(DataGrid.CellTypes).AssemblyQualifiedName,
                Product = "DataGrid",

            });
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Dialog editing",
                    SampleCategory = "Editing",
                    Category = Categories.Grids,
                    Tag = Tags.None,
                    SampleView = typeof(DataGrid.Editing).AssemblyQualifiedName,
                    Product = "DataGrid",

                });
            }
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Editable Columns",
                SampleCategory = "Editing",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.EditableColumnTypes).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Drop down and Read Only Columns",
                SampleCategory = "Editing",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.DropDownAndReadOnlyColumns).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "ComboBox Column",
                SampleCategory = "Editing",
                DesktopImage = "ms-appx:///WhatsNewImages/ComboBoxColumn.png",
                MobileImage = "ms-appx:///WhatsNewImages/ComboBoxColumn.png",
                Description = "This sample showcases to load different ItemsSource for different rows in GridComboBoxColumn.",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.ComboBoxColumn).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });



            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "AddNewRow",
                SampleCategory = "Rows",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.AddNewRow).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Stacked Headers",
                SampleCategory = "Rows",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.StackedHeaderRows).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Auto Row Height",
                SampleCategory = "Rows",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.AutoRowHeight).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Unbound Row",
                SampleCategory = "Rows",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.UnBoundRow).AssemblyQualifiedName,
                Tag = Tags.None,
                SampleType = SampleType.Featured,
                Product = "DataGrid",
            });


            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Unbound column",
                SampleCategory = "Columns",
                Description = "This sample showcases the unbound column capability of SfDataGrid. SfDataGrid supports the addition of extra columns to the data source columns. These additional columns are called as unbound columns, as they do not belong to the data source. Unbound column has two features like Expression and Format and these unbound fields are used when you want to add additional or custom information to each record. An unbound column is sorted, grouped, and filtered like other GridColumns.",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.UnBoundColumns).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Column Sizer",
                SampleCategory = "Columns",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.ColumnSizerDemo).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Cell Merge",
                SampleCategory = "Columns",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.CellMerge).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Auto Cell Merge",
                    SampleCategory = "Columns",
                    Category = Categories.Grids,
                    SampleView = typeof(DataGrid.AutoCellMerge).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });
            }
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Data Validation",
                SampleCategory = "Data Validation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.DataValidations).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Custom Validation",
                SampleCategory = "Data Validation",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.CustomValidations).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });


            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Row Selection",
                SampleCategory = "Selection",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Selections).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Cell Selection",
                SampleCategory = "Selection",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.CellSelectionDemo).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Real Time Updates",
                SampleCategory = "Performance",
                Description = "This sample showcases the real time update capabilities of the DataGrid control. ",
                Category = Categories.Grids,
                DesktopImage = "ms-appx:///WhatsNewImages/RealTimeUpdate.png",
                MobileImage = "ms-appx:///WhatsNewImages/RealTimeUpdate.png",
                SampleView = typeof(DataGrid.Performance).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
#if !SFDATAGRID_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Trading Grid",
                SampleCategory = "Performance",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.TradingGrid).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Alternative Row Style",
                SampleCategory = "Appearance",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.AlternativeRowStyle).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Styling",
                SampleCategory = "Appearance",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Styling).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Visual Styles",
                    SampleCategory = "Appearance",
                    Description = "This sample showcases the different visual styles available in the DataGrid control. ",
                    Category = Categories.Grids,
                    DesktopImage = "ms-appx:///WhatsNewImages/VisualStyles.png",
                    MobileImage = "ms-appx:///WhatsNewImages/VisualStyles.png",
                    SampleView = typeof(DataGrid.VisualStyles).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });
            }
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Freeze Panes",
                SampleCategory = "Appearance",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.FreezePanes).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
        
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Conditional Row Style",
                SampleCategory = "Appearance",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.RowStyle).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
#endif
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Conditional Cell Style",
                SampleCategory = "Appearance",
                Description = "This sample showcases the conditional formatting functionality in the DataGrid control. ",
                Category = Categories.Grids,
                DesktopImage = "ms-appx:///WhatsNewImages/CellStyle.png",
                MobileImage = "ms-appx:///WhatsNewImages/CellStyle.png",
                SampleView = typeof(DataGrid.ConditionalFormatting).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
#if !SFDATAGRID_STORE
            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Context Menu",
                SampleCategory = "Interactive Features",
                Description = "This sample showcases the capabilities of the context menu available in the DataGrid control. ",
                Category = Categories.Grids,
                DesktopImage = "ms-appx:///WhatsNewImages/ContextMenu.png",
                MobileImage = "ms-appx:///WhatsNewImages/ContextMenu.png",
                SampleView = typeof(DataGrid.ContextMenuDemo).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });
            if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
            {
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Column Chooser",
                    SampleCategory = "Interactive Features",
                    Description = "This sample showcases the capabilities of the ColumnChooser window available in the DataGrid control. ",
                    Category = Categories.Grids,
                    DesktopImage = "ms-appx:///WhatsNewImages/ColumnChosser.png",
                    MobileImage = "ms-appx:///WhatsNewImages/ColumnChosser.png",
                    SampleView = typeof(DataGrid.ColumnChooserDemo).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Clipboard Operation",
                    SampleCategory = "Interactive Features",
                    Category = Categories.Grids,
                    SampleView = typeof(DataGrid.ClipboardOperation).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });
                SampleHelper.SampleViews.Add(new SampleInfo()
                {
                    Header = "Drag and Drop",
                    SampleCategory = "Interactive Features",
                    Description = "This sample showcases the built-in row drag and drop capability in SfDataGrid. ",
                    Category = Categories.Grids,
                    DesktopImage = "ms-appx:///WhatsNewImages/RowDragandDrop.png",
                    MobileImage = "ms-appx:///WhatsNewImages/RowDragandDrop.png",
                    SampleView = typeof(DataGrid.DragandDrop).AssemblyQualifiedName,
                    Tag = Tags.None,
                    Product = "DataGrid",
                });

            }

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Serialization",
                SampleCategory = "Serialization",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Serialization).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Excel Exporting",
                SampleCategory = "Exporting",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Exporting).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Pdf Exporting",
                SampleCategory = "Exporting",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.PdfExporting).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Printing",
                SampleCategory = "Printing",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.Printing).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "Custom Printing",
                SampleCategory = "Printing",
                Category = Categories.Grids,
                SampleView = typeof(DataGrid.CustomPrinting).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "DataGrid",
            });

            //SampleHelper.SampleViews.Add(new SampleInfo()
            //{
            //    Header = "Localization",
            //    SampleCategory = "Localization",
            //    Description = "This sample showcases the complex and indexer properties binding capabilities in SfDataGrid. SfDataGrid supports to bind complex and indexer properties to its columns at any level.",
            //    Category = "DataGrid",
            //    SampleView = typeof(DataGrid.Localization).AssemblyQualifiedName,
            //    Tag = Tags.None,
            //});

            SampleHelper.SampleViews.Add(new SampleInfo()
            {
                Header = "MultiColumnDropDown",
                SampleCategory = "SfMultiColumnDropDownControl",
                Category = Categories.Editors,
                SampleView = typeof(DataGrid.MultiColumnDropDownControl).AssemblyQualifiedName,
                Tag = Tags.None,
                Product = "MultiColumnDropDown",
            });
#endif
            SampleHelper.SetTagsForProduct("DataGrid", Tags.None);
        }
    }
}
