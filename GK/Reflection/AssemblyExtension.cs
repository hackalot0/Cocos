using System.Reflection;

namespace GK.Reflection
{
    public static class AssemblyExtension
    {
        public static string SafeGetName(this Assembly assembly)
        {
            AssemblyName an = assembly?.GetName();
            return an == null ? null : (an.FullName ?? an.Name);
        }
        public static TypeManager GetTypeManager(this Assembly assembly) => new TypeManager(assembly);
    }
}