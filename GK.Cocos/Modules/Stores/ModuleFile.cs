using GK.Sets;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GK.Cocos.Modules
{
    public class ModuleFile : KeyedItem<FileInfo>
    {
        public FileInfo FileInfo => fileInfo;
        private FileInfo fileInfo;

        public ModuleFile() : base(GetKeyFor) { }
        public ModuleFile(FileInfo item) : base(GetKeyFor, item) { }

        private static Guid GetKeyFor(FileInfo arg) => arg == null ? Guid.Empty : string.Join(" | ", arg.FullName, arg.Length).GetBytes().GetHashCode<MD5>().GetGuid();
    }
}