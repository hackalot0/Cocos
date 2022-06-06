using Cocos.Core.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocos.Core.Net
{
    public class Subscription
    {
        public class Set : ObservableSet<Subscription>
        {

        }
    }
    public class Service
    {
        public class Set : ObservableSet<Service>
        {

        }
    }
    public class Peer
    {
        public Subscription.Set Subscriptions { get; private set; }
        public Service.Set Services { get; private set; }

        public Peer()
        {
            Subscriptions = new();
            Services = new();

            Init();
        }

        protected virtual void Init()
        {
            Services.Options.UseRemoveForClear = true;
            Services.Options.UseRemoveAddForReplace = true;
            Services.ItemAdded += Services_ItemAdded;
            Services.ItemRemoved += Services_ItemRemoved;

            Subscriptions.Options.UseRemoveForClear = true;
            Subscriptions.Options.UseRemoveAddForReplace = true;
            Subscriptions.ItemAdded += Subscriptions_ItemAdded;
            Subscriptions.ItemRemoved += Subscriptions_ItemRemoved;
        }

        private void Services_ItemAdded(Events.Item<Service>.Args args)
        {
            throw new NotImplementedException();
        }
        private void Services_ItemRemoved(Events.Item<Service>.Args args)
        {
            throw new NotImplementedException();
        }

        private void Subscriptions_ItemAdded(Events.Item<Subscription>.Args args)
        {
            throw new NotImplementedException();
        }
        private void Subscriptions_ItemRemoved(Events.Item<Subscription>.Args args)
        {
            throw new NotImplementedException();
        }
    }
}