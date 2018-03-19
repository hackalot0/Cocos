using GK.Sets;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace GK.Reflection
{
    [Serializable]
    public class KeyedAssemblySet : ObservableKeyedSet<Guid, KeyedAssembly>
    {
        public KeyedAssemblySet() { }
        public KeyedAssemblySet(IEnumerable<Assembly> items) { items.ForEach(x => Add(x)); }
        public KeyedAssemblySet(IEnumerable<KeyedAssembly> items) { items.ForEach(Add); }
        protected KeyedAssemblySet(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override Guid GetKeyForItem(KeyedAssembly item) => item.Key;
    }
}