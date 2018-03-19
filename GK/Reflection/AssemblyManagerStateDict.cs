using GK.Sets;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GK.Reflection
{
    public class AssemblyManagerStateDict : ObservableDict<AppDomain, AssemblyManagerState>
    {
        public AssemblyManagerStateDict() { }
        public AssemblyManagerStateDict(int capacity) : base(capacity) { }
        public AssemblyManagerStateDict(IDictionary<AppDomain, AssemblyManagerState> dictionary) : base(dictionary) { }
        public AssemblyManagerStateDict(IEnumerable<KeyValuePair<AppDomain, AssemblyManagerState>> collection) : base(collection) { }
        public AssemblyManagerStateDict(IEqualityComparer<AppDomain> comparer) : base(comparer) { }
        public AssemblyManagerStateDict(int capacity, IEqualityComparer<AppDomain> comparer) : base(capacity, comparer) { }
        public AssemblyManagerStateDict(IDictionary<AppDomain, AssemblyManagerState> dictionary, IEqualityComparer<AppDomain> comparer) : base(dictionary, comparer) { }
        public AssemblyManagerStateDict(IEnumerable<KeyValuePair<AppDomain, AssemblyManagerState>> collection, IEqualityComparer<AppDomain> comparer) : base(collection, comparer) { }
        protected AssemblyManagerStateDict(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}