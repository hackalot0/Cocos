using System;

namespace Cocos
{
    public interface IInitializable : IDisposable
    {
        bool IsInitialized { get; }
        bool IsInitializing { get; }

        void Initialize();
    }
}