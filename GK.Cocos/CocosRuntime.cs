using System;
using System.Collections.Generic;

namespace GK.Cocos
{
    public class CocosRuntime : Initializable
    {
        public static IDictionary<AppDomain, CocosRuntimeState> RuntimeStates => runtimeStates;

        private AppDomain currentAppDomain;
        private object locker = new object();
        private CocosRuntimeState runtimeState;
        private static IDictionary<AppDomain, CocosRuntimeState> runtimeStates;

        public CocosRuntime()
        {
            lock (locker)
            {
                if (runtimeStates == null) runtimeStates = new Dictionary<AppDomain, CocosRuntimeState>();
                if (!runtimeStates.TryGetValue(currentAppDomain = AppDomain.CurrentDomain, out runtimeState))
                {
                    runtimeStates.Add(currentAppDomain, runtimeState = new CocosRuntimeState());
                    runtimeState.Initialize();
                }
            }
        }

        protected override void Init()
        {
        }
    }
}