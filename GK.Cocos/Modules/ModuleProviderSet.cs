using System.Collections.Generic;
using GK.Sets;

namespace GK.Cocos.Modules
{
    public class ModuleProviderSet : ObservableSet<ModuleProvider>
    {
        public ModuleProviderSet() { }
        public ModuleProviderSet(IEnumerable<ModuleProvider> items) : base(items) { }
    }
}