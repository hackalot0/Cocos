using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Cocos.Sets
{
    public class Observer<T> : Disposable
    {
        public INotifyCollectionChanged Source => source;

        public bool RemoveOnClear {
            get => removeOnClear;
            set {
                if (removeOnClear == value) return;
                removeOnClear = value;
                RefreshRemoveOnChange();
            }
        }
        public Action<INotifyCollectionChanged, T> ItemAddedAction;
        public Action<INotifyCollectionChanged, T> ItemRemovedAction;
        public Action<INotifyCollectionChanged> CollectionResetAction;

        private bool removeOnClear;
        private List<T> shadowList;
        private INotifyCollectionChanged source;

        public Observer(INotifyCollectionChanged source) => (this.source = source).CollectionChanged += CollectionChanged;

        protected virtual void RefreshRemoveOnChange()
        {
            if (removeOnClear && shadowList == null) shadowList = new List<T>(source as IEnumerable<T>);
            else
            {
                shadowList?.Clear();
                shadowList = null;
            }
        }
        protected virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ReferenceEquals(sender, source))
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        if (removeOnClear)
                        {
                            foreach (T item in shadowList) ItemRemovedAction?.Invoke(source, item);
                            shadowList.Clear();
                        }
                        else CollectionResetAction?.Invoke(source);
                        break;
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
    public class Observer<TKey, TValue> : Disposable
    {
        public INotifyCollectionChanged Source => source;

        public bool RemoveOnClear {
            get => removeOnClear;
            set {
                if (removeOnClear == value) return;
                removeOnClear = value;
                RefreshRemoveOnChange();
            }
        }
        public Action<INotifyCollectionChanged> CollectionResetAction;
        public Action<INotifyCollectionChanged, TKey> ItemAddedAction;
        public Action<INotifyCollectionChanged, TKey> ItemRemovedAction;

        private bool removeOnClear;
        private List<TKey> shadowList;
        private INotifyCollectionChanged source;

        public Observer(INotifyCollectionChanged source) => (this.source = source).CollectionChanged += CollectionChanged;

        protected virtual void RefreshRemoveOnChange()
        {
            if (removeOnClear && shadowList == null) shadowList = new List<TKey>((source as IDictionary<TKey, TValue>).Keys);
            else
            {
                shadowList?.Clear();
                shadowList = null;
            }
        }
        protected virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ReferenceEquals(sender, source))
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        if (removeOnClear)
                        {
                            foreach (TKey item in shadowList) ItemRemovedAction?.Invoke(source, item);
                            shadowList.Clear();
                        }
                        else CollectionResetAction?.Invoke(source);
                        break;
                    case NotifyCollectionChangedAction.Add: foreach (TKey item in e.NewItems) ItemAddedAction?.Invoke(source, item); break;
                    case NotifyCollectionChangedAction.Remove: foreach (TKey item in e.OldItems) ItemRemovedAction?.Invoke(source, item); break;
                    case NotifyCollectionChangedAction.Replace:
                        foreach (TKey item in e.OldItems) ItemRemovedAction?.Invoke(source, item);
                        foreach (TKey item in e.NewItems) ItemAddedAction?.Invoke(source, item);
                        break;
                }
            }
        }

        protected override void Dispose(bool managed) => source.CollectionChanged += CollectionChanged;
    }
}