using System;

namespace GK
{
    public static class GuidExtension
    {
        public static Guid GetGuid(this byte[] bytes) => new Guid(bytes.GetRange(0, 16));
        public static Guid GetGuid(this byte[] bytes, int offset) => new Guid(bytes.GetRange(offset, 16));
    }
}