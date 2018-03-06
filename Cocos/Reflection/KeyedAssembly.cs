using Cocos.Sets;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;

namespace Cocos.Reflection
{
    [DebuggerDisplay("{Key}: {Assembly}")]
    public class KeyedAssembly : KeyedItem<Assembly>
    {
        public Assembly Assembly => Item;

        public KeyedAssembly(Assembly assembly) : base(GetKey, assembly) { }

        public static KeyedAssembly FromAssembly(Assembly assembly) => new KeyedAssembly(assembly);
        public static implicit operator KeyedAssembly(Assembly assembly) => new KeyedAssembly(assembly);
        public static implicit operator Assembly(KeyedAssembly keyedAssembly) => keyedAssembly.Assembly;

        private static Guid GetKey(Assembly assembly)
        {
            AssemblyName an = assembly.GetName();
            return string.Join("|", an.FullName, an.Name).GetBytes().GetHashCode<MD5>().GetGuid();
        }
    }
}
