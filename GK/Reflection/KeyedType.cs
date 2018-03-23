using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace GK.Reflection
{
    [DebuggerDisplay("{Key}: {Type}")]
    public class KeyedType
    {
        public Guid Key => key;
        public Type Type => type;

        private Guid key;
        private Type type;

        public KeyedType(Type type) => key = (this.type = type).SafeGetName().GetBytes().GetHashCode<MD5>().GetGuid();

        public static KeyedType FromType(Type type) => new KeyedType(type);
        public static implicit operator KeyedType(Type type) => new KeyedType(type);
        public static implicit operator Type(KeyedType keyedType) => keyedType.Type;
    }
}