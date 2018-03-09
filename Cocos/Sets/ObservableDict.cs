using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Cocos.Sets
{
    [Serializable]
    public class ObservableDict<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public IDictionary<TKey, TValue> BaseDict => baseDict;

        public ICollection<TKey> Keys => baseDict.Keys;
        public ICollection<TValue> Values => baseDict.Values;
        public int Count => baseDict.Count;
        public bool IsReadOnly => baseDict.IsReadOnly;

        public TValue this[TKey key] { get => baseDict[key]; set => baseDict[key] = value; }

        private IDictionary<TKey, TValue> baseDict;

        public ObservableDict() => baseDict = new Dictionary<TKey, TValue>();
        public ObservableDict(int capacity) => baseDict = new Dictionary<TKey, TValue>(capacity);
        public ObservableDict(int capacity, IEqualityComparer<TKey> comparer) => baseDict = new Dictionary<TKey, TValue>(capacity, comparer);
        public ObservableDict(IDictionary<TKey, TValue> dictionary) => baseDict = new Dictionary<TKey, TValue>(dictionary);
        public ObservableDict(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) => baseDict = new Dictionary<TKey, TValue>(dictionary, comparer);
        public ObservableDict(IEnumerable<KeyValuePair<TKey, TValue>> collection) => baseDict = new Dictionary<TKey, TValue>(collection);
        public ObservableDict(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) => baseDict = new Dictionary<TKey, TValue>(collection, comparer);
        public ObservableDict(IEqualityComparer<TKey> comparer) => baseDict = new Dictionary<TKey, TValue>(comparer);
        protected ObservableDict(SerializationInfo info, StreamingContext context) => baseDict = (Dictionary<TKey, TValue>)info.GetValue("baseDict", typeof(Dictionary<TKey, TValue>));

        public void Clear()
        {
            baseDict.Clear();
            DictCleared();
        }
        public bool ContainsKey(TKey key) => baseDict.ContainsKey(key);
        public bool Contains(KeyValuePair<TKey, TValue> item) => baseDict.Contains(item);
        public bool TryGetValue(TKey key, out TValue value) => baseDict.TryGetValue(key, out value);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => baseDict.CopyTo(array, arrayIndex);
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue("baseDict", baseDict, typeof(Dictionary<TKey, TValue>));

        public void Add(TKey key, TValue value)
        {
            baseDict.Add(key, value);
            ItemAdded(key);
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            baseDict.Add(item);
            ItemAdded(item.Key);
        }
        public bool Remove(TKey key)
        {
            bool removed = baseDict.Remove(key);
            ItemRemoved(key);
            return removed;
        }
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool remove = baseDict.Remove(item);
            ItemRemoved(item.Key);
            return remove;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => baseDict.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => baseDict.GetEnumerator();

        protected virtual void ItemAdded(TKey key) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, key));
        protected virtual void ItemRemoved(TKey key) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, key));
        protected virtual void DictCleared() => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(this, e);
    }
}