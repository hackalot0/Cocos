using Cocos.Core.Events;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cocos.Core.Sets
{
    public static class Observable
    {
        public class Options
        {
            public bool UseRemoveForClear { get; set; }
            public bool UseRemoveAddForReplace { get; set; }
        }
        public class Set<T> : Collection<T>, IObservable.Set<T>
        {
            public event Sender.Event Cleared;
            public event Item<T>.Event ItemAdded;
            public event Item<T>.Event ItemRemoved;
            public event Item<T>.Replace.Event ItemReplaced;

            public Options Options { get; } = new();

            public Set() { }
            public Set(IList<T> list) : base(list) { }

            protected virtual void OnCleared() => Cleared?.Invoke(new() { Sender = this });
            protected virtual void OnItemAdded(T item) => ItemAdded?.Invoke(new() { Sender = this, Item = item });
            protected virtual void OnItemRemoved(T item) => ItemRemoved?.Invoke(new() { Sender = this, Item = item });
            protected virtual void OnItemReplaced(T item, T newItem) => ItemReplaced?.Invoke(new() { Sender = this, Item = item, NewItem = newItem });

            protected override void ClearItems()
            {
                if (Options.UseRemoveForClear) while (Count > 0) RemoveItem(Count - 1);
                else
                {
                    base.ClearItems();
                    OnCleared();
                }
            }
            protected override void InsertItem(int index, T item)
            {
                base.InsertItem(index, item);
                OnItemAdded(item);
            }
            protected override void RemoveItem(int index)
            {
                T item = this[index];
                base.RemoveItem(index);
                OnItemRemoved(item);
            }
            protected override void SetItem(int index, T item)
            {
                T oldItem = this[index];
                if (Options.UseRemoveAddForReplace)
                {
                    OnItemRemoved(oldItem);
                    base.SetItem(index, item);
                    OnItemAdded(item);
                }
                else
                {
                    base.SetItem(index, item);
                    OnItemReplaced(oldItem, item);
                }
            }
        }
        public class Dict<TKey, TValue> : IDictionary<TKey, TValue>, IObservable.Dict<TKey, TValue>
        {
            public event Sender.Event Cleared;
            public event Item<KeyValuePair<TKey, TValue>>.Event ItemAdded;
            public event Item<KeyValuePair<TKey, TValue>>.Event ItemRemoved;
            public event Item<KeyValuePair<TKey, TValue>>.Replace.Event ItemReplaced;

            public Options Options { get; } = new();

            public ICollection<TKey> Keys => _dict.Keys;
            public ICollection<TValue> Values => _dict.Values;
            public IEqualityComparer<TKey> Comparer => _dict.Comparer;

            public TValue this[TKey key] { get => _dict[key]; set => InternalSetItem(key, value); }

            public int Count => _dict.Count;
            public bool IsReadOnly => _idict.IsReadOnly;

            private Dictionary<TKey, TValue> _dict;
            private IDictionary<TKey, TValue> _idict;

            public Dict() { _idict = _dict = new(); }
            public Dict(IDictionary<TKey, TValue> dictionary) { _idict = _dict = new(dictionary); }
            public Dict(IEnumerable<KeyValuePair<TKey, TValue>> collection) { _idict = _dict = new(collection); }
            public Dict(IEqualityComparer<TKey> comparer) { _idict = _dict = new(comparer); }
            public Dict(int capacity) { _idict = _dict = new(capacity); }
            public Dict(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) { _idict = _dict = new(dictionary, comparer); }
            public Dict(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) { _idict = _dict = new(collection, comparer); }
            public Dict(int capacity, IEqualityComparer<TKey> comparer) { _idict = _dict = new(capacity, comparer); }

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
}