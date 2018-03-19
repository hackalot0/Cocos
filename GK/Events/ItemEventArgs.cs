using System;

namespace GK.Events
{
    public class ItemEventArgs<T> : EventArgs
    {
        public T Item => item;
        private T item;

        public ItemEventArgs(T item) { this.item = item; }
    }
}