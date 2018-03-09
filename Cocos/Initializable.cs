namespace Cocos
{
    public abstract class Initializable : Disposable, IInitializable
    {
        public bool IsInitialized => isInitialized;
        public bool IsInitializing => isInitializing;

        private bool isInitialized;
        private bool isInitializing;

        public virtual void Initialize()
        {
            if (isInitializing || isInitialized) return;
            isInitializing = true;
            try { Init(); } catch { isInitializing = false; throw; }
            isInitialized = !(isInitializing = false);
        }

        protected abstract void Init();

        protected override void Dispose(bool managed) => isInitialized = false;
    }
}