using GK.Reflection;
using GK.Sets;
using System;
using System.Collections.Specialized;
using System.Reflection;

namespace GK.Cocos.Modules.Providers
{
    public class AssemblyModuleProvider : ModuleProvider
    {
        private AssemblyManager assemblyManager;

        public AssemblyModuleProvider()
        {
            assemblyManager = new AssemblyManager();
            assemblyManager.KnownAssemblies.Observe<Assembly>(Assembly_Added, Assembly_Removed);
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        private void Assembly_Added(INotifyCollectionChanged source, Assembly item)
        {
            CheckAssembly(item);
        }
        private void Assembly_Removed(INotifyCollectionChanged source, Assembly item)
        {
        }

        private void CheckAssembly(Assembly assembly)
        {
            if (assembly != null) {
                TypeManager tm = new TypeManager(assembly);
                var tmp = tm.InheritsOrEquals<CocosModuleInfo>();
            }
        }
    }
}