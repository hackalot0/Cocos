using System;
using System.Reflection;
using System.Security.Cryptography;

namespace GK.Reflection
{
    public class Library
    {
        public Guid Key => key;
        public String Name => name;
        public Assembly Assembly => assembly;
        public AssemblyName AssemblyName => assemblyName;

        private Guid key;
        private String name;
        private Assembly assembly;
        private AssemblyName assemblyName;

        public Library(Assembly assembly)
        {
            if ((this.assembly = assembly) == null)
            {
                key = Guid.Empty;
                name = null;
                assemblyName = null;
            }
            else key = (name = ((assemblyName = assembly?.GetName()).FullName ?? assemblyName.Name)).GetGuid<MD5>();
        }

        public static implicit operator Library(Assembly assembly) => FromAssembly(assembly);
        public static implicit operator Assembly(Library library) => library.Assembly;

        public static Library FromAssembly(Assembly assembly) => new Library(assembly);
    }
}