using Cocos.Reflection;
using System;
using System.Collections.Specialized;

namespace Cocos.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager am = new AssemblyManager();
        }

        private static void AssemblyAdded(INotifyCollectionChanged source, KeyedAssembly item)
        {
            Console.WriteLine("Assembly added: {0}", item.Assembly.SafeGetName());
        }
        private static void AssemblyRemoved(INotifyCollectionChanged source, KeyedAssembly item)
        {
            Console.WriteLine("Assembly removed: {0}", item.Assembly.SafeGetName());
        }
    }
}