using Cocos.Events;
using Cocos.Sets;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace Cocos.Reflection
{
    public class AssemblyManagerState : StateBase
    {
        public event EventHandler<ItemEventArgs<Assembly>> AssemblyLoad;

        public AppDomain AppDomain => appDomain;
        public KeyedAssemblySet KnownAssemblies => knownAssemblies;

        private AppDomain appDomain;
        private KeyedAssemblySet knownAssemblies;
        private Observer<Guid, KeyedAssembly> knownAssembliesObserver;

        public AssemblyManagerState(AppDomain appDomain) => this.appDomain = appDomain;

        protected override void Init()
        {
            knownAssembliesObserver = (knownAssemblies = new KeyedAssemblySet()).Observe<Guid, KeyedAssembly>(KnownAssembly_Added, KnownAssembly_Removed);
            appDomain.AssemblyLoad += AppDomain_AssemblyLoad;
            knownAssemblies.Add(appDomain.GetAssemblies().Select(KeyedAssembly.FromAssembly));
        }
        protected override void Dispose(bool managed)
        {
            appDomain.AssemblyLoad -= AppDomain_AssemblyLoad;
            knownAssemblies?.Clear();
            appDomain = null;
            knownAssemblies = null;
            base.Dispose(managed);
        }

        protected virtual void OnAssemblyLoad(ItemEventArgs<Assembly> e) => AssemblyLoad?.Invoke(this, e);

        protected virtual void KnownAssembly_Added(INotifyCollectionChanged source, Guid key)
        {
            OnAssemblyLoad(new ItemEventArgs<Assembly>(knownAssemblies[key]));
        }
        protected virtual void KnownAssembly_Removed(INotifyCollectionChanged source, Guid key)
        {

        }

        protected virtual void AppDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args) { knownAssemblies.Add(args.LoadedAssembly); }
    }
}