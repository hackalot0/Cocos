using System;

namespace Cocos.Events
{
    public static class EventHelper
    {
        public static bool HandleChange<T>(ref T field, ref T newValue, Action<ChangeEventArgs<T>> eventHandler)
        {
            bool changed = !Equals(field, newValue);
            if (changed)
            {
                T item = field;
                field = newValue;
                eventHandler?.Invoke(new ChangeEventArgs<T>(item, newValue));
            }
            return changed;
        }
        public static bool HandleChange<T>(ref T field, ref T newValue, object sender, EventHandler<ChangeEventArgs<T>> eventHandler)
        {
            bool changed = !Equals(field, newValue);
            if (changed)
            {
                T item = field;
                field = newValue;
                eventHandler?.Invoke(sender, new ChangeEventArgs<T>(item, newValue));
            }
            return changed;
        }
    }
}