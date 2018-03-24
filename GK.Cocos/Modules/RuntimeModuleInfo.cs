namespace GK.Cocos.Modules
{
    public class RuntimeModuleInfo : ModuleInfoBase
    {
        public ModuleProvider Provider => provider;

        private ModuleProvider provider;

        public RuntimeModuleInfo(ModuleProvider provider) => this.provider = provider;
    }
}