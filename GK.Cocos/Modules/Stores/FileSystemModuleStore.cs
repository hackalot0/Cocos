using GK.Sets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GK.Cocos.Modules.Stores
{
    public class FileSystemModuleStore : ModuleStore
    {
        private class ModuleFile : KeyedItem<FileInfo>
        {
            public FileInfo FileInfo => fileInfo;
            private FileInfo fileInfo;

            public ModuleFile() : base(GetKeyFor) { }
            public ModuleFile(FileInfo item) : base(GetKeyFor, item) { }

            private static Guid GetKeyFor(FileInfo arg) => arg == null ? Guid.Empty : arg.FullName.GetBytes().GetHashCode<MD5>().GetGuid();
        }

        public DirectoryInfo Directory { get => directory; set => directory = value; }
        public ModuleStoreSet ParentStoreSet { get => parentStoreSet; set => parentStoreSet = value; }

        private DirectoryInfo directory;
        private ModuleStoreSet parentStoreSet;
        private ObservableKeyedSet<Guid, ModuleFile> moduleFiles;

        public FileSystemModuleStore(DirectoryInfo directory) => this.directory = directory;
        public FileSystemModuleStore(string path) : this(new DirectoryInfo(path)) { }

        protected override void Init()
        {
            moduleFiles = new ObservableKeyedSet<Guid, ModuleFile>();
            directory.Refresh();
            if (!directory.Exists)
            {
                directory.Create();
                directory.Refresh();
            }
            else
            {
                Refresh_Files();
            }
        }

        protected void Refresh_Files()
        {
            ModuleFile mf = null;
            foreach (FileInfo fi in directory.GetFiles("*", SearchOption.AllDirectories).OrderBy(x => x.Extension))
            {
                try
                {
                    mf = new ModuleFile(fi);
                    moduleFiles.AddOrSkip(mf);
                }
                catch (FormatException) { }
            }
        }
    }
}