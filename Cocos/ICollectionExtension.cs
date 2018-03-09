using System.Collections.Generic;

namespace Cocos
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
    }
}