using GK.Sets;
using System;
using System.IO;
using System.Linq;

namespace GK.Cocos.Modules.Stores
{
    public class FileSystemModuleStore : ModuleStore
    {
        public DirectoryInfo Directory { get => directory; set => directory = value; }

        private DirectoryInfo directory;
        private ObservableKeyedSet<Guid, ModuleContainer> moduleFiles;

        public FileSystemModuleStore(DirectoryInfo directory) => this.directory = directory;
        public FileSystemModuleStore(string path) : this(new DirectoryInfo(path)) { }

        protected override void Init()
        {
            moduleFiles = new ObservableKeyedSet<Guid, ModuleContainer>();
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
            ModuleContainer mf = null;
            foreach (FileInfo fi in directory.GetFiles("*", SearchOption.AllDirectories).OrderBy(x => x.Extension))
            {
                try
                {
                    //mf = new ModuleContainer(fi);
                    //moduleFiles.AddOrSkip(mf);
                }
                catch (FormatException) { }
            }
        }
    }
}