using System;

namespace GK
{
    public interface IInitializable : IDisposable
    {
        bool IsInitialized { get; }
        bool IsInitializing { get; }

        void Initialize(bool initAsync = false);
    }
}