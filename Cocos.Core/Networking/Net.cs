using Cocos.Core.Sets;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Cocos.Core.Networking
{
    public static class Net
    {
        public abstract class Behavior : Peer.Item
        {
            public class Set : Set<Behavior>
            {

            }
            public class AdapterDiscovery : Behavior
            {
                private Observable.Dict<NetworkInterface, Adapter.Windows>? winAdapters;

                protected override void Init()
                {
                    winAdapters = new();

                    NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
                    NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
                }
                protected override void OnInitialized()
                {
                    base.OnInitialized();
                    Task.Run(Update);
                }
                protected override void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        NetworkChange.NetworkAddressChanged -= NetworkChange_NetworkAddressChanged;
                        NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAvailabilityChanged;

                        if (winAdapters != default)
                        {
                            winAdapters.Clear();
                            winAdapters = default;
                        }
                    }

                    base.Dispose(disposing);
                }

                public override void Update()
                {
                    if (!IsInitialized || winAdapters == default) return;

                    var niList = NetworkInterface.GetAllNetworkInterfaces();
                    winAdapters.Keys.Except(niList).ToList().ForEach(NetworkInterface_Remove);
                    niList.Except(winAdapters.Keys).ToList().ForEach(NetworkInterface_Add);
                }

                private void NetworkInterface_Add(NetworkInterface item)
                {
                    if (item == default || winAdapters == default || winAdapters.ContainsKey(item)) return;

                    var aw = new Adapter.Windows(item);
                    winAdapters.Add(item, aw);

                    var str = aw.GetPublicPropertyStrings();
                }
                private void NetworkInterface_Remove(NetworkInterface item)
                {
                    if (item == default || winAdapters == default || !winAdapters.ContainsKey(item)) return;
                    winAdapters.Remove(item);
                }

                private void NetworkChange_NetworkAvailabilityChanged(object? sender, NetworkAvailabilityEventArgs e)
                {
                }
                private void NetworkChange_NetworkAddressChanged(object? sender, EventArgs e)
                {
                }
            }
        }

        [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
        public abstract class Adapter : Peer.Item
        {
            public class Set : Set<Adapter>
            {

            }

            public class Windows : Adapter
            {
                public override string ID => NetworkInterface.Id;
                public override string Name => NetworkInterface.Name;
                public override string Description => NetworkInterface.Description;

                public NetworkInterface NetworkInterface { get; private set; }

                public Windows(NetworkInterface networkInterface)
                {
                    NetworkInterface = networkInterface;
                }

                protected override void Init()
                {
                }
            }

            public abstract string ID { get; }
            public abstract string Name { get; }
            public abstract string Description { get; }

            private string GetDebuggerDisplay() => this.GetPublicPropertyStrings();
        }
        public class Peer : Initializable
        {
            public abstract class Item : Initializable
            {
                public class Set<T> : Observable.Set<T> where T : Item
                {
                    public Peer? Peer { get; init; }

                    public Set()
                    {
                        Options.UseRemoveForClear = true;
                        Options.UseRemoveAddForReplace = true;
                    }

                    protected override void OnItemAdded(T item)
                    {
                        if (item == default) return;
                        item.Peer = Peer;
                        item.Update();
                        base.OnItemAdded(item);
                    }
                    protected override void OnItemRemoved(T item)
                    {
                        if (item == default) return;
                        item.Peer = default;
                        base.OnItemRemoved(item);
                    }
                }

                public Peer? Peer { get; protected set; }

                public virtual void Update() { }
            }

            public Behavior.Set Behaviors { get; private set; }
            public Adapter.Set Adapters { get; private set; }

            public Peer()
            {
                Behaviors = new() { Peer = this };
                Adapters = new() { Peer = this };

                Init();
            }

            protected override void Init()
            {
                Adapters_Init();
                Behaviors_Init();
            }

            private void Adapters_Init() { }
            private void Behaviors_Init()
            {
                Behaviors.Add(new Behavior.AdapterDiscovery());
                Behaviors.ToList().ForEach(a => a.Initialize());
            }
        }
    }
}