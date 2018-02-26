using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Cocos.Reflection
{
    [DebuggerDisplay("{Key}: {Assembly}")]
    public class KeyedAssembly
    {
        public Guid Key => key;
        public Assembly Assembly => assembly;

        private Guid key;
        private Assembly assembly;

        public KeyedAssembly(Assembly assembly)
        {
            this.assembly = assembly;
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (assembly == null) key = Guid.Empty;
            else
            {
                AssemblyName an = assembly.GetName();
                key = string.Join("|", an.FullName, an.Name).GetBytes().GetHashCode<MD5>().GetGuid();
            }
        }

        public static KeyedAssembly FromAssembly(Assembly assembly) => new KeyedAssembly(assembly);

        public static implicit operator KeyedAssembly(Assembly assembly) => new KeyedAssembly(assembly);
        public static implicit operator Assembly(KeyedAssembly keyedAssembly) => keyedAssembly.Assembly;
    }
}