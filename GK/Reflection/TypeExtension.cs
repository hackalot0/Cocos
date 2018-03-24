using System;
using System.Linq;
using System.Security.Cryptography;

namespace GK.Reflection
{
    public static class TypeExtension
    {
        public static string SafeGetName(this Type type) => type == null ? null : (type.AssemblyQualifiedName ?? string.Join(" | ", type.FullName ?? type.Name, type.Assembly.SafeGetName()));
        public static Guid GetGuid(this Type type) => type == null ? Guid.Empty : type.SafeGetName().GetBytes().GetHashCode<MD5>().GetGuid();

        public static bool IsBaseTypeOf(this Type a, Type type)
        {
            type = type.BaseType;
            while (type != null)
            {
                if (type.IsEquivalentTo(a)) return true;
                type = type.BaseType;
            }
            return false;
        }
        public static bool IsEquivalentOrBaseTypeOf(this Type a, Type type)
        {
            while (type != null)
            {
                if (type.IsEquivalentTo(a)) return true;
                type = type.BaseType;
            }
            return false;
        }
    }
}