using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GK.Cocos.Modules.Stores
{
    public class FileSystemModuleStore : ModuleStore
    {
        public DirectoryInfo Directory { get => directory; set => directory = value; }

        private DirectoryInfo directory;

        public FileSystemModuleStore(DirectoryInfo directory) => this.directory = directory;
        public FileSystemModuleStore(string path) : this(new DirectoryInfo(path)) { }

        protected override void Init()
        {
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

        }
    }
}