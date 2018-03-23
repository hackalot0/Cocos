namespace GK.Threading
{
    public class ActionState
    {
        public IWorker Worker => worker;

        public object State { get; set; }
        public object Result { get; set; }
        public bool IsFinished { get; set; }

        private IWorker worker;

        public ActionState(IWorker worker) => this.worker = worker;
    }
}