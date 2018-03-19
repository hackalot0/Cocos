using System;

namespace GK
{
    public static class StringExtension
    {
        public static byte[] GetBytes(this string str)
        {
            if (str == null) return null;
            byte[] b = new byte[str.Length * 2];
            if (b.Length > 0) Buffer.BlockCopy(str.ToCharArray(), 0, b, 0, b.Length);
            return b;
        }
        public static string GetString(this byte[] bytes)
        {
            if (bytes == null) return null;
            char[] c = new char[bytes.Length / 2];
            if (c.Length > 0) Buffer.BlockCopy(bytes, 0, c, 0, c.Length * 2);
            return new string(c);
        }
    }
}