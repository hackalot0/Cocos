using System;

namespace Cocos.Multitasking
{
    public class TaskManager : Initializable
    {
        public TaskWorkerSet ThreadWorkers => workers;

        private TaskWorkerSet workers;

        public TaskManager() => Initialize();

        protected override void Init()
        {
            workers = new TaskWorkerSet();
        }

        public TaskWorker CreateWorker(ThreadOptions options)
        {
            TaskWorker tw = new TaskWorker(options);
            tw.Initialize();
            workers.Add(tw);

            return tw;
        }
    }
}