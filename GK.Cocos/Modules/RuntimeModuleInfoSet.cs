using GK.Sets;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GK.Cocos.Modules
{
    public class RuntimeModuleInfoSet : ObservableKeyedSet<Guid, RuntimeModuleInfo>
    {
        public RuntimeModuleInfoSet()
        {
        }
        public RuntimeModuleInfoSet(int capacity) : base(capacity)
        {
        }
        public RuntimeModuleInfoSet(IDictionary<Guid, RuntimeModuleInfo> dictionary) : base(dictionary)
        {
        }
        public RuntimeModuleInfoSet(IEnumerable<KeyValuePair<Guid, RuntimeModuleInfo>> collection) : base(collection)
        {
        }
        public RuntimeModuleInfoSet(IEqualityComparer<Guid> comparer) : base(comparer)
        {
        }
        public RuntimeModuleInfoSet(int capacity, IEqualityComparer<Guid> comparer) : base(capacity, comparer)
        {
        }
        public RuntimeModuleInfoSet(IDictionary<Guid, RuntimeModuleInfo> dictionary, IEqualityComparer<Guid> comparer) : base(dictionary, comparer)
        {
        }
        public RuntimeModuleInfoSet(IEnumerable<KeyValuePair<Guid, RuntimeModuleInfo>> collection, IEqualityComparer<Guid> comparer) : base(collection, comparer)
        {
        }
        protected RuntimeModuleInfoSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}