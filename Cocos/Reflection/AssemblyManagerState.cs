using Cocos.Sets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cocos.Reflection
{
    public class AssemblyManagerState
    {
        public AppDomain AppDomain => appDomain;
        public ObservableDict<Guid, KeyedAssembly> Registry => registry;

        private bool isInitialized;
        private AppDomain appDomain;
        private List<KeyedAssembly> initLoaderList;
        private ObservableDict<Guid, KeyedAssembly> registry;

        public AssemblyManagerState(AppDomain appDomain)
        {
            this.appDomain = appDomain;
            registry = new ObservableDict<Guid, KeyedAssembly>();
            Initialize();
        }

        protected virtual void Initialize()
        {
            initLoaderList = new List<KeyedAssembly>();
            appDomain.AssemblyLoad += AppDomain_AssemblyLoad;
            initLoaderList.Add(appDomain.GetAssemblies().Select(KeyedAssembly.FromAssembly));

            throw new NotImplementedException("Hier weitermachen!");
            // Anstelle ObservableDict lieber eine ObservableKeyedCollection nutzen!
        }

        protected virtual void AppDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            if (!isInitialized) initLoaderList?.Add(args.LoadedAssembly);
            Console.WriteLine(args.LoadedAssembly.FullName);
        }
    }
}