using Cocos.Core.Events;

namespace Cocos.Core.Sets
{
    public interface IObservableSet<T>
    {
        Observable.Options Options { get; }

        event Sender.Event? Cleared;
        event Item<T>.Event? ItemAdded;
        event Item<T>.Event? ItemRemoved;
        event Item<T>.Replace.Event? ItemReplaced;
    }
}