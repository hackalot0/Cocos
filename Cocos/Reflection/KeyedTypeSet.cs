using Cocos.Sets;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cocos.Reflection
{
    [Serializable]
    public class KeyedTypeSet : ObservableKeyedSet<Guid, KeyedType>
    {
        public KeyedTypeSet() { }
        public KeyedTypeSet(IEnumerable<Type> items) { items.ForEach(x => Add(x)); }
        public KeyedTypeSet(IEnumerable<KeyedType> items) { items.ForEach(Add); }
        protected KeyedTypeSet(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override Guid GetKeyForItem(KeyedType item) => item.Key;
    }
}