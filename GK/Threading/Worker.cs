using GK.Events;
using GK.Sets;
using System;

namespace GK.Threading
{
    public abstract class Worker : Disposable, IWorker
    {
        public event WorkerEventHandler Actions;
        public event EventHandler<ChangeEventArgs<WorkerState>> WorkerStateChanged;

        public static ObservableSet<Worker> Workers => workers;

        public virtual bool IsError => Error != null;

        public virtual bool ThrowOnError { get; set; }
        public virtual bool WaitForAction { get; set; }
        public virtual int WorkerID { get; protected set; }
        public virtual Exception Error { get; protected set; }
        public virtual ActionState ActionState { get; protected set; }
        public virtual WorkerState WorkerState { get => workerState; protected set => EventHelper.HandleChange(ref workerState, ref value, OnWorkerStateChanged); }

        private WorkerState workerState;
        private object locker = new object();
        private static ObservableSet<Worker> workers;

        public Worker()
        {
            lock (locker) if (workers == null) workers = new ObservableSet<Worker>();
            workers.Add(this);
            WorkerState = WorkerState.Creating;
            Initialize();
            WorkerState = WorkerState.Ready;
        }

        public virtual void Start(object state = null)
        {
            switch (WorkerState)
            {
                case WorkerState.Ready:
                    WorkerState = WorkerState.Starting;
                    ActionState = new ActionState(this) { State = state };
                    InternalStart();
                    break;
                case WorkerState.Suspending:
                case WorkerState.Suspended: Resume(); break;
            }
        }
        public void Suspend()
        {
            if (WorkerState != WorkerState.Running) return;
            WorkerState = WorkerState.Suspending;
            InternalSuspend();
        }
        public void Resume()
        {
            if (WorkerState != WorkerState.Suspended) return;
            WorkerState = WorkerState.Resuming;
            InternalResume();
        }
        public void Stop()
        {
            switch (WorkerState)
            {
                case WorkerState.Starting:
                case WorkerState.Running:
                case WorkerState.Suspending:
                case WorkerState.Suspended:
                case WorkerState.Resuming: WorkerState = WorkerState.Stopping; InternalStop(); break;
            }
        }
        public void Wait()
        {
            switch (WorkerState)
            {
                case WorkerState.Starting:
                case WorkerState.Running:
                case WorkerState.Suspending:
                case WorkerState.Suspended:
                case WorkerState.Resuming:
                case WorkerState.Stopping: InternalWait(); break;
            }
        }
        public bool Wait(TimeSpan timeout)
        {
            switch (WorkerState)
            {
                case WorkerState.Starting:
                case WorkerState.Running:
                case WorkerState.Suspending:
                case WorkerState.Suspended:
                case WorkerState.Resuming:
                case WorkerState.Stopping: return InternalWait(timeout);
            }
            return true;
        }

        protected virtual void OnWorkerStateChanged(ChangeEventArgs<WorkerState> e) => WorkerStateChanged?.Invoke(this, e);

        protected virtual void InternalStart() { }
        protected virtual void InternalSuspend() { }
        protected virtual void InternalResume() { }
        protected virtual void InternalStop() { }
        protected virtual void InternalWait() { }
        protected abstract bool InternalWait(TimeSpan timeout);

        protected abstract void Initialize();
        protected abstract void InternalCleanup();
        protected WorkerEventHandler GetActions() => Actions;

        protected override void Dispose(bool managed)
        {
            Stop();
            Wait();
            InternalCleanup();
            workers.Remove(this);
        }
    }
}