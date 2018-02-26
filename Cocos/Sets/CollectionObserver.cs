using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Cocos.Sets
{
    public class CollectionObserver<T>
    {
        public ICollection<T> BaseCollection => baseCollection;

        public Action<ICollection<T>, T> ItemAddedAction;
        public Action<ICollection<T>, T> ItemRemovedAction;
        public Action<ICollection<T>> CollectionResetAction;

        private ICollection<T> baseCollection;
        private INotifyCollectionChanged incc;

        public CollectionObserver(ICollection<T> baseCollection)
        {
            incc = ((this.baseCollection = baseCollection) is INotifyCollectionChanged ? baseCollection as INotifyCollectionChanged : null);
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (incc == null) throw new NotSupportedException();
            incc.CollectionChanged += CollectionChanged;
        }

        protected virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ReferenceEquals(sender, baseCollection))
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset: CollectionResetAction?.Invoke(baseCollection); break;
                    case NotifyCollectionChangedAction.Add: foreach (T item in e.NewItems) ItemAddedAction?.Invoke(baseCollection, item); break;
                    case NotifyCollectionChangedAction.Remove: foreach (T item in e.OldItems) ItemRemovedAction?.Invoke(baseCollection, item); break;
                    case NotifyCollectionChangedAction.Replace:
                        foreach (T item in e.OldItems) ItemRemovedAction?.Invoke(baseCollection, item);
                        foreach (T item in e.NewItems) ItemAddedAction?.Invoke(baseCollection, item);
                        break;
                }
            }
        }
    }
}