using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace GK.Sets
{
    public class ObservableSet<T> : ICollection<T>, INotifyCollectionChanged
    {
        public int Count => baseSet.Count;
        public bool IsReadOnly => baseSet.IsReadOnly;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private ICollection<T> baseSet;

        public ObservableSet() => baseSet = new List<T>();
        public ObservableSet(IEnumerable<T> items) => baseSet = new List<T>(items);

        public void Add(T item)
        {
            baseSet.Add(item);
            ItemAdded(item);
        }
        public bool Remove(T item)
        {
            bool done = baseSet.Remove(item);
            if (done) ItemRemoved(item);
            return done;
        }
        public void Clear()
        {
            baseSet.Clear();
            Cleared();
        }
        public bool Contains(T item) => baseSet.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => baseSet.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => baseSet.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => baseSet.GetEnumerator();

        protected virtual void ItemAdded(T item) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        protected virtual void ItemRemoved(T item) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        protected virtual void Cleared() => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(this, e);
    }
}