using System;

namespace GK.Reflection
{
    public class AssemblyManager : Initializable
    {
        public static AssemblyManagerStateDict States => states;
        public AssemblyManagerState State => state;

        private static AssemblyManagerStateDict states;
        private AssemblyManagerState state;
        private object locker = new object();

        protected override void Dispose(bool managed)
        {
            if (state.BindingCount == 1)
            {
                states.Remove(state.AppDomain);
                state.TryDispose();
            }
        }

        protected override void Init()
        {
            lock (locker) if (states == null) states = new AssemblyManagerStateDict();
            AppDomain ad = AppDomain.CurrentDomain;
            if (!states.TryGetValue(ad, out state)) states.Add(ad, state = new AssemblyManagerState(ad));
            state.BindingCount += 1;
            state.Initialize();
        }
    }
}