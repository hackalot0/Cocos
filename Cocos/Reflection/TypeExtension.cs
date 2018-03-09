using System;

namespace Cocos.Reflection
{
    public static class TypeExtension
    {
        public static string SafeGetName(this Type type) => type == null ? null : (type.AssemblyQualifiedName ?? string.Join(" | ", type.FullName ?? type.Name, type.Assembly.SafeGetName()));
    }
}