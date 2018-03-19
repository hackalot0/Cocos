using System.Collections.Specialized;
using GK.Reflection;
using GK.Sets;

namespace GK.Cocos.Modules
{
    public class ModuleManager : CocosRuntimeManager
    {
        public ModuleStoreSet ModuleStores => moduleStores;
        public AssemblyManager AssemblyManager => assemblyManager;

        private ModuleStoreSet moduleStores;
        private AssemblyManager assemblyManager;

        private Observer<ModuleStore> moduleStoreObserver;
        private Observer<KeyedAssembly> assemblyManagerObserver;

        protected override void Init()
        {
            assemblyManager = new AssemblyManager();
            assemblyManager.Initialize();
            assemblyManagerObserver = assemblyManager.State.KnownAssemblies.Observe<KeyedAssembly>(Assembly_Added, Assembly_Removed);

            moduleStoreObserver = (moduleStores = new ModuleStoreSet()).Observe<ModuleStore>(ModuleStore_Added, ModuleStore_Removed);
        }

        private void Assembly_Added(INotifyCollectionChanged itemSet, KeyedAssembly item)
        {
        }
        private void Assembly_Removed(INotifyCollectionChanged itemSet, KeyedAssembly item)
        {
        }

        private void ModuleStore_Added(INotifyCollectionChanged itemSet, ModuleStore item)
        {
            if (!item.IsInitialized) item.Initialize();

        }
        private void ModuleStore_Removed(INotifyCollectionChanged itemSet, ModuleStore item)
        {
        }
    }
}