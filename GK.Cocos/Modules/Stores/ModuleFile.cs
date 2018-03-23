using System;
using System.IO;
using System.Security.Cryptography;

namespace GK.Cocos.Modules
{
    public class ModuleFile
    {
        public Guid ID => id;
        public FileInfo FileInfo => fileInfo;

        private Guid id;
        private FileInfo fileInfo;

        public ModuleFile(FileInfo fileInfo) => id = (this.fileInfo = fileInfo) == null ? Guid.Empty : string.Join(" | ", this.fileInfo.FullName, this.fileInfo.Length).GetBytes().GetHashCode<MD5>().GetGuid();
    }
}