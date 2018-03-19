using System.Linq;
using System.Reflection;

namespace GK.Reflection
{
    public class TypeManagerState : StateBase
    {
        public Assembly Assembly => assembly;
        public KeyedTypeSet KnownTypes => knownTypes;

        private Assembly assembly;
        private KeyedTypeSet knownTypes;

        public TypeManagerState(Assembly assembly) => this.assembly = assembly;

        protected override void Init() => knownTypes = new KeyedTypeSet(assembly.GetTypes().AsParallel().Select(KeyedType.FromType));
        protected override void Dispose(bool managed)
        {
            knownTypes?.Clear();
            assembly = null;
            knownTypes = null;
            base.Dispose(managed);
        }
    }
}