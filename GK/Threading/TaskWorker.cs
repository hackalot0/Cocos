using System;
using System.Threading;
using System.Threading.Tasks;

namespace GK.Threading
{
    public class TaskWorker : Worker, IWorker
    {
        public Task Task => task;
        public override int WorkerID => task.Id;

        private Task task;

        protected override void Initialize()
        {
            task = new Task(TaskProcessor);
        }

        protected override void InternalStart() => task.Start();
        protected override void InternalWait() => task.Wait();
        protected override bool InternalWait(TimeSpan timeout) => task.Wait(timeout);
        protected override void InternalCleanup()
        {
            task.Dispose();
            task = null;
        }

        private void TaskProcessor()
        {
            bool run = true;
            WorkerEventHandler weh = null;
            WorkerState = WorkerState.Running;
            while (run && !IsDisposing && !IsDisposed)
            {
                switch (WorkerState)
                {
                    case WorkerState.Running:
                        try
                        {
                            if ((weh = GetActions()) == null)
                            {
                                if (WaitForAction) Thread.Sleep(1);
                                else Stop();
                            }
                            else weh.Invoke(ActionState);
                        }
                        catch (Exception error)
                        {
                            Error = error;
                            Stop();
                            if (ThrowOnError) throw;
                        }
                        break;
                    case WorkerState.Suspending: WorkerState = WorkerState.Suspended; break;
                    case WorkerState.Suspended: Thread.Sleep(1); break;
                    case WorkerState.Resuming: WorkerState = WorkerState.Running; break;

                    default:
                    case WorkerState.Stopping: run = false; break;
                }
            }
            WorkerState = WorkerState.Stopped;
        }
    }
}