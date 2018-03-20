using System;
using System.Collections.Generic;

namespace GK
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) { if (source != null && action != null) foreach (T item in source) action(item); }
        public static IDictionary<TKey, ICollection<TValue>> ToOneToManyDict<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keyResolver) => ToOneToManyDict(source, keyResolver, null);
        public static IDictionary<TKey, ICollection<TValue>> ToOneToManyDict<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keyResolver, IEqualityComparer<TKey> keyComparer)
        {
            TKey key = default(TKey);
            ICollection<TValue> values = null;
            IDictionary<TKey, ICollection<TValue>> r = keyComparer == null ? new Dictionary<TKey, ICollection<TValue>>() : new Dictionary<TKey, ICollection<TValue>>(keyComparer);
            foreach (TValue value in source)
            {
                if (!r.TryGetValue(key = keyResolver(value), out values)) r.Add(key, values = new List<TValue>());
                values.Add(value);
            }
            return r;
        }
    }
}