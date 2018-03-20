using System.Collections.Generic;
using GK.Sets;

namespace GK.Cocos.Modules
{
    public class ModuleStoreSet : ObservableSet<ModuleStore>
    {
        public ModuleManager ParentModuleManager { get; set; }

        public ModuleStoreSet() { }
        public ModuleStoreSet(IEnumerable<ModuleStore> items) : base(items) { }
    }
}