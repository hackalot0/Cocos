using System.Reflection;

namespace Cocos.Reflection
{
    public class TypeManager : Disposable
    {
        public static TypeManagerStateDict States => states;
        private static TypeManagerStateDict states;

        private TypeManagerState state;
        private object locker = new object();

        public TypeManager(Assembly assembly) => Initialize(assembly);

        protected virtual void Initialize(Assembly assembly)
        {
            lock (locker) if (states == null) states = new TypeManagerStateDict();
            if (!states.TryGetValue(assembly, out state)) states.Add(assembly, state = new TypeManagerState(assembly));
            state.BindingCount += 1;
            state.Initialize();
        }

        protected override void Dispose(bool managed)
        {
            if (state.BindingCount == 1)
            {
                states.Remove(state.Assembly);
                state.TryDispose();
            }
        }
    }
}