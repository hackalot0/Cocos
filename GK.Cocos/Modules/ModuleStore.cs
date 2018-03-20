using System;
using System.Collections.Generic;
using System.Text;

namespace GK.Cocos.Modules
{
    public abstract class ModuleStore : Initializable
    {
        public ModuleStoreSet ParentStoreSet { get; set; }
    }
}