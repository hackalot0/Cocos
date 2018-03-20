using GK.Sets;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GK.Cocos.Modules.Stores
{
    public class ModuleFileSet : ObservableKeyedSet<Guid, ModuleFile>
    {
        public ModuleStore ParentStore { get; set; }

        public ModuleFileSet() { }
        public ModuleFileSet(int capacity) : base(capacity) { }
        public ModuleFileSet(IDictionary<Guid, ModuleFile> dictionary) : base(dictionary) { }
        public ModuleFileSet(IEnumerable<KeyValuePair<Guid, ModuleFile>> collection) : base(collection) { }
        public ModuleFileSet(IEqualityComparer<Guid> comparer) : base(comparer) { }
        public ModuleFileSet(int capacity, IEqualityComparer<Guid> comparer) : base(capacity, comparer) { }
        public ModuleFileSet(IDictionary<Guid, ModuleFile> dictionary, IEqualityComparer<Guid> comparer) : base(dictionary, comparer) { }
        public ModuleFileSet(IEnumerable<KeyValuePair<Guid, ModuleFile>> collection, IEqualityComparer<Guid> comparer) : base(collection, comparer) { }
        protected ModuleFileSet(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override Guid GetKeyForItem(ModuleFile item) => item.Key;
    }
}