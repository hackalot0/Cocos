using GK.Cocos.Modules;
using GK.Cocos.Modules.Stores;
using System;
using System.Collections.Generic;
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
            moduleManager.ModuleStores.Add(new FileSystemModuleStore(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules")));

            singletonManager.Add(moduleManager);
        }
    }
}