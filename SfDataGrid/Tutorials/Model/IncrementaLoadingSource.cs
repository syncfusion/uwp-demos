using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Data.Services.Client;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Xaml.Data;

namespace DataGrid
{
    /// <summary>
    /// This class represents the IncrementaLoadingSource
    /// </summary>
    public class IncrementaLoadingSource<T> : IList<T>, ISupportIncrementalLoading, INotifyCollectionChanged
    {
        #region Members

        List<T> InternalList;
        Func<CancellationToken, uint, int, Task<IList<T>>> LoadMoreItems;

        #endregion

        #region Public Members

        public int MaxItemCount { get; set; }

        #endregion

        #region Ctor

        public IncrementaLoadingSource(Func<CancellationToken, uint, int, Task<IList<T>>> loadeMoreItemsFunc)
        {
            InternalList = new List<T>();
            LoadMoreItems = loadeMoreItemsFunc;
        }

        #endregion

        #region ISupportIncreamentaLoading Members

        public bool HasMoreItems
        {
            get { return InternalList.Count < MaxItemCount; }
        }
        IAsyncOperation<LoadMoreItemsResult> result;
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            try
            {
                if (isBusy)
                    return result;
                isBusy = true;
                result=AsyncInfo.Run((c) => LoadMoreItemsAsync(c, count));
                return result;
            }
            finally
            {
                
            }

        }

        #endregion

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return InternalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            InternalList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            InternalList.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return InternalList[index];
            }
            set
            {
                InternalList[index] = value;
            }
        }

        public void Add(T item)
        {
            InternalList.Add(item);
        }

        public void Clear()
        {
            InternalList.Clear();
        }

        public bool Contains(T item)
        {
            return InternalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            InternalList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return InternalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return InternalList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InternalList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (InternalList as IEnumerable).GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChaged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        void RaiseCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, args);
        }

        #endregion

        #region Private Methods
        bool isBusy = false;
        async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                Debug.WriteLine(count);
                var baseIndex = this.InternalList.Count;
                var items = await LoadMoreItems(c, count, baseIndex);
                InternalList.AddRange(items);
                NotifyOfInsertedItems(baseIndex, items.Count);
                return new LoadMoreItemsResult() { Count = (uint)items.Count };
            }
            finally
            {
                isBusy = false;
            }
        }

        void NotifyOfInsertedItems(int baseIndex, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, this.InternalList[i + baseIndex], i + baseIndex);
                RaiseCollectionChanged(args);
            }
        }

        #endregion
    }
}

