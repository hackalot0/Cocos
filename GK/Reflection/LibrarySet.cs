using GK.Sets;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GK.Reflection
{
    public class LibrarySet : ObservableKeyedSet<Guid, Library>
    {
        public LibrarySet() { }
        public LibrarySet(int capacity) : base(capacity) { }
        public LibrarySet(IDictionary<Guid, Library> dictionary) : base(dictionary) { }
        public LibrarySet(IEnumerable<KeyValuePair<Guid, Library>> collection) : base(collection) { }
        public LibrarySet(IEqualityComparer<Guid> comparer) : base(comparer) { }
        public LibrarySet(int capacity, IEqualityComparer<Guid> comparer) : base(capacity, comparer) { }
        public LibrarySet(IDictionary<Guid, Library> dictionary, IEqualityComparer<Guid> comparer) : base(dictionary, comparer) { }
        public LibrarySet(IEnumerable<KeyValuePair<Guid, Library>> collection, IEqualityComparer<Guid> comparer) : base(collection, comparer) { }
        protected LibrarySet(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override Guid GetKeyForItem(Library item) => item.Key;
    }
}