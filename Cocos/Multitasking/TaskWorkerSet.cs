using Cocos.Sets;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cocos.Multitasking
{
    public class TaskWorkerSet : ObservableKeyedSet<int, TaskWorker>
    {
        public TaskWorkerSet() { }
        public TaskWorkerSet(int capacity) : base(capacity) { }
        public TaskWorkerSet(IDictionary<int, TaskWorker> dictionary) : base(dictionary) { }
        public TaskWorkerSet(IEnumerable<KeyValuePair<int, TaskWorker>> collection) : base(collection) { }
        public TaskWorkerSet(IEqualityComparer<int> comparer) : base(comparer) { }
        public TaskWorkerSet(int capacity, IEqualityComparer<int> comparer) : base(capacity, comparer) { }
        public TaskWorkerSet(IDictionary<int, TaskWorker> dictionary, IEqualityComparer<int> comparer) : base(dictionary, comparer) { }
        public TaskWorkerSet(IEnumerable<KeyValuePair<int, TaskWorker>> collection, IEqualityComparer<int> comparer) : base(collection, comparer) { }
        protected TaskWorkerSet(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override int GetKeyForItem(TaskWorker item) => item == null ? 0 : item.ID;
    }
}