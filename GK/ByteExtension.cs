using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace GK
{
    public static class ByteExtension
    {
        public static Byte[] GetHashCode<T>(this byte[] bytes) where T : HashAlgorithm
        {
            T t = InitializeHashAlgorithm<T>();
            return t == null ? null : bytes.GetHashCode(t);
        }
        public static Byte[] GetHashCode<T>(this string text) where T : HashAlgorithm
        {
            T t = InitializeHashAlgorithm<T>();
            return t == null ? null : text.GetBytes().GetHashCode(t);
        }

        public static Byte[] GetHashCode(this byte[] bytes, HashAlgorithm algorithm) => algorithm == null || bytes == null ? null : algorithm.ComputeHash(bytes);
        private static T InitializeHashAlgorithm<T>() where T : HashAlgorithm
        {
            Type tHA = typeof(T);
            MethodInfo mi = tHA.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(IsHashAlgorithmCreateMethod);
            return (T)mi.Invoke(null, null);
        }
        private static bool IsHashAlgorithmCreateMethod(MethodInfo mi) => mi.Name == "Create" && mi.GetParameters().Length == 0;
    }
}