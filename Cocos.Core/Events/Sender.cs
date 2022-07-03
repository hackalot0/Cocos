using System;

namespace Cocos.Core.Events
{
    public class Sender
    {
        public delegate void Event(Args args);

        public class Args : EventArgs
        {
            public object Sender { get; set; }
        }
    }
}