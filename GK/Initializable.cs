using System;
using System.Threading;

namespace GK
{
    public abstract class Initializable : Disposable, IInitializable
    {
        public event EventHandler Initialized;

        public bool IsInitialized => isInitialized;
        public bool IsInitializing => isInitializing;

        private bool initAsync;
        private bool isInitialized;
        private bool isInitializing;
        private Thread initThread;

        public virtual void Initialize(bool initAsync = false)
        {
            if (isInitializing || isInitialized) return;
            isInitializing = true;
            
            if (this.initAsync = initAsync)
            {
                initThread = new Thread(Initializer) { Name = string.Format("Init - {0}", GetType().FullName) };
                initThread.Start();
            }
            else Initializer();
        }

        protected abstract void Init();
        protected override void Dispose(bool managed) => isInitialized = false;
        protected virtual void OnInitialized(EventArgs e) => Initialized?.Invoke(this, e);

        private void Initializer()
        {
            Init();
            isInitialized = !(isInitializing = false);
            OnInitialized(EventArgs.Empty);
        }
    }
}