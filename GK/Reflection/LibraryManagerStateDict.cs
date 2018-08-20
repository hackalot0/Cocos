using GK.Sets;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GK.Reflection
{
    public class LibraryManagerStateDict : ObservableDict<AppDomain, LibraryManagerState>
    {
        public LibraryManagerStateDict() { }
        public LibraryManagerStateDict(int capacity) : base(capacity) { }
        public LibraryManagerStateDict(IDictionary<AppDomain, LibraryManagerState> dictionary) : base(dictionary) { }
        public LibraryManagerStateDict(IEnumerable<KeyValuePair<AppDomain, LibraryManagerState>> collection) : base(collection) { }
        public LibraryManagerStateDict(IEqualityComparer<AppDomain> comparer) : base(comparer) { }
        public LibraryManagerStateDict(int capacity, IEqualityComparer<AppDomain> comparer) : base(capacity, comparer) { }
        public LibraryManagerStateDict(IDictionary<AppDomain, LibraryManagerState> dictionary, IEqualityComparer<AppDomain> comparer) : base(dictionary, comparer) { }
        public LibraryManagerStateDict(IEnumerable<KeyValuePair<AppDomain, LibraryManagerState>> collection, IEqualityComparer<AppDomain> comparer) : base(collection, comparer) { }
        protected LibraryManagerStateDict(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}