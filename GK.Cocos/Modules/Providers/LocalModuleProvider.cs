using System;

namespace GK.Cocos.Modules.Providers
{
    public class LocalModuleProvider : ModuleProvider
    {
        public string Path => path;
        private string path;

        public LocalModuleProvider(string path)
        {
            this.path = path;
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}