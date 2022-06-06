namespace Cocos.Core
{
    public abstract class Disposable : IDisposable
    {
        public event Sender.Event? Disposing;
        public event Sender.Event? Disposed;

        protected object SyncRoot { get; private set; }

        public bool IsDisposing { get; private set; }
        public bool IsDisposed { get; private set; }

        public Disposable()
        {
            SyncRoot = new object();
            IsDisposed = false;
            IsDisposing = false;
        }

        ~Disposable() => HandleDispose(false);

        public void Dispose() => HandleDispose(true);
        protected virtual void Dispose(bool disposing) { }

        protected virtual void OnDisposing() => Disposing?.Invoke(new() { Sender = this });
        protected virtual void OnDisposed() => Disposed?.Invoke(new() { Sender = this });

        private void HandleDispose(bool disposing)
        {
            if (IsDisposing || IsDisposed) return;
            IsDisposing = true;
            OnDisposing();
            try
            {
                Dispose(disposing);
                IsDisposed = true;
                OnDisposed();
            }
            catch
            {
                IsDisposing = false;
                throw;
            }
            finally
            {
                IsDisposing = false;
            }
        }
    }
}