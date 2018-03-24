using GK.Cocos.Modules;
using GK.Cocos.Modules.Providers;
using System;
using System.IO;

namespace GK.Cocos
{
    public class CocosRuntimeState : Initializable
    {
        public SingletonManager SingletonManager => singletonManager;

        private SingletonManager singletonManager;

        protected override void Init()
        {
            (singletonManager = new SingletonManager() { CocosRuntimeState = this }).Initialize();
            ModuleManager moduleManager = new ModuleManager();
            moduleManager.Initialize();
            moduleManager.ModuleProviders.Add(new LocalModuleProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules")));

            singletonManager.Add(moduleManager);
        }
    }
}