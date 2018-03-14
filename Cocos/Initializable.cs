using System.Threading;
using System.Threading.Tasks;

namespace Cocos
{
    public abstract class Initializable : Disposable, IInitializable
    {
        public bool IsInitialized => isInitialized;
        public bool IsInitializing => isInitializing;

        private bool initAsync;
        private bool isInitialized;
        private bool isInitializing;

        protected Initializable(bool initAsync = false) => this.initAsync = initAsync;

        public virtual void Initialize()
        {
            if (isInitializing || isInitialized) return;
            isInitializing = true;

            if (initAsync)
            {

            }
            else
            {
                Init();
            }

            isInitialized = !(isInitializing = false);
        }

        private void InitializationDone(Task _initTask)
        {

        }

        protected abstract void Init();

        protected override void Dispose(bool managed) => isInitialized = false;
    }
}