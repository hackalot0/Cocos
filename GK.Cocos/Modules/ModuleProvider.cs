namespace GK.Cocos.Modules
{
    public abstract class ModuleProvider
    {
        public RuntimeModuleInfoSet AvailableModules => availableModules;
        public ModuleManager ModuleManager { get; set; }

        private RuntimeModuleInfoSet availableModules;

        public ModuleProvider()
        {
            availableModules = new RuntimeModuleInfoSet();
        }

        public abstract void Refresh();
    }
}