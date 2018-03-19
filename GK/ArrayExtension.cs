using System;

namespace GK
{
    public static class ArrayExtension
    {
        public static void FastCopy(this Array src, int srcOffset, Array dst, int dstOffset, int length) => Array.Copy(src, srcOffset, dst, dstOffset, length);

        public static Array GetRange(this Array src, int offset, int length)
        {
            Array r = Array.CreateInstance(src?.GetType()?.GetElementType(), length);
            src.FastCopy(offset, r, 0, length);
            return r;
        }
        public static T[] GetRange<T>(this T[] src, int offset, int length)
        {
            T[] r = new T[length];
            src.FastCopy(offset, r, 0, length);
            return r;
        }
    }
}