using Cocos.Reflection;
using Cocos.Multitasking;
using System;
using System.Collections.Specialized;

namespace Cocos.ConsoleTests
{
    class Program
    {
        static int counter;

        static void Main(string[] args)
        {
            TaskManager tm = new TaskManager();
            TaskWorker tw = tm.CreateWorker(new ThreadOptions { IsBackground = false, Name = "HelloWorld", Priority = ThreadPriority.Normal, RunSynchronous = false, Action = ConsoleCounter });
            tw.Run();
            Console.ReadLine();
            tw.Abort();

            AssemblyManager am = new AssemblyManager();
        }

        private static void ConsoleCounter()
        {
            while (true) Console.WriteLine("Current Cycle: {0}", ++counter);
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