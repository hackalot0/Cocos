using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocos.Multitasking
{
    public class TaskWorker : Initializable
    {
        public int ID => id;
        public Task Task => task;

        private int id;
        private Task task;
        private Exception error;

        public TaskWorker() : base(initAsync: false) { }

        public void Run()
        {
            if (options.RunSynchronous)
            {
                ThreadRunner(this);
            }
            else
            {
                task.Start();
            }
        }
        public void Abort()
        {
        }

        protected override void Init()
        {
            if (options.RunSynchronous)
            {
                DisposeTask();
            }
            else
            {
                CreateTask();
            }
        }

        protected virtual void CreateTask()
        {
            if (task != null) DisposeTask();
            task = new Task(ThreadProcessor, this);
        }
        protected virtual void DisposeTask()
        {
            if (thread != null)
            {
                Abort();
                thread = null;
            }
        }

        private static void ThreadProcessor(object obj)
        {
            try { ThreadRunner(obj as TaskWorker); }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }
        private static void ThreadRunner(TaskWorker tw)
        {
            try { tw.options.Action(); }
            catch (Exception error) { tw.error = error; }
        }
    }
}