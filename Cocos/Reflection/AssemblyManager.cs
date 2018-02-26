using Cocos.Sets;
using System;
using System.Collections.Generic;

namespace Cocos.Reflection
{
    public class AssemblyManager
    {
        #region Static stuff
        private static ObservableDict<AppDomain, AssemblyManagerState> states;
        private static DictObserver<AppDomain, AssemblyManagerState> stateObserver;

        static AssemblyManager()
        {
            stateObserver = new DictObserver<AppDomain, AssemblyManagerState>(states = new ObservableDict<AppDomain, AssemblyManagerState>())
            {
                ItemAddedAction = States_Added,
                ItemRemovedAction = States_Removed,
                CollectionResetAction = States_Cleared,
            };
        }

        private static void States_Added(IDictionary<AppDomain, AssemblyManagerState> dict, AppDomain key)
        {
        }
        private static void States_Removed(IDictionary<AppDomain, AssemblyManagerState> dict, AppDomain key)
        {
        }
        private static void States_Cleared(IDictionary<AppDomain, AssemblyManagerState> dict)
        {
        }
        #endregion

        private AssemblyManagerState state;

        public AssemblyManager() { Initialize(); }

        protected virtual void Initialize()
        {
            AppDomain ad = AppDomain.CurrentDomain;
            if (!states.TryGetValue(ad, out AssemblyManagerState state)) states.Add(ad, state = new AssemblyManagerState(ad));
        }
    }
}