using System;

namespace GK.Cocos.Modules
{
    public abstract class ModuleInfoBase : IModuleInfo
    {
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }
        public virtual Version Version { get; set; }
    }
}