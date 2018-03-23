using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;

namespace GK.Reflection
{
    [DebuggerDisplay("[{Key}] {Assembly}")]
    public class KeyedAssembly
    {
        public Guid Key => key;
        public Assembly Assembly => assembly;

        private Guid key;
        private Assembly assembly;

        public KeyedAssembly(Assembly assembly) => key = (this.assembly = assembly).SafeGetName().GetBytes().GetHashCode<MD5>().GetGuid();

        public static KeyedAssembly FromAssembly(Assembly assembly) => new KeyedAssembly(assembly);
        public static implicit operator KeyedAssembly(Assembly assembly) => new KeyedAssembly(assembly);
        public static implicit operator Assembly(KeyedAssembly keyedAssembly) => keyedAssembly.Assembly;
    }
}