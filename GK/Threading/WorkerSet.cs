using System.Collections.Generic;
using System.Runtime.Serialization;
using GK.Sets;

namespace GK.Threading
{
    public class WorkerSet : ObservableKeyedSet<int, TaskWorker>
    {
        public WorkerSet() { }
        public WorkerSet(int capacity) : base(capacity) { }
        public WorkerSet(IDictionary<int, TaskWorker> dictionary) : base(dictionary) { }
        public WorkerSet(IEnumerable<KeyValuePair<int, TaskWorker>> collection) : base(collection) { }
        public WorkerSet(IEqualityComparer<int> comparer) : base(comparer) { }
        public WorkerSet(int capacity, IEqualityComparer<int> comparer) : base(capacity, comparer) { }
        public WorkerSet(IDictionary<int, TaskWorker> dictionary, IEqualityComparer<int> comparer) : base(dictionary, comparer) { }
        public WorkerSet(IEnumerable<KeyValuePair<int, TaskWorker>> collection, IEqualityComparer<int> comparer) : base(collection, comparer) { }
        protected WorkerSet(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override int GetKeyForItem(TaskWorker item) => item.WorkerID;
    }
}