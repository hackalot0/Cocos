using System.Collections.Generic;

namespace GK.Threading
{
    public class WorkerManager : Disposable
    {
        public WorkerSet Workers => workers;
        private WorkerSet workers;

        private object locker = new object();
        private static List<WorkerManager> managers;

        public WorkerManager()
        {
            lock (locker) if (managers == null) managers = new List<WorkerManager>();
            workers = new WorkerSet();
            managers.Add(this);
        }

        protected override void Dispose(bool managed)
        {
            managers.Remove(this);
            lock (locker) if (managers.Count == 0) managers = null;
        }
    }
}