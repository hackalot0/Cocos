using Cocos.Core.Events;

namespace Cocos.Core
{
    public abstract class Initializable : Disposable, IInitializable
    {
        public event Sender.Event Initializing;
        public event Sender.Event Initialized;

        public bool IsInitializing { get; private set; }
        public bool IsInitialized { get; private set; }

        protected virtual void OnInitializing() => Initializing?.Invoke(new() { Sender = this });
        protected virtual void OnInitialized() => Initialized?.Invoke(new() { Sender = this });

        public void Initialize()
        {
            if (IsInitializing || IsInitialized) return;
            IsInitializing = true;
            OnInitializing();
            try
            {
                Init();
                IsInitialized = true;
                OnInitialized();
            }
            catch
            {
                IsInitializing = false;
                throw;
            }
            finally
            {
                IsInitializing = false;
            }
        }

        protected abstract void Init();
    }
}