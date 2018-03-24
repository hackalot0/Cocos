using GK.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GK.Reflection
{
    public class TypeManagerState : StateBase
    {
        public Assembly Assembly => assembly;
        public TypeManagerCache Cache => cache;
        public ObservableKeyedSet<Guid, Type> KnownTypes => knownTypes;

        private Assembly assembly;
        private TypeManagerCache cache;
        private ObservableKeyedSet<Guid, Type> knownTypes;

        public TypeManagerState(Assembly assembly) => this.assembly = assembly;

        public void ClearCache() => cache.Clear();

        public Type GetType(Guid id) => cache.GuidTypes.TryGetValue(id, out Type r) ? r : null;
        public Guid GetGuid(Type type)
        {
            if (!cache.TypeGuids.TryGetValue(type, out Guid id))
            {
                cache.TypeGuids.Add(type, id = type.GetGuid());
                cache.GuidTypes.Add(id, type);
            }
            return id;
        }
        public IEnumerable<Type> GetInterfaces(Type type)
        {
            if (!cache.TypeInterfaces.TryGetValue(type, out IEnumerable<Type> r)) cache.TypeInterfaces.Add(type, r = type.GetInterfaces().ToList());
            return r;
        }

        protected override void Init()
        {
            knownTypes = new ObservableKeyedSet<Guid, Type>(assembly.GetTypes().AsParallel().ToDictionary(x => x.GetGuid());
            cache = new TypeManagerCache();
        }

        protected override void Dispose(bool managed)
        {
            knownTypes?.Clear();
            assembly = null;
            knownTypes = null;
            base.Dispose(managed);
        }
    }
}