using GK.Cocos.Modules;
using GK.Cocos.Modules.Stores;
using GK.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GK
{
    public class CocosRuntime : Initializable
    {
        public ModuleManager ModuleManager => moduleManager;

        private ModuleManager moduleManager;

        protected override void Init()
        {
            moduleManager = new ModuleManager();
            moduleManager.Initialize();
            var tmp = AppDomain.CurrentDomain;
            moduleManager.ModuleStores.Add(new FileSystemModuleStore(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules")));
        }
    }
}