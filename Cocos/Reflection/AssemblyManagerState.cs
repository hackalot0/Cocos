using System;
using System.Collections.Generic;
using System.Linq;

namespace Cocos.Reflection
{
    public class AssemblyManagerState : StateBase
    {
        public AppDomain AppDomain => appDomain;
        public KeyedAssemblySet KnownAssemblies => knownAssemblies;

        private AppDomain appDomain;
        private KeyedAssemblySet knownAssemblies;

        public AssemblyManagerState(AppDomain appDomain) => this.appDomain = appDomain;

        protected override void Init()
        {
            knownAssemblies = new KeyedAssemblySet();
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

        protected virtual void AppDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args) { knownAssemblies.Add(args.LoadedAssembly); }
    }
}