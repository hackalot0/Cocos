using System.Collections.Generic;
using System.Collections.Specialized;
using GK.Cocos.Modules.Providers;
using GK.Sets;

namespace GK.Cocos.Modules
{
    public class ModuleManager : CocosManager
    {
        public ModuleProviderSet ModuleProviders => moduleProviders;

        private ModuleProviderSet moduleProviders;
        private Observer<ModuleProvider> moduleProvidersObserver;
        private Dictionary<ModuleProvider, Observer<RuntimeModuleInfo>> moduleInfoObservers;

        protected override void Init()
        {
            moduleInfoObservers = new Dictionary<ModuleProvider, Observer<RuntimeModuleInfo>>();
            moduleProvidersObserver = (moduleProviders = new ModuleProviderSet()).Observe<ModuleProvider>(ModuleProvider_Added, ModuleProvider_Removed);

            moduleProviders.Add(new AssemblyModuleProvider() { ModuleManager = this });
        }

        protected virtual void Refresh_Modules()
        {

        }

        private void ModuleProvider_Added(INotifyCollectionChanged itemSet, ModuleProvider item)
        {
            moduleInfoObservers.Add(item, item.AvailableModules.Observe<RuntimeModuleInfo>(CocosModuleInfo_Added, CocosModuleInfo_Removed));
            item.ModuleManager = this;
        }
        private void ModuleProvider_Removed(INotifyCollectionChanged itemSet, ModuleProvider item)
        {
            item.ModuleManager = null;
            moduleInfoObservers[item].TryDispose();
            moduleInfoObservers.Remove(item);
        }

        private void CocosModuleInfo_Added(INotifyCollectionChanged itemSet, RuntimeModuleInfo item)
        {
        }
        private void CocosModuleInfo_Removed(INotifyCollectionChanged itemSet, RuntimeModuleInfo item)
        {
        }
    }
}