using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Cocos.Sets
{
    public class DictObserver<TKey, TValue>
    {
        public IDictionary<TKey, TValue> BaseDict => baseDict;

        public Action<IDictionary<TKey, TValue>, TKey> ItemAddedAction;
        public Action<IDictionary<TKey, TValue>, TKey> ItemRemovedAction;
        public Action<IDictionary<TKey, TValue>> CollectionResetAction;

        private IDictionary<TKey, TValue> baseDict;
        private INotifyCollectionChanged incc;

        public DictObserver(IDictionary<TKey, TValue> baseDict)
        {
            incc = ((this.baseDict = baseDict) is INotifyCollectionChanged ? baseDict as INotifyCollectionChanged : null);
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (incc == null) throw new NotSupportedException();
            incc.CollectionChanged += CollectionChanged;
        }

        protected virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ReferenceEquals(sender, baseDict))
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset: CollectionResetAction?.Invoke(baseDict); break;
                    case NotifyCollectionChangedAction.Add: foreach (TKey item in e.NewItems) ItemAddedAction?.Invoke(baseDict, item); break;
                    case NotifyCollectionChangedAction.Remove: foreach (TKey item in e.OldItems) ItemRemovedAction?.Invoke(baseDict, item); break;
                    case NotifyCollectionChangedAction.Replace:
                        foreach (TKey item in e.OldItems) ItemRemovedAction?.Invoke(baseDict, item);
                        foreach (TKey item in e.NewItems) ItemAddedAction?.Invoke(baseDict, item);
                        break;
                }
            }
        }
    }
}