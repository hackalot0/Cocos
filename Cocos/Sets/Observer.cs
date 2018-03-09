using System;
using System.Collections.Specialized;

namespace Cocos.Sets
{
    public class Observer<T> : Disposable
    {
        public INotifyCollectionChanged Source => source;

        public Action<INotifyCollectionChanged, T> ItemAddedAction;
        public Action<INotifyCollectionChanged, T> ItemRemovedAction;
        public Action<INotifyCollectionChanged> CollectionResetAction;

        private INotifyCollectionChanged source;

        public Observer(INotifyCollectionChanged source) => (this.source = source).CollectionChanged += CollectionChanged;

        protected virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ReferenceEquals(sender, source))
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset: CollectionResetAction?.Invoke(source); break;
                    case NotifyCollectionChangedAction.Add: foreach (T item in e.NewItems) ItemAddedAction?.Invoke(source, item); break;
                    case NotifyCollectionChangedAction.Remove: foreach (T item in e.OldItems) ItemRemovedAction?.Invoke(source, item); break;
                    case NotifyCollectionChangedAction.Replace:
                        foreach (T item in e.OldItems) ItemRemovedAction?.Invoke(source, item);
                        foreach (T item in e.NewItems) ItemAddedAction?.Invoke(source, item);
                        break;
                }
            }
        }

        protected override void Dispose(bool managed) => source.CollectionChanged += CollectionChanged;
    }
}