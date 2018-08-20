using GK.Events;
using GK.Sets;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace GK.Reflection
{
    public class LibraryManagerState : StateBase
    {
        public event EventHandler<ItemEventArgs<Library>> LibraryLoad;

        public AppDomain AppDomain { get; }
        public LibrarySet Libraries { get; private set; }

        private Observer<Guid, Library> librariesObserver;

        public LibraryManagerState() : this(AppDomain.CurrentDomain) { }
        public LibraryManagerState(AppDomain appDomain) { AppDomain = appDomain; }

        protected override void Init()
        {
            librariesObserver = (Libraries = new LibrarySet()).Observe<Guid, Library>(Library_Added, Library_Removed);
            AppDomain.AssemblyLoad += AppDomain_AssemblyLoad;
            Libraries.Add(AppDomain.GetAssemblies().Select(Library.FromAssembly));
        }
        protected override void Dispose(bool managed)
        {
            AppDomain.AssemblyLoad -= AppDomain_AssemblyLoad;
            Libraries?.Clear();
            Libraries = null;
            base.Dispose(managed);
        }
        protected virtual void OnLibraryLoad(ItemEventArgs<Library> e) => LibraryLoad?.Invoke(this, e);

        private void AppDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args) => Libraries.Add(args.LoadedAssembly);

        private void Library_Added(INotifyCollectionChanged source, Guid key)
        {
            OnLibraryLoad(new ItemEventArgs<Library>(Libraries[key]));
        }
        private void Library_Removed(INotifyCollectionChanged source, Guid key)
        {

        }
    }
}