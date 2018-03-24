using GK.Events;
using System;
using System.Reflection;

namespace GK.Reflection
{
    public class AssemblyManager : Initializable
    {
        public event EventHandler<ItemEventArgs<Assembly>> AssemblyLoad;

        public AppDomain AppDomain => state.AppDomain;
        public KeyedAssemblySet KnownAssemblies => state.KnownAssemblies;
        public AssemblyManagerState State => state;

        public static AssemblyManagerStateDict States => states;

        private static AssemblyManagerStateDict states;
        private AssemblyManagerState state;
        private object locker = new object();

        protected override void Init()
        {
            lock (locker)
            {
                if (states == null) states = new AssemblyManagerStateDict();
                AppDomain ad = AppDomain.CurrentDomain;
                if (!states.TryGetValue(ad, out state)) states.Add(ad, state = new AssemblyManagerState(ad));
                state.BindingCount += 1;
                state.Initialize();
                state.AssemblyLoad += State_AssemblyLoad;
            }
        }
        protected override void Dispose(bool managed)
        {
            lock (locker)
            {
                if (state.BindingCount == 1)
                {
                    state.AssemblyLoad -= State_AssemblyLoad;
                    states.Remove(state.AppDomain);
                    state.TryDispose();
                }
            }
        }

        private void State_AssemblyLoad(object sender, ItemEventArgs<Assembly> e) => AssemblyLoad?.Invoke(sender, e);
    }
}