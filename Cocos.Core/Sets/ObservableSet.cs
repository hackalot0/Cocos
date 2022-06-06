using Cocos.Core.Events;
using System.Collections.ObjectModel;

namespace Cocos.Core.Sets
{
    public class ObservableSet<T> : Collection<T>, IObservableSet<T>
    {
        public event Sender.Event? Cleared;
        public event Item<T>.Event? ItemAdded;
        public event Item<T>.Event? ItemRemoved;
        public event Item<T>.Replace.Event? ItemReplaced;

        public Observable.Options Options { get; private set; }

        public ObservableSet()
        {
            Options = new();
        }
        public ObservableSet(IList<T> list) : base(list)
        {
            Options = new();
        }

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
}