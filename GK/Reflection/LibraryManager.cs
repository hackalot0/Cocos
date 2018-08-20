using GK.Events;
using System;

namespace GK.Reflection
{
    public class LibraryManager : Initializable
    {
        public event EventHandler<ItemEventArgs<Library>> LibraryLoad;

        public AppDomain AppDomain => State.AppDomain;
        public LibrarySet Libraries => State.Libraries;
        public LibraryManagerState State => state;
        public static LibraryManagerStateDict States { get; private set; }

        private readonly object locker = new object();
        private LibraryManagerState state;

        static LibraryManager()
        {
            States = new LibraryManagerStateDict();
        }
        public LibraryManager() : this(appDomain: null) { }
        public LibraryManager(AppDomain appDomain)
        {
            if (appDomain == null) appDomain = AppDomain.CurrentDomain;
            lock (locker)
            {
                if (!States.TryGetValue(appDomain, out state)) States.Add(appDomain, state = new LibraryManagerState(appDomain));
                state.BindingCount += 1;
                state.LibraryLoad += State_LibraryLoad;
            }
        }

        protected override void Init() { state.Initialize(); }

        private void State_LibraryLoad(object sender, ItemEventArgs<Library> e) => LibraryLoad?.Invoke(sender, e);
    }
}