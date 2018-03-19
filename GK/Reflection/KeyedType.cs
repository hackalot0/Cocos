using GK.Sets;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace GK.Reflection
{
    [DebuggerDisplay("{Key}: {Type}")]
    public class KeyedType : KeyedItem<Type>
    {
        public Type Type => Item;

        public KeyedType(Type type) : base(GetKey, type) { }

        public static KeyedType FromType(Type type) => new KeyedType(type);
        public static implicit operator KeyedType(Type type) => new KeyedType(type);
        public static implicit operator Type(KeyedType keyedType) => keyedType.Type;

        private static Guid GetKey(Type type) => type.SafeGetName().GetBytes().GetHashCode<MD5>().GetGuid();
    }
}