using GK.Reflection;
using GK.Sets;
using System.Linq;
using System.Collections.Specialized;

namespace GK.Cocos.Modules.Providers
{
    public class AssemblyModuleProvider : ModuleProvider
    {
        private LibraryManager libManager;

        public AssemblyModuleProvider()
        {
            libManager = new LibraryManager();
            libManager.Initialize();
            libManager.Libraries.Observe<Library>(Library_Added, Library_Removed);
        }

        public override void Refresh()
        {
            foreach (Library lib in libManager.Libraries.Values.ToList())
            {

            }
        }

        private void Library_Added(INotifyCollectionChanged source, Library item)
        {
            CheckLibrary(item);
        }
        private void Library_Removed(INotifyCollectionChanged source, Library item)
        {
        }

        private void CheckLibrary(Library lib)
        {
            if (lib != null) {
                TypeManager tm = new TypeManager(lib);
                //var tmp = tm.InheritsOrEquals<CocosModuleInfo>();
            }
        }
    }
}