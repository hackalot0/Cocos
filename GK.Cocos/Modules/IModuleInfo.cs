using System;

namespace GK.Cocos.Modules
{
    public interface IModuleInfo
    {
        string Name { get; set; }
        string Author { get; set; }
        Version Version { get; set; }
    }
}