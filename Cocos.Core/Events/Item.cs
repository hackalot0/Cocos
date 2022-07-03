namespace Cocos.Core.Events
{
    public class Item<T>
    {
        public delegate void Event(Args args);

        public class Args : Sender.Args
        {
            public T Item { get; set; }
        }

        public class Replace
        {
            public delegate void Event(Args args);

            public class Args : Item<T>.Args
            {
                public T NewItem { get; set; }
            }
        }
    }
}