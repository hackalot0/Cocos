using Cocos.Sets;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Cocos.Reflection
{
    [Serializable]
    public class KeyedAssemblySet : ObservableKeyedSet<Guid, Assembly>
    {
        public KeyedAssemblySet()
        {
        }
        public KeyedAssemblySet(int capacity) : base(capacity)
        {
        }
        public KeyedAssemblySet(IDictionary<Guid, Assembly> dictionary) : base(dictionary)
        {
        }
        public KeyedAssemblySet(IEnumerable<KeyValuePair<Guid, Assembly>> collection) : base(collection)
        {
        }
        public KeyedAssemblySet(IEqualityComparer<Guid> comparer) : base(comparer)
        {
        }
        public KeyedAssemblySet(Func<Assembly, Guid> keyRetriever) : base(keyRetriever)
        {
        }
        public KeyedAssemblySet(int capacity, IEqualityComparer<Guid> comparer) : base(capacity, comparer)
        {
        }
        public KeyedAssemblySet(IDictionary<Guid, Assembly> dictionary, IEqualityComparer<Guid> comparer) : base(dictionary, comparer)
        {
        }
        public KeyedAssemblySet(IEnumerable<KeyValuePair<Guid, Assembly>> collection, IEqualityComparer<Guid> comparer) : base(collection, comparer)
        {
        }
        protected KeyedAssemblySet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}