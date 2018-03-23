using GK.Events;
using System;

namespace GK.Threading
{
    public interface IWorker : IDisposable
    {
        ActionState ActionState { get; }
        Exception Error { get; }
        bool IsError { get; }
        bool ThrowOnError { get; set; }
        bool WaitForAction { get; set; }
        int WorkerID { get; }
        WorkerState WorkerState { get; }

        event WorkerEventHandler Actions;
        event EventHandler<ChangeEventArgs<WorkerState>> WorkerStateChanged;

        void Resume();
        void Start(object state = null);
        void Stop();
        void Suspend();
        void Wait();
        bool Wait(TimeSpan timeout);
    }
}