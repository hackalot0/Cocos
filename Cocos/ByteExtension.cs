using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace Cocos
{
    public static class ByteExtension
    {
        public static Byte[] GetHashCode<T>(this byte[] bytes) where T : HashAlgorithm
        {
            Type tHA = typeof(T);
            MethodInfo mi = tHA.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(IsHashAlgorithmCreateMethod);
            return mi == null ? null : bytes.GetHashCode((HashAlgorithm)mi.Invoke(null, null));
        }
        public static Byte[] GetHashCode(this byte[] bytes, HashAlgorithm algorithm) => algorithm.ComputeHash(bytes);

        private static bool IsHashAlgorithmCreateMethod(MethodInfo mi) => mi.Name == "Create" && mi.GetParameters().Length == 0;
    }
}