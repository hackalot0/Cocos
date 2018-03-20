using System.Collections.Generic;
using System.Linq;

namespace GK
{
    public static class ICollectionExtension
    {
        public static int Add<T>(this ICollection<T> col, IEnumerable<T> items)
        {
            int add = 0;
            foreach (T item in items)
            {
                col.Add(item);
                ++add;
            }
            return add;
        }

        public static bool AddOrSkip<T>(this ICollection<T> col, T item)
        {
            bool add = !col.Contains(item);
            if (add) col.Add(item);
            return add;
        }
        public static bool RemoveOrSkip<T>(this ICollection<T> col, T item)
        {
            bool del = col.Contains(item);
            if (del) col.Remove(item);
            return del;
        }

        public static int AddOrSkip<T>(this ICollection<T> col, IEnumerable<T> items) => items.Count(col.AddOrSkip);
        public static int RemoveOrSkip<T>(this ICollection<T> col, IEnumerable<T> items) => items.Count(col.RemoveOrSkip);
    }
}