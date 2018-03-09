using Cocos.Sets;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Cocos.Reflection
{
    public class TypeManagerStateDict : ObservableDict<Assembly, TypeManagerState>
    {
        public TypeManagerStateDict() { }
        public TypeManagerStateDict(int capacity) : base(capacity) { }
        public TypeManagerStateDict(IDictionary<Assembly, TypeManagerState> dictionary) : base(dictionary) { }
        public TypeManagerStateDict(IEnumerable<KeyValuePair<Assembly, TypeManagerState>> collection) : base(collection) { }
        public TypeManagerStateDict(IEqualityComparer<Assembly> comparer) : base(comparer) { }
        public TypeManagerStateDict(int capacity, IEqualityComparer<Assembly> comparer) : base(capacity, comparer) { }
        public TypeManagerStateDict(IDictionary<Assembly, TypeManagerState> dictionary, IEqualityComparer<Assembly> comparer) : base(dictionary, comparer) { }
        public TypeManagerStateDict(IEnumerable<KeyValuePair<Assembly, TypeManagerState>> collection, IEqualityComparer<Assembly> comparer) : base(collection, comparer) { }
        protected TypeManagerStateDict(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}