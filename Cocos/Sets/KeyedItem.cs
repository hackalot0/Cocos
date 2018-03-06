using Cocos.Events;
using System;

namespace Cocos.Sets
{
    public class KeyedItem<T>
    {
        public event EventHandler<ChangeEventArgs<T>> ItemChanged;

        public Guid Key => key;
        public Func<T, Guid> KeyRetriever => keyRetriever;
        public T Item { get => item; set => EventHelper.HandleChange(ref item, ref value, OnItemChanged); }

        private T item;
        private Guid key;
        private Func<T, Guid> keyRetriever;

        public KeyedItem(Func<T, Guid> keyRetriever) { this.keyRetriever = keyRetriever; }
        public KeyedItem(Func<T, Guid> keyRetriever, T item) : this(keyRetriever) { Item = item; }

        protected virtual void OnItemChanged(ChangeEventArgs<T> e)
        {
            Refresh();
            ItemChanged?.Invoke(this, e);
        }

        protected virtual void Refresh()
        {
            key = keyRetriever?.Invoke(item) ?? Guid.Empty;
        }
    }
}