using Cocos.Core.Events;

namespace Cocos.Core
{
    public interface IInitializable : IDisposable
    {
        event Sender.Event? Initializing;
        event Sender.Event? Initialized;

        void Initialize();
    }
}