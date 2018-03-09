using System;

namespace Cocos
{
    public static class IDisposableExtension
    {
        public static bool TryDispose(this IDisposable target)
        {
            try { target?.Dispose(); }
            catch (ObjectDisposedException) { return false; }
            return true;
        }
    }
}