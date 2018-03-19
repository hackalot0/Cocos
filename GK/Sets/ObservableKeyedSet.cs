using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GK.Sets
{
    [Serializable]
    public class ObservableKeyedSet<TKey, TValue> : ObservableDict<TKey, TValue>, ICollection<TValue>
    {
        public Func<TValue, TKey> KeyRetriever => keyRetriever;
        private Func<TValue, TKey> keyRetriever;

        public ObservableKeyedSet() { }
        public ObservableKeyedSet(int capacity) : base(capacity) { }
        public ObservableKeyedSet(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public ObservableKeyedSet(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { }
        public ObservableKeyedSet(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public ObservableKeyedSet(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        public ObservableKeyedSet(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        public ObservableKeyedSet(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever) { this.keyRetriever = keyRetriever; }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever, int capacity) : base(capacity) { this.keyRetriever = keyRetriever; }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever, IDictionary<TKey, TValue> dictionary) : base(dictionary) { this.keyRetriever = keyRetriever; }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever, IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { this.keyRetriever = keyRetriever; }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever, IEqualityComparer<TKey> comparer) : base(comparer) { this.keyRetriever = keyRetriever; }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever, int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { this.keyRetriever = keyRetriever; }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { this.keyRetriever = keyRetriever; }
        public ObservableKeyedSet(Func<TValue, TKey> keyRetriever, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { this.keyRetriever = keyRetriever; }
        protected ObservableKeyedSet(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected virtual TKey GetKeyForItem(TValue item) => keyRetriever.Invoke(item);

        public void Add(TValue item) => Add(GetKeyForItem(item), item);
        public bool Remove(TValue item) => Remove(GetKeyForItem(item));

        public bool Contains(TValue item) => ContainsKey(GetKeyForItem(item));

        public void CopyTo(TValue[] array, int arrayIndex) => Values.CopyTo(array, arrayIndex);
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => Values.GetEnumerator();
    }
}