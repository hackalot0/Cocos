using System;
using System.Collections.Generic;

namespace GK.Reflection
{
    public class TypeManagerCache
    {
        public Dictionary<Type, Guid> TypeGuids => typeGuids;
        public Dictionary<Guid, Type> GuidTypes => guidTypes;
        public Dictionary<Type, IEnumerable<Type>> TypeInterfaces => typeInterfaces;

        private Dictionary<Type, Guid> typeGuids;
        private Dictionary<Guid, Type> guidTypes;
        private Dictionary<Type, IEnumerable<Type>> typeInterfaces;

        public TypeManagerCache()
        {
            typeGuids = new Dictionary<Type, Guid>();
            guidTypes = new Dictionary<Guid, Type>();
            typeInterfaces = new Dictionary<Type, IEnumerable<Type>>();
        }

        public void Clear()
        {
            typeGuids.Clear();
            guidTypes.Clear();
            typeInterfaces.Clear();
        }
    }
}