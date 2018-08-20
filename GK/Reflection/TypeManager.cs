using GK.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GK.Reflection
{
    public class TypeManager : Disposable
    {
        public Assembly Assembly => state.Assembly;
        public static TypeManagerStateDict States { get; private set; }
        public ObservableKeyedSet<Guid, Type> KnownTypes => state.KnownTypes;

        private TypeManagerState state;
        private readonly object locker = new object();

        public TypeManager(Assembly assembly) => Initialize(assembly);

        protected virtual void Initialize(Assembly assembly)
        {
            lock (locker)
            {
                if (States == null) States = new TypeManagerStateDict();
                if (!States.TryGetValue(assembly, out state)) States.Add(assembly, state = new TypeManagerState(assembly));
                state.BindingCount += 1;
                state.Initialize();
            }
        }

        public void ClearCache() => state.ClearCache();

        public Type GetType(Guid id) => state.GetType(id);
        public Guid GetGuid(Type type) => state.GetGuid(type);
        public IEnumerable<Type> GetInterfaces(Type type) => state.GetInterfaces(type);

        protected override void Dispose(bool managed)
        {
            lock (locker)
            {
                if (state.BindingCount == 1)
                {
                    States.Remove(state.Assembly);
                    state.TryDispose();
                }
            }
        }
    }
}