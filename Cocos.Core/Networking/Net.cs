using Cocos.Core.Events;
using Cocos.Core.Sets;

namespace Cocos.Core.Networking
{
    public static class Net
    {
        public class Behavior
        {
            public class Set : ObservableSet<Behavior>
            {

            }
        }
        public class Adapter
        {
            public class Set : ObservableSet<Adapter>
            {

            }
        }
        public class Peer
        {
            public Behavior.Set Behaviors { get; private set; }
            public Adapter.Set Adapters { get; private set; }

            public Peer()
            {
                Behaviors = new();
                Adapters = new();

                Init();
            }

            protected virtual void Init()
            {
                Adapters.Options.UseRemoveForClear = true;
                Adapters.Options.UseRemoveAddForReplace = true;
                Adapters.ItemAdded += Adapters_ItemAdded;
                Adapters.ItemRemoved += Adapters_ItemRemoved;

                Behaviors.Options.UseRemoveForClear = true;
                Behaviors.Options.UseRemoveAddForReplace = true;
                Behaviors.ItemAdded += Behaviors_ItemAdded;
                Behaviors.ItemRemoved += Behaviors_ItemRemoved;
            }

            private void Adapters_ItemAdded(Item<Adapter>.Args args)
            {
                throw new NotImplementedException();
            }
            private void Adapters_ItemRemoved(Item<Adapter>.Args args)
            {
                throw new NotImplementedException();
            }

            private void Behaviors_ItemAdded(Item<Behavior>.Args args)
            {
                throw new NotImplementedException();
            }
            private void Behaviors_ItemRemoved(Item<Behavior>.Args args)
            {
                throw new NotImplementedException();
            }
        }
    }
}