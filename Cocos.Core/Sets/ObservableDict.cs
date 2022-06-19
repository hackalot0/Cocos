using Cocos.Core.Events;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Cocos.Core.Sets
{
    public class ObservableDict<TKey, TValue> : IDictionary<TKey, TValue>, IObservableDict<TKey, TValue> where TKey : notnull
    {
        public event Sender.Event? Cleared;
        public event Item<KeyValuePair<TKey, TValue>>.Event? ItemAdded;
        public event Item<KeyValuePair<TKey, TValue>>.Event? ItemRemoved;
        public event Item<KeyValuePair<TKey, TValue>>.Replace.Event? ItemReplaced;

        public Observable.Options Options { get; } = new();

        public ICollection<TKey> Keys => _dict.Keys;
        public ICollection<TValue> Values => _dict.Values;
        public IEqualityComparer<TKey> Comparer => _dict.Comparer;

        public TValue this[TKey key] { get => _dict[key]; set => InternalSetItem(key, value); }

        public int Count => _dict.Count;
        public bool IsReadOnly => _idict.IsReadOnly;

        private Dictionary<TKey, TValue> _dict;
        private IDictionary<TKey, TValue> _idict;

        public ObservableDict() { _idict = _dict = new(); }
        public ObservableDict(IDictionary<TKey, TValue> dictionary) { _idict = _dict = new(dictionary); }
        public ObservableDict(IEnumerable<KeyValuePair<TKey, TValue>> collection) { _idict = _dict = new(collection); }
        public ObservableDict(IEqualityComparer<TKey>? comparer) { _idict = _dict = new(comparer); }
        public ObservableDict(int capacity) { _idict = _dict = new(capacity); }
        public ObservableDict(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) { _idict = _dict = new(dictionary, comparer); }
        public ObservableDict(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer) { _idict = _dict = new(collection, comparer); }
        public ObservableDict(int capacity, IEqualityComparer<TKey>? comparer) { _idict = _dict = new(capacity, comparer); }

        public void Clear()
        {
            if (Options.UseRemoveForClear) _dict.Keys.ToList().ForEach(a => Remove(a));
            else
            {
                _dict.Clear();
                OnCleared();
            }
        }

        protected virtual void InternalAddItem(TKey key, TValue value)
        {
            _dict.Add(key, value);
            OnItemAdded(key, value);
        }
        protected virtual bool InternalRemoveItem(TKey key)
        {
            TValue value = _dict[key];
            bool result = _dict.Remove(key);
            if (result) OnItemRemoved(key, value);
            return result;
        }
        protected virtual void InternalSetItem(TKey key, TValue newValue)
        {
            TValue oldValue = _dict[key];
            if (Options.UseRemoveAddForReplace)
            {
                OnItemRemoved(key, oldValue);
                _dict[key] = newValue;
                OnItemAdded(key, newValue);
            }
            else
            {
                _dict[key] = newValue;
                OnItemReplaced(key, oldValue, newValue);
            }
        }

        public void Add(TKey key, TValue value) => InternalAddItem(key, value);
        public void Add(KeyValuePair<TKey, TValue> item) => InternalAddItem(item.Key, item.Value);

        public bool Remove(TKey key) => InternalRemoveItem(key);
        public bool Remove(KeyValuePair<TKey, TValue> item) => InternalRemoveItem(item.Key);

        public bool Contains(KeyValuePair<TKey, TValue> item) => _dict.ContainsKey(item.Key);
        public bool ContainsKey(TKey key) => _dict.ContainsKey(key);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => _dict.TryGetValue(key, out value);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _idict.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _idict.GetEnumerator();

        protected virtual void OnCleared() => Cleared?.Invoke(new() { Sender = this });
        protected virtual void OnItemAdded(TKey key, TValue item) => ItemAdded?.Invoke(new() { Sender = this, Item = new(key, item) });
        protected virtual void OnItemRemoved(TKey key, TValue item) => ItemRemoved?.Invoke(new() { Sender = this, Item = new(key, item) });
        protected virtual void OnItemReplaced(TKey key, TValue item, TValue newItem) => ItemReplaced?.Invoke(new() { Sender = this, Item = new(key, item), NewItem = new(key, newItem) });

        IEnumerator IEnumerable.GetEnumerator() => _dict.GetEnumerator();
    }
}