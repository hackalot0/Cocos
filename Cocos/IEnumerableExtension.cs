using System;
using System.Collections.Generic;

namespace Cocos
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) { if (source != null && action != null) foreach (T item in source) action(item); }
    }
}