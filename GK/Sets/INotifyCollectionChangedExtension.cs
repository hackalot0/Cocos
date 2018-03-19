using System;
using System.Collections.Specialized;

namespace GK.Sets
{
    public static class INotifyCollectionChangedExtension
    {
        public static Observer<T> Observe<T>(this INotifyCollectionChanged source, Action<INotifyCollectionChanged, T> itemAdded, Action<INotifyCollectionChanged, T> itemRemoved) => new Observer<T>(source) { ItemAddedAction = itemAdded, ItemRemovedAction = itemRemoved, RemoveOnClear = true };
        public static Observer<T> Observe<T>(this INotifyCollectionChanged source, Action<INotifyCollectionChanged, T> itemAdded, Action<INotifyCollectionChanged, T> itemRemoved, Action<INotifyCollectionChanged> cleared = null) => new Observer<T>(source) { ItemAddedAction = itemAdded, ItemRemovedAction = itemRemoved, CollectionResetAction = cleared, RemoveOnClear = false };
        public static Observer<TKey, TValue> Observe<TKey, TValue>(this INotifyCollectionChanged source, Action<INotifyCollectionChanged, TKey> itemAdded, Action<INotifyCollectionChanged, TKey> itemRemoved) => new Observer<TKey, TValue>(source) { ItemAddedAction = itemAdded, ItemRemovedAction = itemRemoved, RemoveOnClear = true };
        public static Observer<TKey, TValue> Observe<TKey, TValue>(this INotifyCollectionChanged source, Action<INotifyCollectionChanged, TKey> itemAdded, Action<INotifyCollectionChanged, TKey> itemRemoved, Action<INotifyCollectionChanged> cleared = null) => new Observer<TKey, TValue>(source) { ItemAddedAction = itemAdded, ItemRemovedAction = itemRemoved, CollectionResetAction = cleared, RemoveOnClear = false };
    }
}