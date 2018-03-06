namespace Cocos.Events
{
    public class ChangeEventArgs<T> : ItemEventArgs<T>
    {
        public T NewItem => newItem;
        private T newItem;

        public ChangeEventArgs(T item, T newItem) : base(item) { this.newItem = newItem; }
    }
}