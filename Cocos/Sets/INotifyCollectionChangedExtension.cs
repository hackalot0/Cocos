using System;
using System.Collections.Specialized;

namespace Cocos.Sets
{
    public static class INotifyCollectionChangedExtension
    {
        public static Observer<T> Observe<T>(this INotifyCollectionChanged source, Action<INotifyCollectionChanged, T> itemAdded = null, Action<INotifyCollectionChanged, T> itemRemoved = null, Action<INotifyCollectionChanged> cleared = null) => new Observer<T>(source) { ItemAddedAction = itemAdded, ItemRemovedAction = itemRemoved, CollectionResetAction = cleared };
    }
}