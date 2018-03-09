using System;

namespace Cocos
{
    public abstract class Disposable : IDisposable
    {
        public bool IsDisposed => isDisposed;
        public bool IsDisposing => isDisposing;

        private bool isDisposed;
        private bool isDisposing;

        protected abstract void Dispose(bool managed);

        ~Disposable()
        {
            if (isDisposing || isDisposed) return;
            isDisposing = true;
            Dispose(false);
            isDisposed = !(isDisposing = false);
        }

        public void Dispose()
        {
            if (isDisposing || isDisposed) return;
            isDisposing = true;
            Dispose(true);
            GC.SuppressFinalize(this);
            isDisposed = !(isDisposing = false);
        }
    }
}