using Common;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataBinding : SampleLayout
    {
        ListBinding listBinding;
        ObservableCollectionBinding observableCollectionBinding;
        public DataBinding()
        {
            this.InitializeComponent();
            listBinding = new ListBinding();
            observableCollectionBinding = new ObservableCollectionBinding();
            this.DataContext=  new DataBoundViewModel();
            this.comboBinding.SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        /// Occurs when selection is changed in SfDataGird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (dataBindArea != null)
                    {
                        if (!(dataBindArea.Content is ListBinding) && ((ComboBoxItem)e.AddedItems[0]).Content.Equals("List"))
                            dataBindArea.Content = listBinding;
                        else
                            dataBindArea.Content = observableCollectionBinding;
                    }
                });

        }

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        public sealed override void Dispose()
        {
            this.listBinding.Dispose();
            this.observableCollectionBinding.Dispose();
            this.listBinding = null;
            this.observableCollectionBinding = null;
            this.Resources.Clear();
            this.comboBinding.SelectionChanged -= OnSelectionChanged;
            if (dataBindArea.Content is ListBinding)
            {
                (dataBindArea.Content as ListBinding).Dispose();
                ((dataBindArea.Content as ListBinding).Content as Grid).Children.Clear();
            }
            else
            {
                (dataBindArea.Content as ObservableCollectionBinding).Dispose();
                ((dataBindArea.Content as ObservableCollectionBinding).Content as Grid).Children.Clear();               
            }
            dataBindArea = null;
            base.Dispose();
        }
    }
}
