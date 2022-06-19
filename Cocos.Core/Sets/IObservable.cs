using Cocos.Core.Events;

namespace Cocos.Core.Sets
{
    public interface IObservable
    {
        Observable.Options Options { get; }

        public interface Set<T> : IObservable
        {
            event Sender.Event? Cleared;
            event Item<T>.Event? ItemAdded;
            event Item<T>.Event? ItemRemoved;
            event Item<T>.Replace.Event? ItemReplaced;
        }
        public interface Dict<TKey, TValue> : Set<KeyValuePair<TKey, TValue>> { }
    }
}